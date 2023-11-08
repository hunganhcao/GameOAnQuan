using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataItem
{
    public enum CellType
    {
        MyText,
        Spacer,
        OtherText
    }
    public CellType cellType;
    public float cellSize;
}
