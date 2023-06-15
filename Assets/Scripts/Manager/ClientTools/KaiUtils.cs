using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class KaiUtils
{
    public static void SetActive(bool active, params GameObject[] gameObjects)
    {
        if (gameObjects.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects != null)
                {
                    gameObjects[i].SetActive(active);
                }
                else
                {
                    Log(gameObjects[i].name + "is Empty!");
                }
            }
        }
    }

    public static void Log(string content, params object[] contents)
    {
        if (contents.Length == 0)
        {
            Debug.Log(content);
        }
        else
        {
            Debug.Log(string.Format(content, contents));
        }
    }

    public static void Warn(string content, params object[] contents)
    {
        if (contents.Length == 0)
        {
            Debug.LogWarning(content);
        }
        else
        {
            Debug.LogWarning(string.Format(content, contents));
        }
    }
    public static void Error(string content, params object[] contents)
    {
        if (contents.Length == 0)
        {
            Debug.LogError($"Error: {content}");
        }
        else
        {
            Debug.LogError($"Error: {string.Format(content, contents)}");
        }
    }
    public static void Error(object contents)
    {
        Debug.LogError($"Error: {contents}");
    }


    #region Function

    public static string GetSceneName(int index)
    {
        switch (index)
        {
            case 1:
                return CONST.SCENE_NAME_LEVEL_01;
                break;
            case 2:
                return CONST.SCENE_NAME_LEVEL_02;
                break;
            case 3:
                return CONST.SCENE_NAME_LEVEL_03;
                break;
            default:
                return null;
                break;
        }
    }

    public static string GetBgName()
    {
        int index = SaveManager.Instance.farSceneIndex;
        switch (index)
        {
            case 1:
                return "LV1-r1";
                break;
            case 2:
                return "LV2-r1";
                break;
            case 3:
                return "LV3-r1";
                break;
            default:
                return "LV1-r1";
                return null;
                break;
        }
    }

    public static string GetSceneBGM(int index)
    {
        switch (index)
        {
            case 1:
                return "LV1";
                break;
            case 2:
                return "LV2";
                break;
            case 3:
                return "LV3";
                break;
            default:
                return null;
                break;
        }
    }

    public static string[] GetSceneSubtitles(int index)
    {
        List<string> sbs;
        switch (index)
        {
            case 1:
                sbs = new List<string>()
                { "好久没感觉到这么安全了",
                    "真实还是虚假，我好像一直都在医院里吧",
                    "我怎么好像什么都不记得了，但是又很熟悉......",
                    "儿时梦想的乐园嘛？怎么会出现在这里？" };
                break;
            case 2:
                sbs = new List<string>()
                {
                    "回来了？梦嘛？还是在做梦）",
                    "仔细辨别，你究竟是回来了？还是正在前进？不要问我是谁，真相需要你自己寻找",
                    "梦幻与虚无交织，真美啊",
                    "只有痛苦才能换来短暂的宁静吗，真想永远留在这里",
                    "你是有使命的",
                    "使命？什么使命？刚才好像是我自己不自觉说出的话" };

                break;
            case 3:
                sbs = new List<string>()
                {
                    "切割",
                    "切割",
                    "尽情切割吧",
                    "尽情切割吧",
                    "尽情切割吧"
                };

                break;
            default:
                return null;
                break;
        }
        return sbs.ToArray() ;
    }

    #endregion
}
