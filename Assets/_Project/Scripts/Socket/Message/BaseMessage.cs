using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class DataRoom
{
    public int IdRoom;
}
[System.Serializable]
public class BaseMessage 
{
    public Code code;
    public string message;

    public BaseMessage() { }
    public BaseMessage(Code code, object data)
    {
        this.code = code;
        this.message = JsonConvert.SerializeObject(data);
    }

    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
    public BaseMessage FromJson(string msg)
    {
        try
        {
            return JsonConvert.DeserializeObject<BaseMessage>(msg);
        }
        catch(Exception e)
        {
            try
            {
                Debug.LogError(e.Message);
                return JsonUtility.FromJson<BaseMessage>(msg);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                var a = new BaseMessage();
                JsonUtility.FromJsonOverwrite(msg, a);
                return a;
            }
        }
    }
}
