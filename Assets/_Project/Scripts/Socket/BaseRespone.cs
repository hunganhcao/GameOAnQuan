using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseRespone<T>
{
    public int code;
    public string message;
    public T data;

    public void ToSuccess()
    {
        code = 200;
    }
    public void ToError(string msg)
    {
        code = 400;
        message = msg;
    }
}
