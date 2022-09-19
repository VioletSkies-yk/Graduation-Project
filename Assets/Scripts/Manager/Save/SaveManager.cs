using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : OsSingleton<SaveManager>
{
    public SaveData GameData;

    private const string DataName = "GameData.txt";
    private string FullDataPath;

    /// <summary>
    ///  游戏开始时载入存档
    /// </summary>
    public bool InitLoadData(int index)
    {
        FullDataPath = DefineSavePath(index);
        string dataStr = LoadData(FullDataPath);
        if (dataStr == string.Empty)
        {
            GameData = null;
            return false;
        }
        else
        {
            GameData = JsonUtility.FromJson<SaveData>(dataStr);
            return true;
        }
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string LoadData(string path)
    {
        string dataStr = string.Empty;
        if (File.Exists(path))
        {
            StreamReader file = File.OpenText(path);
            dataStr = file.ReadToEnd();
            file.Close();
        }
        return dataStr;
    }

    // 保存
    public void Save(SaveData GameData,int index)
    {
        FullDataPath = DefineSavePath(index);

        string content = JsonUtility.ToJson(GameData, true);

        if (!File.Exists(FullDataPath))
        {
            // 创建后要主动释放：Dispose
            File.CreateText(FullDataPath).Dispose();
        }

        File.WriteAllText(FullDataPath, content, System.Text.Encoding.UTF8);

        EventManager.Instance.TriggerEvent(CONST.SaveDataSuccess);
    }


    // 清除存档
    public void DelData(int index)
    {
        FullDataPath = DefineSavePath(index);
        if (File.Exists(FullDataPath))
        {
            File.Delete(FullDataPath);
        }
    }

    public SaveData CreateSave()
    { //创建一个Save对象存储当前游戏数据
        SaveData data = new SaveData
        {
            coins = 10,
            playerPositionX = 1
        };
        return data;
    }

    private string DefineSavePath(int index)
    {
        return Application.persistentDataPath + "/" + DataName+index.ToString();
    }

    public bool isSaveDataEmpty(int index)
    {
        return !File.Exists(DefineSavePath(index));
    }
}

[Serializable]
public class SaveData
{
    public int coins;
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public List<float> enemyPositionX = new List<float>();
    public List<float> enemyPositionY = new List<float>();
    public List<bool> isDead = new List<bool>();

}