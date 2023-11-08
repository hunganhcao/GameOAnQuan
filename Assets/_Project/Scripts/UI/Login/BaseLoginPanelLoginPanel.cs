using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XXX.SO.Event;
using XXX.UI.Popup;

namespace WorldForKid.Login
{
    public class BaseLoginPanel : BasePopup
    {
        [Header("BaseLoginPanel")]
        [SerializeField] protected TMP_InputField input_Username;
        [SerializeField] protected TMP_InputField input_Password;
        [SerializeField] protected Button btn_OK;
        [SerializeField] protected Button btn_Cancel;

        protected virtual bool CheckNullInputs()
        {
            if (string.IsNullOrEmpty(input_Username.text)
                || string.IsNullOrEmpty(input_Password.text))
                return true;
            return false;
        }
        protected void Initialized()
        {
            ClearAllInputs();

            btn_OK.RegisterOnClick(OnClickButtonOK);
            btn_Cancel.RegisterOnClick(OnClickButtonCancel);

            ShowMessage("");
        }
        protected virtual void ClearAllInputs()
        {
            input_Username.text = "";
            input_Password.text = "";
        }
        protected virtual void OnClickButtonOK() { }
        protected virtual void OnClickButtonCancel() { }
        protected void ShowMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            PopupManager.Instance.ShowPopupWarming(msg);
        }
        protected bool CheckNullInputsAndShowMessage()
        {
            if (CheckNullInputs())
            {
                ShowMessage("Các trường không được để trống!");
                return true;
            }
            return false;
        }

        protected ModelLogin GetModelLogin()
        {
            return new ModelLogin()
            {
                Username = input_Username.text,
                Password = input_Password.text,
            };
        }
        protected virtual void HandleOnOK()
        {

        }
    }
}
