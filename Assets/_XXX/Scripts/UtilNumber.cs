using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilNumber 
{
    public static T GetPercent<T>(float number) where T : struct
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)(int)(number * 100);
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)(number * 100f);
        }
        else
        {
            throw new ArgumentException("Unsupported type");
        }
    }
}
