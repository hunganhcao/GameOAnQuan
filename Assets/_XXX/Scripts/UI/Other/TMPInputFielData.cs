using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class TMPInputFielData : MonoBehaviour
{
    [SerializeField] private KeyStorange keyStorange;
    private string key => keyStorange.ToString();
    private TMP_InputField input;

    private void Start()
    {
        input = GetComponent<TMP_InputField>();
        input.onValueChanged.AddListener(HandleValueChange);
        input.text = PlayerPrefs.GetString(key, "");
    }

    private void HandleValueChange(string msg)
    {
        PlayerPrefs.SetString(key, msg);
    }
}
