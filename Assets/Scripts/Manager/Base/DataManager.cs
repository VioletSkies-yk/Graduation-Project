using Assets.Scripts.GamePlay.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Assets.Scripts.GamePlay.Data.TestDataList;

/// <summary>
/// 游戏入口。
/// </summary>
public class DataManager : OsSingletonMono<DataManager>
{
    private string ConfigPath= "Config";

    public Dictionary<Type, List<object>> dataTypeList = new Dictionary<Type, List<object>>();

    public Sheet1DataList Sheet1DataList;

    public Hero1DataList Hero1DataList;

    //public Test Test { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        InitDataList();
    }

    private List<T> JsonToEntity<T>(string name)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"{ConfigPath}/{name}");
        Assembly ab = Assembly.Load("Assembly-CSharp");
        Type type = ab.GetType(textAsset.name);
        List<T> objList = JsonConvert.DeserializeObject<List<T>>(textAsset.text);

        List<T> list = new List<T>();
        for (int i = 0; i < objList.Count; i++)
        {
            list.Add(objList[i]);
        }
        return list;
    }

    public void InitDataList()
    {
        Sheet1DataList = new Sheet1DataList();
        Sheet1DataList.Init(JsonToEntity<Sheet1>("Sheet1"));

        for (int i = 0; i < Sheet1DataList.StoreItemDic.Count; i++)
        {
            Debug.Log(Sheet1DataList.StoreItemDic[i].ID);
            Debug.Log(Sheet1DataList.StoreItemDic[i].MaxString);
            for (int j = 0; j < Sheet1DataList.StoreItemDic[i].GroupStringTest.Length; j++)
            {
                Debug.Log(Sheet1DataList.StoreItemDic[i].GroupStringTest[j]);
            }
        }


        Hero1DataList = new Hero1DataList();
        Hero1DataList.Init(JsonToEntity<Hero1>("Hero1"));

        for (int i = 0; i < Hero1DataList.HeroDataList.Count; i++)
        {
            Debug.Log(Hero1DataList.HeroDataList[i].ID);
        }

    }
}
