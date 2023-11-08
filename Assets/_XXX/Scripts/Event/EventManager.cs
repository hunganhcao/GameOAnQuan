using Newtonsoft.Json;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
public static class EventManager 
{
    public delegate void CallBackObserver(object data);

    static Dictionary<string, HashSet<CallBackObserver>> dictObserver = new Dictionary<string, HashSet<CallBackObserver>>();
    // Use this for initialization
    public static void AddEvent(EventName topic, CallBackObserver callbackObserver)
    {
        var topicName = topic.ToString();
        HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
        listObserver.Add(callbackObserver);
    }

    public static void RemoveEvent(EventName topic, CallBackObserver callbackObserver)
    {
        var topicName = topic.ToString();
        HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
        if (listObserver.Contains(callbackObserver))
        {
            listObserver.Remove(callbackObserver);
        }
    }

    public static void Notify(EventName topic, object Data)
    {
        var topicName = topic.ToString();
        try
        {
            Debug.Log(string.Format("Event: {0}, data: {1}", topicName, JsonConvert.SerializeObject(Data)));
        }
        catch
        {
            Debug.Log(string.Format("Event: {0}", topicName));
        }
        HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
        foreach (CallBackObserver observer in listObserver)
        {
            observer(Data);
        }
    }

    public static void Notify(EventName topic)
    {
        var topicName = topic.ToString();
        Debug.Log(string.Format("Event: {0}, data: {1}", topicName, ""));
        HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
        foreach (CallBackObserver observer in listObserver)
        {
            observer(null);
        }
    }

    static private HashSet<CallBackObserver> CreateListObserverForTopic(string topicName)
    {
        if (!dictObserver.ContainsKey(topicName))
            dictObserver.Add(topicName, new HashSet<CallBackObserver>());
        return dictObserver[topicName];
    }
}
