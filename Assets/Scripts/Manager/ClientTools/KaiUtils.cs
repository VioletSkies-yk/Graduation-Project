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
            default:
                return null;
                break;
        }
    }

    public static string GetBgName(int index)
    {
        switch (index)
        {
            case 1:
                return "LV1-r1";
                break;
            case 2:
                return "LV2-r1";
                break;
            default:
                return null;
                break;
        }
    }

    #endregion
}
