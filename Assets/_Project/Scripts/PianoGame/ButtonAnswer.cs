using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MathPiano
{
    public class ButtonAnswer : MonoBehaviour
    {
        [SerializeField] private EventName e_OnClickButtonAnswer;
        [SerializeField] private Button btn;
        [SerializeField] private TMP_Text txt_Answer;
        public bool IsAllowClick
        {
            set
            {
                btn.interactable = value;
            }
        }
        private bool _isRightAnswer;
        private Action _onClick;
        private bool _isClicked = false;

        public void Initialized(string content, bool isRightAnswer, Action onClick)
        {
            _isClicked = false;
            btn.interactable = false;

            txt_Answer.text = content;
            this._isRightAnswer = isRightAnswer;
            this._onClick = onClick;

            btn.RegisterOnClick(OnClickButton);
        }

        private void OnClickButton()
        {
            btn.interactable = false;
            _onClick?.Invoke();
            if (!_isClicked)
            {
                this.Notify(e_OnClickButtonAnswer, _isRightAnswer);
            }
            
        }
    }
}
