using Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ExcelTools : EditorWindow
{
    private static string m_JsonSavePath = "Json";
    private static string m_EntitySavePath = "Entity";
    private static string m_DirPath = "Excel";
    private Vector2 scrollPos;
    public static readonly char[] separators = { '|', ',' };

    [MenuItem("Excel/Read Excel")]
    static void OpenWindow()
    {
        ExcelTools window = (ExcelTools)EditorWindow.GetWindow(typeof(ExcelTools));
        window.Show();
    }
    private void OnEnable()
    {
        m_JsonSavePath = EditorPrefs.GetString("JsonSavePath");
        m_EntitySavePath = EditorPrefs.GetString("EntitySavePath");
        m_DirPath = EditorPrefs.GetString("DirPath");

    }
    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("表存放目录: ");
        GUILayout.TextField(m_DirPath);
        if (GUILayout.Button("选择表存放目录"))
        {
            OpenFileSelectWindow_Dir();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("实体存放目录: ");
        GUILayout.TextField(m_EntitySavePath);
        if (GUILayout.Button("选择实体所在目录"))
        {
            OpenFileSelectWindow_Entity();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Json存放目录: ");
        GUILayout.TextField(m_JsonSavePath);
        if (GUILayout.Button("选择Json存放目录"))
        {
            OpenFileSelectWindow_Json();
        }
        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("生成实体类"))
        {
            CreateEntities();
        }
        if (GUILayout.Button("生成Json"))
        {
            ExcelToJson();
        }
        EditorGUILayout.EndScrollView();
        if (EditorGUI.EndChangeCheck())
            EditorPrefs.SetString("JsonSavePath", m_JsonSavePath);
        EditorPrefs.SetString("EntitySavePath", m_EntitySavePath);
        EditorPrefs.SetString("DirPath", m_DirPath);
    }
    private void OpenFileSelectWindow_Dir()
    {
        m_DirPath = EditorUtility.OpenFolderPanel("自定义标题", Application.dataPath, "默认文件名");
    }
    private void OpenFileSelectWindow_Json()
    {
        m_JsonSavePath = EditorUtility.OpenFolderPanel("自定义标题", Application.dataPath, "默认文件名");
    }
    private void OpenFileSelectWindow_Entity()
    {
        m_EntitySavePath = EditorUtility.OpenFolderPanel("自定义标题", Application.dataPath, "默认文件名");
    }

    public void CreateEntities()
    {
        var list = GetAllExcel(m_DirPath);
        for (int i = 0; i < list.Count; i++)
        {
            AutoCreateClass(list[i].FullName);
        }
    }
    public void ExcelToJson()
    {
        //DataManager.Instance.dataTypeList.Clear();
        var list = GetAllExcel(m_DirPath);
        for (int i = 0; i < list.Count; i++)
        {
            ToJson(list[i].FullName);
        }
    }

    public static List<FileInfo> GetAllExcel(string dirPath)
    {
        List<FileInfo> fileList = new List<FileInfo>();
        if (!Directory.Exists(dirPath))
        {
            Debug.LogError("文件夹地址错误");
            return null;
        }
        DirectoryInfo direction = new DirectoryInfo(dirPath);//获取文件夹，exportPath是文件夹的路径
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".xlsx"))
            {
                if (!fileList.Contains(files[i]))
                {
                    fileList.Add(files[i]);
                }
            }
        }
        return fileList;
    }
    public static List<ExcelSheetData> ReadExcel(string filePath, ref string sheetName, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        List<ExcelSheetData> list = new List<ExcelSheetData>();
        int count = result.Tables.Count;
        for (int i = 0; i < count; i++)
        {
            //Tables[0] 下标0表示excel文件中第一张表的数据
            sheetName = result.Tables[i].TableName;
            Debug.Log(sheetName);
            columnNum = result.Tables[i].Columns.Count;
            rowNum = result.Tables[i].Rows.Count;
            stream.Close();
            list.Add(new ExcelSheetData(result.Tables[i].TableName, result.Tables[i].Columns.Count, result.Tables[i].Rows.Count, result.Tables[i].Rows));
        }
        return list;
    }
    public static void AutoCreateClass(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string storeEntityPath = $"{m_EntitySavePath}/{fileName}.cs";
        //获得表数据
        string sheetName = "";
        int columnNum = 0, rowNum = 0;
        var collectList = ReadExcel(filePath, ref sheetName, ref columnNum, ref rowNum);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < collectList.Count; index++)
        {
            sheetName = collectList[index].sheetName;
            columnNum = collectList[index].columnNum;
            rowNum = collectList[index].rowNum;
            DataRowCollection collect = collectList[index].collect;

            sb.AppendLine($"\tpublic class {sheetName}");
            sb.AppendLine("\t{");
            for (int i = 0; i < columnNum; i++)
            {
                sb.AppendLine($"\t\t/// <summary>\t");
                sb.AppendLine($"\t\t/// {collect[0][i].ToString()}");
                sb.AppendLine($"\t\t/// </summary>");

                sb.AppendLine($"\t\tpublic {collect[2][i].ToString()} {collect[1][i].ToString()};");
                sb.AppendLine("\t");
            }
            sb.AppendLine("\t}");
        }
        try
        {
            if (!Directory.Exists(m_EntitySavePath))
            {
                Directory.CreateDirectory(m_EntitySavePath);
            }
            if (!File.Exists(storeEntityPath))
            {
                File.Create(storeEntityPath).Dispose(); //避免资源占用
            }
            File.WriteAllText(storeEntityPath, sb.ToString());
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Excel转json时创建对应的实体类出错，实体类为：{fileName},e:{e.Message}");
        }
    }
    public void ToJson(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        //string storeJsonPath = $"{m_JsonSavePath}/{fileName}.json";
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            //获得表数据
            string sheetName = "";
            int columnNum = 0, rowNum = 0;
            var collectList = ReadExcel(filePath, ref sheetName, ref columnNum, ref rowNum);
            for (int index = 0; index < collectList.Count; index++)
            {
                sheetName = collectList[index].sheetName;
                columnNum = collectList[index].columnNum;
                rowNum = collectList[index].rowNum;
                DataRowCollection collect = collectList[index].collect;

                string storeJsonPath = $"{m_JsonSavePath}/{sheetName}.json";

                List<System.Object> objList = new List<object>();
                objList.Clear();
                //获取当前实体类
                Assembly ab = Assembly.Load("Assembly-CSharp");
                Type type = ab.GetType(sheetName);
                Debug.Log(type);
                if (type == null)
                {
                    Debug.LogError("你还没有创建对应的实体类!");
                    return;
                }
                if (!Directory.Exists(m_JsonSavePath))
                    Directory.CreateDirectory(m_JsonSavePath);

                //逐行添加数据 从第四行开始是数据
                for (int i = 3; i < rowNum; i++)
                {
                    //创建一个实体类
                    object obj = ab.CreateInstance(type.ToString());
                    for (int z = 0; z < columnNum; z++)
                    {
                        //获取表格中对应字段名的信息
                        FieldInfo info = type.GetField(collect[1][z].ToString());
                        //是数组
                        if (info.FieldType.IsArray)
                        {
                            string currentContext = collect[i][z].ToString();
                            string[] strs = currentContext.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            //数组内元素的类型
                            Type elementType = info.FieldType.GetElementType();

                            //创建数组
                            object array = Activator.CreateInstance(info.FieldType, new object[] { strs.Length });

                            MethodInfo setValue = info.FieldType.GetMethod("SetValue", new Type[2] { typeof(object), typeof(int) });
                            //给数组赋值
                            for (int m = 0; m < strs.Length; m++)
                            {
                                object v = Convert.ChangeType(strs[m], elementType);
                                setValue.Invoke(array, new object[] { v, m });
                            }
                            type.GetField(collect[1][z].ToString()).SetValue(obj, array);

                        }
                        //不是数组
                        else
                        {
                            //将表格中的数据转换为对应的数据类型
                            object value = Convert.ChangeType(collect[i][z].ToString(), info.FieldType);
                            //给对应的字段赋值
                            type.GetField(collect[1][z].ToString()).SetValue(obj, value);
                        }


                    }
                    objList.Add(obj);
                }

                //写入json文件

                if (!File.Exists(storeJsonPath))
                {
                    File.Create(storeJsonPath).Dispose();
                }
                File.WriteAllText(storeJsonPath, JsonConvert.SerializeObject(objList));
                Debug.Log($"生成{sheetName}Json文件成功");
            }
        }
    }

    public class ExcelSheetData
    {
        public string sheetName = "";
        public int columnNum = 0;
        public int rowNum = 0;
        public DataRowCollection collect;

        public ExcelSheetData(string sheetName, int columnNum, int rowNum, DataRowCollection collect)
        {
            this.sheetName = sheetName;
            this.columnNum = columnNum;
            this.rowNum = rowNum;
            this.collect = collect;
        }
    }

}
