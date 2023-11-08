using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.UI.Popup;

[System.Serializable]
public class ListenMessageSocket<TDataReceive>
{
    [SerializeField] private EventName e_Name;
    private Action<TDataReceive> a_Receive;


    public void RegisterEvent(Action<TDataReceive> onReceive)
    {
        EventManager.AddEvent(e_Name, Handle);
        a_Receive = onReceive;
    }
    public void RegisterEvent(EventName eventName, Action<TDataReceive> onReceive)
    {
        e_Name = eventName;
        EventManager.AddEvent(e_Name, Handle);
        a_Receive = onReceive;
    }

    private void Handle(object data)
    {
        var obj = JsonConvert.DeserializeObject<BaseRespone<TDataReceive>>((string)data);
        if(obj == null)
        {
            PopupManager.Instance.ShowPopupWarming("Dữ liệu trả về rỗng");
        }
        if(obj.code != 200)
        {
            PopupManager.Instance.ShowPopupWarming(obj.message);
            Debug.Log("code khác 200");
            return;
        }
        a_Receive?.Invoke(obj.data);
    }
}
