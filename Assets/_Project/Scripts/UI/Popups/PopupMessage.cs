using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopupMessage : XXX.UI.Popup.BasePopup
{
    [SerializeField] private TMP_Text txt_Message;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        var msg = (string)data;
        if (!string.IsNullOrEmpty(msg))
        {
            txt_Message.text = msg;
        }
    }
}
