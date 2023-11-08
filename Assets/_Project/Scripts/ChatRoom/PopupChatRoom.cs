using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SocketCall;

namespace Assets._Project.Scripts.ChatRoom
{
    public class PopupChatRoom : XXX.UI.Popup.BasePopup
    {
        public class DataInit
        {
            public int idRoom;
        }

        [SerializeField] private CustomEnhance.EnhanceSrollHeigh enhanced;
        [SerializeField] private ListenMessageSocket<SChatRoom> l_ChatRoom;

        [SerializeField] private Button btnSingleMessage;
        [SerializeField] private Button btnCloseContentPanel;
        [SerializeField] private TMP_Text txtSingleMessgae;
        [SerializeField] private Button btnSend;
        [SerializeField] private GameObject objContentPanel;
        [SerializeField] private TMP_InputField ipfChatContent;


        private DataInit _data;
        private List<ChatRoomData> _chats;
        private bool _isShowContent = false;

        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);
            _data = data as DataInit;
            if (_data == null) return;

            _chats = new List<ChatRoomData>();

            btnSingleMessage.RegisterOnClick(OnClickBtnSingleMessgae);
            btnCloseContentPanel.RegisterOnClick(OnClickBtnCloseContentPanel);

            btnSend.RegisterOnClick(OnClickSend);

            txtSingleMessgae.text = "Chào mừng bạn!"; ;
            ShowContentPanel(false);

            l_ChatRoom.RegisterEvent(EventName.Socket_ChatRoom , HandleChatRoom);
        }

        private void OnClickSend()
        {
            SocketCall.SendChatRoom(_data.idRoom, ipfChatContent.text);
            ipfChatContent.text = string.Empty;
        }
        private void OnClickBtnCloseContentPanel()
        {
            ShowContentPanel(false);
        }
        private void OnClickBtnSingleMessgae()
        {
            ShowContentPanel(true);
        }
        private void ShowContentPanel(bool isShow)
        {
            _isShowContent = isShow;
            objContentPanel.SetActive(isShow);
            btnSingleMessage.gameObject.SetActive(!isShow);
            txtSingleMessgae.gameObject.SetActive(!isShow);
            if(isShow)
            {
                enhanced.Clear();
                foreach(var item in _chats)
                {
                    enhanced.AddNewRow(item, true);
                }
            }
        }
        private void HandleChatRoom(SChatRoom data)
        {
            if (data.IdRoom != _data.idRoom) return;
            var chat = new ChatRoomData();
            chat.Message = data.Message;
            chat.UsernameSender = data.Sender;
            chat.TimeSend = data.TimeSend;
            chat.cellSize = 218;
            _chats.Add(chat);
            DataManager.GetUserIngame(chat.UsernameSender, (x) =>
            {
                txtSingleMessgae.text = string.Format("<color=>{0}</color>: {1}", x.DisplayName, chat.Message);
            });
            if(_isShowContent)
                enhanced.AddNewRow(chat, true);
        }
    }
}
