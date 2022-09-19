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
            Debug.LogError(content);
        }
        else
        {
            Debug.LogError(string.Format(content, contents));
        }
    }
}
