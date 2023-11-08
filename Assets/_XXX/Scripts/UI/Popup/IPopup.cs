using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XXX.UI.Popup
{
    public interface IPopup
    {
        GameObject ThisGameObject { get; }
        Canvas Canvas { get; }
        void Show();
        void Close();
        void UpdateSortingOrder(int sortingOrder);
        void Initialized(object data = null, Action actionClose = null);
    }
}
