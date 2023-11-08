using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseCellView : EnhancedScrollerCellView
{
    [SerializeField] private GameObject obj;
    public virtual void SetData(BaseDataItem data)
    {
        obj.SetActive(!(data.cellType == BaseDataItem.CellType.Spacer));
    }
}
