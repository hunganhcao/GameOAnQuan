using UnityEngine;
using XXX.UI.Popup;
using TMPro;
using UnityEngine.UI;
using WorldForKid.Scenes;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace WorldForKid.ConnectSocket {
    public class PopupConnectSocket : BasePopup
    {
        [SerializeField] private ListenMessageSocket<SMessageFirstConnect> l_FirstConnect;
        [SerializeField] private EventName e_ShowPopupMessage;
        [SerializeField] private TMP_InputField inputIPAddress;
        [SerializeField] private TMP_InputField inputPortSocket;
        [SerializeField] private TMP_InputField inputPortAPI;
        [SerializeField] private Button btnConnectSocket;

        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);

            btnConnectSocket.RegisterOnClick(OnClickConnectSocket);
        }

        private void Start()
        {
            Initialized();
            l_FirstConnect.RegisterEvent(EventName.Socket_FirstConnect, HandleConnected);
        }

        private void HandleConnected(SMessageFirstConnect obj)
        {
            SenceManagerCustom.LoadScene(Constain.SN_LOBBY);
        }

        private void OnClickConnectSocket()
        {
            try
            {
                DataManager.IpServer = inputIPAddress.text;
                DataManager.PortAPI = Convert.ToInt32(inputPortAPI.text);
                DataManager.PortSocket = Convert.ToInt32(inputPortSocket.text);
                SceneManager.LoadScene(Constain.SN_LOGIN);
            }
            catch
            {
                PopupManager.Instance.ShowPopupWarming("Nhập không đúng định dạng");
            }
            //var ipAddress = inputIPAddress.text;
            //    var port = Convert.ToInt32(inputPortSocket.text);
            //    btnConnectSocket.interactable = false;
            //    SocketManager.Instance.ConnectToServer(ipAddress, port);
            //    SocketCall.FistConnect(DataManager.currentPlayer.Id);

            //}
            //catch
            //{
            //    this.Notify(e_ShowPopupMessage, "Không kết nối được máy chủ!");
            //    btnConnectSocket.interactable = true;
            //}
        }

    }
}