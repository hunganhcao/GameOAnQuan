using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPrefSystem
{
    public static bool GetBool(KeyStorange keyStorange, bool defaultValue)
    {
        var de = defaultValue ? 1 : 0;
        return PlayerPrefs.GetInt(keyStorange.ToString(), de) == 1 ? true : false;
    }
    public static void SetBool(KeyStorange keyStorange, bool value)
    {
        int de = value ? 1 : 0;
        PlayerPrefs.SetInt(keyStorange.ToString(), de);
    }
}
