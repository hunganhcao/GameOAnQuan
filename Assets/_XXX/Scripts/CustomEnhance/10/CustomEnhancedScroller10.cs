using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CustomEnhancedScroller10<TData, TCellView> : MonoBehaviour, IEnhancedScrollerDelegate
    where TCellView : BaseCellView<TData>
{
    private SmallList<TData> _data;
    [SerializeField] private EnhancedScroller scroller;
    [SerializeField] private EnhancedScrollerCellView cellViewPrefab;

    [SerializeField] private int numberOfCellsPerRow;
    public void Init()
    {
        scroller.Delegate = this;
    }
    public void LoadData(List<TData> data)
    {
        _data = new SmallList<TData>();
        foreach (var dataItem in data)
        {
            _data.Add(dataItem);
        }
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        var cellView = scroller.GetCellView(cellViewPrefab) as TCellView;

        cellView.name = string.Format("{0} {1}-{2}", cellViewPrefab.name, dataIndex * numberOfCellsPerRow, dataIndex * numberOfCellsPerRow + numberOfCellsPerRow - 1);

        cellView.SetData(ref _data, dataIndex * numberOfCellsPerRow);

        return cellView;
    }

    #endregion
}
