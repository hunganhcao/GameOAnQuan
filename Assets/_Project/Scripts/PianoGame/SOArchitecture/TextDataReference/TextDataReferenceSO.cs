using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDataReferenceSO<T> : MonoBehaviour
{
    [SerializeField] protected string format;
    [SerializeField] protected TMP_Text _txt;
    [SerializeField] protected BaseSODataReference<T> _data;
    private void OnValidate()
    {
        if (_txt == null) _txt = GetComponent<TMP_Text>();
    }
    private void Awake()
    {
        _data.OnChangeValue += HandleValueChange;
    }
    private void OnDestroy()
    {
        _data.OnChangeValue -= HandleValueChange;
    }
    private void Start()
    {
        UpdateText();
    }
    private void HandleValueChange(T oldValue, T newValue)
    {
        UpdateText();
    }
    private void UpdateText()
    {
        _txt.text = string.Format(format, _data.Value);
    }
}

