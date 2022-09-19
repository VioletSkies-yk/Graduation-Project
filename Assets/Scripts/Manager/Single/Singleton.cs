using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton on GameObject
/// </summary>
public class OsSingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }

    protected static T instance;
}

/// <summary>
/// Pure Singleton
/// </summary>
/// <returns></returns>
public class OsSingleton<T> where T : class, new()
{
    private static T _mSingleton = null;

    public static T Instance
    {
        get
        {
            if (_mSingleton == null)
            {
                _mSingleton = new T();
            }
            return _mSingleton;
        }
    }
}