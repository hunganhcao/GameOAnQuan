using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.Attribute;
using XXX.SO.Event;


namespace XXX.UI.Popup
{
    public partial class PopupManager : SingletonDontDestroy<PopupManager>
    {
        private StackPopup _popups;
        protected StackPopup Popups => _popups ?? (_popups = new StackPopup());

        [SerializeField] private Canvas _canvasToShow;
        protected void ShowPopup<T>(T popup, ref IPopup handle, object data) where T : BasePopup
        {
            if (handle != null)
            {
                if (handle.ThisGameObject.activeSelf) return;
                Display(ref handle);
                return;
            }

            handle = Instantiate(popup, _canvasToShow.transform, false);
            Display(ref handle);

            void Display(ref IPopup handle)
            {
                var popup = (T)handle;
                popup.Initialized(data, ActionClose);
                Popups.Show(handle);
            }
            void ActionClose()
            {
                Popups.Close();
            }
        }
        [System.Serializable]
        public class PopupReferencer
        {
            [SerializeField] private BasePopup _popupPrefab;
            private IPopup _popupHandle;

            public void ShowPopup(object data = null)
            {
                Instance.ShowPopup(_popupPrefab, ref _popupHandle, data);
            }
            public void Close()
            {
                if(_popupHandle != null) _popupHandle.Close();
            }
            public void SetOrderCanvas(int order)
            {
                _popupHandle.UpdateSortingOrder(order);
            }
        }
        protected void Start()
        {
            base.Awake();
        }
    }



    public class StackPopup
    {
        private Stack<IPopup> _stacks = new Stack<IPopup>();
        public void Hide()
        {
            var popuphide = _stacks.Pop();
            popuphide.Close();
        }
        public void Show(IPopup uniPopupHandler)
        {
            var lastOrder = 0;
            if (_stacks.Count > 0)
            {
                var top = _stacks.Peek();
                lastOrder = top.Canvas.sortingOrder;
            }

            uniPopupHandler.UpdateSortingOrder(lastOrder + 10);
            _stacks.Push(uniPopupHandler);
            uniPopupHandler.Show(); // show
        }
        public void Close()
        {
            _stacks.Pop();
        }
        public void CloseAll()
        {
            foreach (var i in _stacks)
            {
                i.Close();
            }
            _stacks.Clear();
        }
    }
}