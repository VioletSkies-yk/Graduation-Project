using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// My event type.
/// </summary>
[Serializable]
public class GameEvent : UnityEvent
{

}

public class GameEvent<T> : UnityEvent<T>
{

}

/// <summary>
/// Message system.
/// </summary>
public class EventManager : OsSingleton<EventManager>
{
    // Events list
    private readonly Dictionary<string, GameEvent> _eventDictionary = new Dictionary<string, GameEvent>();
    private readonly Dictionary<string, List<UnityAction>> _listenerDic = new Dictionary<string, List<UnityAction>>(); // 排重用
    private readonly Hashtable _paramEventsTable = new Hashtable();
    private readonly Dictionary<string, ArrayList> _paramListenerDic = new Dictionary<string, ArrayList>(); // 排重用

    //private EventManager()
    //{

    //}

    /// <summary>
    /// Start listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public void StartListening(string eventName, UnityAction listener)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            Debug.LogError("非法的参数：事件名称为空!");
            return;
        }
        if (listener == null)
        {
            Debug.LogError("非法的参数：监听回调为空!， 事件名称为 " + eventName);
            return;
        }

        var listenerDic = Instance._listenerDic;
        List<UnityAction> listenerList;
        if (listenerDic.TryGetValue(eventName, out listenerList))
        {
            foreach (var registeredListener in listenerList)
            {
                if (registeredListener == listener)
                {
                    Debug.LogError("重复的监听者： eventName == " + eventName + ", listener == " + listener.Method.Name);
                    return;
                }
            }
            listenerList.Add(listener);
        }
        else
        {
            listenerDic.Add(eventName, new List<UnityAction> { listener });
        }

        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StartListening<T>(string eventName, UnityAction<T> listener)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            Debug.LogError("非法的参数：事件名称为空!");
            return;
        }
        if (listener == null)
        {
            Debug.LogError("非法的参数：监听回调为空!， 事件名称为 " + eventName);
            return;
        }

        var paramListenerDic = Instance._paramListenerDic;
        ArrayList paramListenerList;
        if (paramListenerDic.TryGetValue(eventName, out paramListenerList))
        {
            foreach (var registeredListener in paramListenerList)
            {
                var r = registeredListener as UnityAction<T>;
                if (r == null)
                {
                    continue;
                }
                if (r == listener)
                {
                    Debug.LogError("重复的监听者： eventName == " + eventName + ", listener == " + listener.Method.Name);
                    return;
                }
            }

            paramListenerList.Add(listener);
        }
        else
        {
            paramListenerDic.Add(eventName, new ArrayList { listener });
        }

        var tType = typeof(T);
        Dictionary<string, GameEvent<T>> eventDic = null;
        if (Instance._paramEventsTable.Contains(tType))
        {
            eventDic = (Dictionary<string, GameEvent<T>>)Instance._paramEventsTable[tType];
        }
        else
        {
            eventDic = new Dictionary<string, GameEvent<T>>();
            Instance._paramEventsTable.Add(tType, eventDic);
        }

        GameEvent<T> thisEvent = null;
        if (eventDic.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent<T>();
            thisEvent.AddListener(listener);
            eventDic.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stop listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public void StopListening(string eventName, UnityAction listener)
    {
        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
            var listenerDic = Instance._listenerDic;
            List<UnityAction> listenerList;
            if (listenerDic.TryGetValue(eventName, out listenerList) && listenerList.Contains(listener))
            {
                listenerList.Remove(listener);
            }
        }
    }

    public void StopListening<T>(string eventName, UnityAction<T> listener)
    {
        var tType = typeof(T);
        var paramEventsTable = Instance._paramEventsTable;
        if (paramEventsTable.ContainsKey(tType))
        {
            GameEvent<T> thisEvent;
            var eventDic = (Dictionary<string, GameEvent<T>>)paramEventsTable[tType];
            if (eventDic.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
                ArrayList paramListenerList;
                if (Instance._paramListenerDic.TryGetValue(eventName, out paramListenerList) && paramListenerList.Contains(listener))
                {
                    paramListenerList.Remove(listener);
                }
            }
        }
    }

    /// <summary>
    /// Trigger specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    public void TriggerEvent(string eventName)
    {
        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public void TriggerEvent<T>(string eventName, T param)
    {
        var tType = typeof(T);
        if (!Instance._paramEventsTable.ContainsKey(tType)) return;
        var eventDic = (Dictionary<string, GameEvent<T>>)Instance._paramEventsTable[tType];
        GameEvent<T> thisEvent;
        if (eventDic.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public void ClearEvents()
    {
        var events = Instance._eventDictionary.Values;
        foreach (var @event in events)
        {
            @event.RemoveAllListeners();
        }
        Instance._eventDictionary.Clear();
        var listenerDic = Instance._listenerDic;
        foreach (var pair in listenerDic)
        {
            var listeners = pair.Value;
            if (listeners != null)
            {
                listeners.Clear();
            }
        }
        listenerDic.Clear();

        var paramEventsTable = Instance._paramEventsTable;
        var paramEvents = paramEventsTable.Values;
        foreach (var paramEvent in paramEvents)
        {
            var dic = paramEvent as IDictionary;
            if (dic != null)
            {
                foreach (DictionaryEntry entry in dic)
                {
                    var unityEvent = entry.Value as UnityEventBase;
                    if (unityEvent != null)
                    {
                        unityEvent.RemoveAllListeners();
                    }
                }
                dic.Clear();
            }
        }
        paramEventsTable.Clear();
        foreach (var pair in Instance._listenerDic)
        {
            var listeners = pair.Value;
            if (listeners != null)
            {
                listeners.Clear();
            }
        }
        Instance._listenerDic.Clear();
    }

    public bool HasListener(string eventName)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            return false;
        }
        var listenerDic = Instance._listenerDic;
        if (listenerDic.TryGetValue(eventName, out List<UnityAction> listenerList))
        {
            return listenerList.Count > 0;
        }
        var paramListenerDic = Instance._paramListenerDic;
        if (paramListenerDic.TryGetValue(eventName, out ArrayList paramListenerList))
        {
            return paramListenerList.Count > 0;
        }
        return false;
    }
}
