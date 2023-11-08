using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTapToContinute : XXX.UI.Popup.BasePopup
{
    public class InitData
    {
        public Action tapToContinute;
    }
    private InitData _initData;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        _initData = (InitData)data;

    }
    protected override void OnClickButtonClose()
    {
        base.OnClickButtonClose();
        _initData.tapToContinute?.Invoke();
    }
}
