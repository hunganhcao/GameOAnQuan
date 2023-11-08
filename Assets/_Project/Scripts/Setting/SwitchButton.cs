using DG.DemiLib;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Image bg;
    [SerializeField] private Transform handle;
    [SerializeField] private float distance;

    private void OnValidate()
    {
        if(distance == 0) 
        {
            distance = Math.Abs(handle.localPosition.x);
        }
    }

    private Action<bool> _onChange;
    private bool _isOn;
    private KeyStorange _keyStorange;
    private bool IsOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
            var timeChange = 0.3f;
            var target = value ? distance : -distance;
            var bgColor = value ? Color.green : Color.white;
            handle.DOLocalMoveX(target, timeChange);
            bg.DOColor(bgColor, timeChange);
            PlayPrefSystem.SetBool(_keyStorange, value);
        }
    }

    public void SetData(KeyStorange key, Action<bool> onChange)
    {
        IsOn = PlayPrefSystem.GetBool(key, true);
        _keyStorange = key;
        _onChange = onChange;
        btn.RegisterOnClick(OnClickBtn);
    }

    private void OnClickBtn()
    {
        IsOn = !IsOn;
        _onChange?.Invoke(IsOn); 
    }
}
