using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupLoadingContent : XXX.UI.Popup.BasePopup
{
    public class InitData
    {
        public string content;
    }
    [SerializeField] private TMP_Text txt;

    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        var intData = data as InitData;
        if (intData == null) { return; }
        txt.text = intData.content;

    }
}
