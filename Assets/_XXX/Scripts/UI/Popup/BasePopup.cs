using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using XXX.Attribute;
using XXX.SO.Animtion;
using DG.Tweening;


namespace XXX.UI.Popup
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class BasePopup : MonoBehaviour, IPopup
    {
        #region Properties
        [Header("BasePopup")]
        [SerializeField] private Canvas _canvas;

        // action close dùng để đóng popup stack
        private Action _actionClose;


        #endregion

        #region Has Button Close
        [SerializeField] private bool _hasBtnClose;
        [SerializeField] [ShowIf(nameof(_hasBtnClose), true)] private Button _btnClose;
        private void RegiterButtonClose()
        {
            if (!_hasBtnClose) return;
            _btnClose.RegisterOnClick(OnClickButtonClose);
        }
        protected virtual void OnClickButtonClose()
        {
            Close();
        }
        #endregion

        #region Implement interface
        public Canvas Canvas => _canvas;
        public GameObject ThisGameObject => gameObject;
        public virtual void Close()
        {
            ThisGameObject.SetActive(false);
        }
        public virtual void Initialized(object data = null, Action actionClose = null)
        {
            _actionClose = actionClose;
            RegiterButtonClose();
            Show();
        }
        public virtual void Show()
        {
            BeforeShow();
            ThisGameObject.SetActive(true);
            AfterShow();

        }
        public virtual void UpdateSortingOrder(int sortingOrder)
        {
            _canvas.sortingOrder = sortingOrder;
        }
        #endregion

        protected virtual void BeforeShow() { }
        protected virtual void AfterShow()
        {
        }
        protected virtual void BeforeClose()
        {
        }
        protected virtual void AfterClose()
        {
            _actionClose?.Invoke();
            ThisGameObject.SetActive(false);
        }
    }
    [System.Serializable]
    public class AnimationEnable
    {
        [SerializeField] private bool _hasAnimation;
        [SerializeField] [ShowIf(nameof(_hasAnimation), true)] private GameObject _content;
        [SerializeField] [ShowIf(nameof(_hasAnimation), true)] private TweenDataList _tweenDataList;
        private Action _onDone;

        public bool HasAnimation => _hasAnimation;
        public void StartAnimtion()
        {
            if (!_hasAnimation) return;
            var sequence = _tweenDataList.GetSequence(_content.transform)
                .OnComplete(() => { _onDone?.Invoke(); });
        }
        public AnimationEnable OnDone(Action onDone)
        {
            _onDone = onDone;
            return this;
        }
    }
}

