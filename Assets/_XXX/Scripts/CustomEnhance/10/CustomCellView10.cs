using EnhancedUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomCellView10<TData> : BaseCellView<TData>
{
    [SerializeField] public BaseCellView<TData>[] rowCellViews;
    public override void SetData(ref SmallList<TData> data, int startingIndex)
    {
        base.SetData(ref data, startingIndex);
        for (var i = 0; i < rowCellViews.Length; i++)
        {
            var rowCellView = rowCellViews[i];
            var isActive = startingIndex + i < data.Count;
            rowCellView.gameObject.SetActive(isActive);
            if (isActive)
            {
                rowCellView.SetData(data[startingIndex + i]);
            }
        }
    }
}
