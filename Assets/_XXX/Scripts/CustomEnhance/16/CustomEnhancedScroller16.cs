using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class CustomEnhancedScroller16 : IEnhancedScrollerDelegate
{
    private List<BaseDataItem> _data;
    [SerializeField] private float _totalCellSize = 0;
    [SerializeField] private float _oldScrollPosition = 0;
    public EnhancedScroller scroller;
    public BaseCellView myTextCellViewPrefab;
    public BaseCellView otherTextCellViewPrefab;
    public BaseCellView spacerCellViewPrefab;
    public delegate int GetCellSize(BaseDataItem data);

    public void ReloadData(List<BaseDataItem> datas)
    {
        scroller.Delegate = this;
        _data = new List<BaseDataItem>();
        _data.Add(new BaseDataItem() { cellType = BaseDataItem.CellType.Spacer });
        ResizeScroller();
        foreach (var item in datas)
        {
            AddNewRow(item, true);
        }
    }
    public void AddNewRow(BaseDataItem data, bool isReloadScroll)
    {
        scroller.ClearAll();
        _oldScrollPosition = scroller.ScrollPosition;
        scroller.ScrollPosition = 0;
        _data.Add(data);
        ResizeScroller();
        if (isReloadScroll)
            scroller.JumpToDataIndex(_data.Count - 1, jumpComplete: ResetSpacer);
    }
    private void ResizeScroller()
    {
        var scrollRectSize = scroller.ScrollRectSize;
        var offset = _oldScrollPosition - scroller.ScrollSize;

        var rectTransform = scroller.GetComponent<RectTransform>();
        var size = rectTransform.sizeDelta;
        rectTransform.sizeDelta = new Vector2(size.x, float.MaxValue);

        _totalCellSize = scroller.padding.top + scroller.padding.bottom;

        for (int i = 1; i < _data.Count; i++)
        {
            _totalCellSize += _data[i].cellSize + (i < _data.Count - 1 ? scroller.spacing : 0);
        }

        _data[0].cellSize = scrollRectSize;
        rectTransform.sizeDelta = size;
        scroller.ReloadData();
        scroller.ScrollPosition = (_totalCellSize - _data[_data.Count - 1].cellSize) + offset;
    }
    private void ResetSpacer()
    {
        _data[0].cellSize = Mathf.Max(scroller.ScrollRectSize - _totalCellSize, 0);
        scroller.ReloadData(1.0f);
    }
    #region EnhancedScroller Handlers

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return _data[dataIndex].cellSize;
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        BaseCellView cellView;

        if (dataIndex == 0)
        {
            cellView = scroller.GetCellView(spacerCellViewPrefab) as BaseCellView;
            cellView.name = spacerCellViewPrefab.name;
            cellView.SetData(_data[dataIndex]);
        }
        else
        {
            if (_data[dataIndex].cellType == BaseDataItem.CellType.MyText)
            {
                cellView = scroller.GetCellView(myTextCellViewPrefab) as BaseCellView;
            }
            else
            {
                cellView = scroller.GetCellView(otherTextCellViewPrefab) as BaseCellView;
            }
            cellView.name = string.Format("{0} {1}", cellView.name, dataIndex);
            cellView.SetData(_data[dataIndex]);
        }


        return cellView;
    }

    #endregion
}
