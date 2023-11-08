using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPInputEmptyDisable : MonoBehaviour
{
    private TMP_InputField input;
    private void Awake()
    {
        input = GetComponent<TMP_InputField>();
    }
    private void OnDisable()
    {
        input.text = string.Empty;
    }
}
