using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSODataReference<T> : BaseSODescription
{
    [SerializeField] protected T _value;
    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            var oldValue = _value;
            var newValue = value;
            _value = value;
            RaiseEvent(oldValue, newValue);
        }
    }
    public Action<T, T> OnChangeValue { get; set; } = default;
    private void RaiseEvent(T oldValue, T newValue)
    {
        if (OnChangeValue != null)
            OnChangeValue.Invoke(oldValue, newValue);
    }
}