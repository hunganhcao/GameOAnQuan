using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntReference
{
    private int _value;
    [SerializeField] private EventName eventName;

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
}
