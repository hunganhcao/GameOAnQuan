using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SocketCall;

public class PopupVs : XXX.UI.Popup.BasePopup
{
    public class InitData
    {
        public Player mine;
        public Player other;
        public float timeWaitClose;
        public Action onDone;
    }
    [SerializeField] private AvatarDisplay avtMine;
    [SerializeField] private AvatarDisplay avtOther;

    private InitData _initData;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        _initData = data as InitData;
        if (_initData == null) { return; }

        avtMine.Initialized(true, "");
        avtOther.Initialized(false, _initData.other.Id);
        StartCoroutine(WaitToClose());
    }
    private IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(_initData.timeWaitClose);
        Close();
        _initData.onDone?.Invoke();
    }
}
