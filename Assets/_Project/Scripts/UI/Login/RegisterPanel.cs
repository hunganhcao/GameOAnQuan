using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using XXX.SO.Event;
using WorldForKid.API;
using XXX.UI.Popup;
using System;

namespace WorldForKid.Login
{
    public class RegisterPanel : BaseLoginPanel
    {
        [Header("RegisterPanel")]
        [SerializeField] private TMP_InputField input_ComfrimPassword;

        private void OnEnable()
        {
            Initialized();
        }
        protected override void OnClickButtonCancel()
        {
            base.OnClickButtonCancel();
            Close();
            PopupManager.Instance.ShowPopupLogin(null);
        }
        protected override void OnClickButtonOK()
        {
            base.OnClickButtonOK();
            if (CheckNullInputsAndShowMessage()) return;
            if (!CheckComfrimPasswordAndShowMessge()) return;

            APIRequest.SendRegister(input_Username.text, input_Password.text, HandleOnOK);
        }

        private void HandleOnOK(UserDTO respone)
        {
            base.HandleOnOK();
            PopupManager.Instance.ShowPopupLogin(null);
            PopupManager.Instance.ShowPopupLog("Bạn đăng kí thành công");
            Close();
        }

        private bool CheckComfrimPasswordAndShowMessge()
        {
            if (!input_ComfrimPassword.text.Equals(input_Password.text))
            {
                ShowMessage("Xác nhận mật khẩu sai");
                return false;
            }
            ShowMessage("");
            return true;
        }
        protected override void ClearAllInputs()
        {
            base.ClearAllInputs();
            input_ComfrimPassword.text = "";
        }
    }
}
