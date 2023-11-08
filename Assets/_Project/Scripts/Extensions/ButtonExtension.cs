using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void RegisterOnClick(this Button btn, Action onClick)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(delegate () {
            if (SoundManager.Instance != null)
               // SoundManager.Instance.PlayEffect(ESoundType.ButtonClick);
            onClick?.Invoke(); 
        });
    }
}
