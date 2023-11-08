using EnhancedScrollerDemos.Chat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldForKid.API;
using static SocketCall;

public class PopupChatPrivate : XXX.UI.Popup.BasePopup
{
    public class DataInit
    {
        public string UsernameFriend;
    }

    [SerializeField] private CustomEnhance.EnhanceSrollHeigh enhanced;
    [SerializeField] private ListenMessageSocket<ChatPrivateDTO> l_ChatPrivate;
    [SerializeField] private Button btnSend;
    [SerializeField] private TMP_InputField ipfChatContent;
    [SerializeField] private TMP_Text txtFriendName;


    private DataInit _data;
    private List<ChatRoomData> _chats;

    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        _data = data as DataInit;
        if (_data == null) return;

        _chats = new List<ChatRoomData>();
        btnSend.RegisterOnClick(OnClickSend);
        DataManager.GetUserIngame(_data.UsernameFriend, (x) =>
        {
            txtFriendName.text = x.DisplayName;
        });

        l_ChatPrivate.RegisterEvent(EventName.Socket_ChatPrivate, HandleChatPrivate);
        enhanced.Clear();

        APIRequest.GetAllChatPrivate(_data.UsernameFriend, HandleGetAllChatPrivate);
        ipfChatContent.onValueChanged.RemoveAllListeners();
        ipfChatContent.onValueChanged.AddListener(ChangeInput);
    }

    private void ChangeInput(string msg)
    {
        btnSend.interactable = msg.Length > 2;
    }

    private void HandleGetAllChatPrivate(List<AChatPrivate> data)
    {
        data = data.OrderBy(x => x.TimeSend).ToList();
        foreach(var item in data)
        {
            var chat = new ChatRoomData();
            chat.Message = item.Message;
            chat.UsernameSender = item.UsernameSender; ;
            chat.TimeSend = item.TimeSend;
            chat.cellSize = 218;
            _chats.Add(chat);
            enhanced.AddNewRow(chat, true);
        }
    }

    private void OnClickSend()
    {
        APIRequest.AddChatPrivate(_data.UsernameFriend, ipfChatContent.text, null);
        SocketCall.SendChatPrivate(_data.UsernameFriend, ipfChatContent.text);
        ipfChatContent.text = string.Empty;
    }
    private void HandleChatPrivate(ChatPrivateDTO data)
    {
        //if (data.Sender != _data.UsernameFriend) return;
        var chat = new ChatRoomData();
        chat.Message = data.Message;
        chat.UsernameSender = data.Sender;
        chat.TimeSend = data.TimeSend;
        chat.cellSize = 218;
        _chats.Add(chat);
        enhanced.AddNewRow(chat, true);
    }
}
