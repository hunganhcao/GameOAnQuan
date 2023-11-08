using Assets._Project.Scripts.ChatRoom;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WorldForKid.Scenes;
using XXX.UI.Popup;
using static SocketCall;

public class PopupRoom : XXX.UI.Popup.BasePopup
{
    public class InitData
    {
        public int IdRoom;
        public List<Player> PlayerList;
        public string IdOwner;
    }

    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_RecevieJoinRoom;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_LeaveRoom;
    [SerializeField] private ListenMessageSocket<SRoomStatus> l_RecevieStartGame;
    [SerializeField] private ListenMessageSocket<List<PianoQuest>> l_RecevieDataGamePiano;
    [SerializeField] private ListenMessageSocket<int> l_RecevieStartGameOAnQuan;
    [SerializeField] private Button btn_Start;
    [SerializeField] private Button btn_Ready;
    [SerializeField] private TMP_Text txtTitleRoom;
    [SerializeField] private string formatTitleRoom;
    [SerializeField] private AvatarDisplay avatarMine;
    [SerializeField] private AvatarDisplay avatarOther;
    [SerializeField] private PopupChatRoom popupChatRoom;

    private InitData _data;
    private Player _mine;
    private Player _other;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        _data = data as InitData;
        if (_data == null) { return; }
        l_RecevieJoinRoom.RegisterEvent(EventName.Socket_JoinRoom, ReInitRoom);
        l_LeaveRoom.RegisterEvent(EventName.Socket_LeaveRoom, ReInitRoom);
        l_RecevieDataGamePiano.RegisterEvent(EventName.Socket_DataGamePiano, HandleDataGamePiano);
        l_RecevieStartGameOAnQuan.RegisterEvent(EventName.Socket_StartGameOAnQuan, StartGameOAnQuan);
        l_RecevieStartGame.RegisterEvent(EventName.Socket_StartGame, HandleStartGame);

        txtTitleRoom.text = string.Format(formatTitleRoom, _data.IdRoom);

        btn_Start.RegisterOnClick(OnClickStart);
        btn_Ready.RegisterOnClick(OnClickReady);

        SetAvatar();
        SetButton();

        var dataInitChatRoom = new PopupChatRoom.DataInit()
        {
            idRoom = DataManager.roomWaitStatus.Id,
        };
        popupChatRoom.Initialized(dataInitChatRoom);
    }

    private void StartGameOAnQuan(int obj)
    {
        Close();
        SceneManager.LoadScene(Constain.SN_OAnQuan);
    }

    private void HandleStartGame(SRoomStatus data)
    {
        //DataManager.roomWaitStatus = data;
        var initData = new PopupVs.InitData()
        {
            timeWaitClose = 3,
            mine = data.PlayerList.FirstOrDefault(x => DataManager.CheckMine(x.Id)),
            other = data.PlayerList.FirstOrDefault(x => !DataManager.CheckMine(x.Id)),
            //onDone = PianoGameManager.Instance.StartGame
        };
        PopupManager.Instance.HidePopupLoseLoadingContent();
        PopupManager.Instance.ShowPopupVs(initData);
    }

    private void HandleDataGamePiano(List<PianoQuest> data)
    {
        PianoGameManager.pianoQuests = data;
        SenceManagerCustom.LoadScene(Constain.SN_PianoGame);
        Close();
    }

    private void OnClickReady()
    {

    }

    private void OnClickStart()
    {
        if(DataManager.roomWaitStatus.PlayerList.Count == 2)
        {
            SocketCall.StartGame();
        }
        else
        {
            SocketCall.CallMatchRoom();
            PopupManager.Instance.ShowPopupLoseLoadingContent("Chờ đối thủ");
        }
    }

    private void ReInitRoom(SRoomStatus data)
    {
        var initData = new PopupRoom.InitData()
        {
            IdRoom = data.Id,
            PlayerList = data.PlayerList,
            IdOwner = data.IdOwner,
        };
        Initialized(initData);
    }
    private void SetAvatar()
    {
        _mine = null;
        _other = null;
        foreach (var player in _data.PlayerList)
        {
            if (DataManager.CheckMine(player.Id))
            {
                _mine = player;
            }
            else
            {
                _other = player;
            }
        }
        avatarMine.Initialized(true, _mine.Id);
        if (_other == null)
        {
            avatarOther.gameObject.SetActive(false);
        }
        else
        {
            avatarOther.gameObject.SetActive(true);
            avatarOther.Initialized(false, _other.Id);
        }
    }
    private void SetButton()
    {
        bool isOwner = DataManager.CheckMine(_data.IdOwner);
        btn_Start.gameObject.SetActive(isOwner);
        btn_Ready.gameObject.SetActive(!isOwner);

    }
    protected override void OnClickButtonClose()
    {
        base.OnClickButtonClose();
        SocketCall.CallLeaveRoom();
    }
}
