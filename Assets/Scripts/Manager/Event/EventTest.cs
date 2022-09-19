using System;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    private const string Event1 = "event1";
    private const string Event2 = "event2";

    private void Start()
    {
        EventManager.Instance.StartListening(Event1, Callback);
        EventManager.Instance.StartListening(Event2, Callback);

        Debug.LogError("event1, event2");
        EventManager.Instance.TriggerEvent(Event1);
        EventManager.Instance.TriggerEvent(Event2);

        Debug.LogError("Add another event1 and call");
        EventManager.Instance.StartListening(Event1, Callback);
        EventManager.Instance.TriggerEvent(Event1);
    }


    private void Callback()
    {
        Debug.LogError("callback call");
    }
}
