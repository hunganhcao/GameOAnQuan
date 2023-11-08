using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : XXX.UI.Popup.BasePopup
{
    [SerializeField] private SwitchButton btnMusic;
    [SerializeField] private SwitchButton btnEffect;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        btnMusic.SetData(KeyStorange.ActiveMusic, OnChangeMusic);
        btnEffect.SetData(KeyStorange.ActiveSoundEffect, OnChangeEffect);
    }

    private void OnChangeEffect(bool data)
    {
        SoundManager.Instance.SetActiveEffect(data);
    }

    private void OnChangeMusic(bool data)
    {
        SoundManager.Instance.SetActiveMusic(data);
    }
}
