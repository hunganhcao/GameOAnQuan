using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCellView<TData> : EnhancedScrollerCellView
{
    public virtual void SetData(TData data) {  }
    public virtual void SetData(ref SmallList<TData> data, int startingIndex) {  }
}
