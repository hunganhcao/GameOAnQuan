using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XXX.UI.Popup;

public class PopupLoadingTime : BasePopup
{
    public class DataInit
    {
        public float duration;
        public float step;
        public Action onDone;
        public Action<float> onUpdate;
    }

    [SerializeField] private Image imgFill;
    [SerializeField] private TMP_Text txtFill;
    [SerializeField] private string fomartFill;

    private Timer timer;
    private void Start()
    {
        var data = new DataInit()
        {
            duration = 3, 
            step = 0.02f,
            onUpdate = UpdateTime,
            onDone = OnDone,
        };
        Initialized(data, null);
    }
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        var dataInit = (DataInit)data;

        timer = new Timer(this, dataInit.duration, dataInit.step, dataInit.onDone, dataInit.onUpdate);
        timer.Start();
    }
    private void UpdateTime(float remaining)
    {
        var ratio = timer.RatioRemaing();
        imgFill.fillAmount = 1- ratio;
        txtFill.text = string.Format(fomartFill, ratio);
        txtFill.text = string.Format(fomartFill, 100 - UtilNumber.GetPercent<int>(ratio));
    }
    private void OnDone()
    {
        Close();
    }
}
