using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ETypeOperator
{
    SUMMARTION = 0, MULTIPLICATION = 1, SUBTRACTION = 2
}
[System.Serializable]
public class PianoQuest
{
    public int a;
    public int b;
    public ETypeOperator t;
    public int[] A;
    public int p;// vị trí đúng
}