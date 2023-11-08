using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtension 
{
    public static void Notify(this MonoBehaviour mono, EventName eventName, object data) 
    {
        EventManager.Notify(eventName, data);
    }
}
