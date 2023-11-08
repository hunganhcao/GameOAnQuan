using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XXX.SO.Event;
using XXX.UI.Popup;

public class PopupCreateRoom : XXX.UI.Popup.BasePopup
{
    public class InitData
    {
        public ERoomType gameType;
    }
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_RecevieCreateRoom;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_RecevieJoinRoom;
    [SerializeField] private Button btn_CreateEmptyRoom;
    [SerializeField] private Button btn_JoinRoom;
    [SerializeField] private TMP_InputField ipfIdRoom;
    private ERoomType _gameType;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        var initData = data as InitData;
        if (initData == null) { return; }
        _gameType = initData.gameType;
        btn_CreateEmptyRoom.RegisterOnClick(OnClickButtonCreateEmptyRoom);
        btn_JoinRoom.RegisterOnClick(OnClickButtonJoinRoom);
        l_RecevieCreateRoom.RegisterEvent(EventName.Socket_CreateRoom, CreateRoom);
        l_RecevieJoinRoom.RegisterEvent(EventName.Socket_JoinRoom, CreateRoom);
    }

    private void CreateRoom(SocketCall.SRoomStatus data)
    {
        var initData = new PopupRoom.InitData() 
        {
            IdRoom = data.Id,
            PlayerList = data.PlayerList,
            IdOwner = data.IdOwner,
        };

        PopupManager.Instance.ShowPopupRoom(initData);
        Close();
    }

    private void OnClickButtonJoinRoom()
    {
        int idRoom = Convert.ToInt32(ipfIdRoom.text);

        SocketCall.JoinRoom(idRoom);
    }

    private void OnClickButtonCreateEmptyRoom()
    {
        SocketCall.CreatRoom(_gameType);
    }
}                                                                                          
