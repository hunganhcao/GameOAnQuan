using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.Attribute;
using static EnhancedUI.EnhancedScroller.EnhancedScroller;

[System.Serializable]
public class EnhancedScroller1 : IEnhancedScrollerDelegate
{   
    [SerializeField] public EnhancedScroller scroller;
    [SerializeField] private BaseCellView cellViewPrefab;
    [SerializeField] private ELayoutDirection scrollDirection;
    [ShowIf(nameof(scrollDirection), ELayoutDirection.None)]
    [SerializeField] private float cellSize;

    private SmallList<BaseDataItem> _data;

    public void Initialized()
    {
        scroller.Delegate = this;
        var rect = cellViewPrefab.GetComponent<RectTransform>().rect;
        switch (scrollDirection)
        {
            case ELayoutDirection.Horizontal:
                cellSize = rect.width; break;
            case ELayoutDirection.Vertical:
                cellSize = rect.height; break;
        }
    }
    public void Clear()
    {
        _data = new SmallList<BaseDataItem>();
        scroller.ReloadData();
    }
    public void AddData(BaseDataItem item)
    {
        _data.Add(item);
        scroller.ReloadData();
    }
    public void LoadData(List<BaseDataItem> datas)
    {
        _data = new SmallList<BaseDataItem>();
        for (var i = 0; i < datas.Count; i++)
            _data.Add(datas[i]);
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return cellSize;
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        var cellView = scroller.GetCellView(cellViewPrefab) as BaseCellView;
        
        cellView.name = string.Format("{0} {1}", cellViewPrefab.name, dataIndex);

        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
    #endregion
}
