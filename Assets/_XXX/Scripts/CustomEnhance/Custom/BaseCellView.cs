using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomEnhance
{
    [System.Serializable]
    public class BaseCellView : EnhancedScrollerCellView
    {
        public virtual void SetData(BaseDataItem data)
        {
        }
    }
}
