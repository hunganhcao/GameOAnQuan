using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorldForKid.API;
using WorldForKid.ConnectSocket;
using WorldForKid.Scenes;
using XXX.UI.Popup;

namespace WorldForKid.Login
{
    public class LoginPanel : BaseLoginPanel
    {
        private void OnEnable()
        {
            Initialized();
        }
        protected override void OnClickButtonOK()
        {
            base.OnClickButtonOK();
            if (CheckNullInputsAndShowMessage()) return;

            APIRequest.SendLogin(input_Username.text, input_Password.text, HandleOnOK);
        }
        protected override void OnClickButtonCancel()
        {
            base.OnClickButtonCancel();
            Close();
            PopupManager.Instance.ShowPopupRegister(null);
        }
        protected void HandleOnOK(UserDTO user)
        {
            Close();
            DataManager.currentPlayer = user;
            SocketManager.Instance.ConnectToServer(DataManager.IpServer, DataManager.PortSocket);
            SocketCall.FistConnect(DataManager.currentPlayer.DisplayName);
            SceneManager.LoadScene(Constain.SN_LOBBY);
        }
    }
}
