using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInReference : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private EventName eventName;

    void Awake()
    {
        EventManager.AddEvent(eventName, HandleChangeValue);
    }
    private void OnDestroy()
    {
        EventManager.AddEvent(eventName, HandleChangeValue);
    }

    private void HandleChangeValue(object data)
    {
        var respon = (int)data;
        txt.text = respon.ToString();
    }
}
