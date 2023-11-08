using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Text txt;
    [SerializeField] private InputField input;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var text = input.text;
            for(int i = 1; i < text.Length; i++)
            {
                txt.text = text.Substring(0, i);
                LayoutRebuilder.ForceRebuildLayoutImmediate(txt.rectTransform);
                Debug.Log(i + ": " + txt.text + ": " + txt.rectTransform.rect.width);
            }
        }
    }
}
