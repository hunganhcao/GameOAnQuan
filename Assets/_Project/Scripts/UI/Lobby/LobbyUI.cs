using Assets._Project.Scripts.ChatRoom;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XXX.SO.Event;
using XXX.UI.Popup;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private AvatarDisplay avatar;
    [SerializeField] private Button btn_Setting;
    [SerializeField] private Button btn_Avatar;
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnFriend;
    [SerializeField] private Button btnInventory;
    [SerializeField] private Button btnAchievement;
    [SerializeField] private Button btnRanking;
    [SerializeField] private Button btn_Piano;
    [SerializeField] private Button btn_OAnQuan;
    [SerializeField] private PopupChatRoom popupChatRoom;

    private void OnEnable()
    {
        SoundManager.Instance.PlayMusic(ESoundType.Bg_Lobby);
        avatar.Initialized(true, "");
        btn_Avatar.RegisterOnClick(OnClickButtonAvatar);
        btn_Setting.RegisterOnClick(OnClickButtonSetting);
        btnRanking.RegisterOnClick(OnClickRanking);
        btn_Piano.RegisterOnClick(OnClickButtonPiano);
        btn_OAnQuan.RegisterOnClick(OnClickButtonOAnQuan);

        btnShop.RegisterOnClick(OnClickShop);

        btnFriend.RegisterOnClick(OnClickFriend);

        btnInventory.RegisterOnClick(OnClickInventory);

        btnAchievement.RegisterOnClick(OnClickAchievement);

        var chatRoom = new PopupChatRoom.DataInit()
        {
            idRoom = DataManager.idRoomGlobal
        };
        popupChatRoom.Initialized(chatRoom);
    }

    private void OnClickAchievement()
    {
        PopupManager.Instance.ShowPopupAchievement(null);
    }

    private void OnClickInventory()
    {
        PopupManager.Instance.ShowPopupInventory(null);
    }

    private void OnClickFriend()
    {
        PopupManager.Instance.ShowPopupFriend(null);
    }

    private void OnClickShop()
    {
        PopupManager.Instance.ShowPopupShop(null);
    }

    private void OnClickButtonSetting()
    {
        PopupManager.Instance.ShowPopupSetting(null);
    }
    private void OnClickButtonAvatar()
    {
        PopupManager.Instance.ShowPopupInfo(null);
    }
    private void OnClickRanking()
    {
        PopupManager.Instance.ShowPopupRanking(null);
    }
    private void OnClickButtonPiano()
    {
        var data = new PopupCreateRoom.InitData()
        {
            gameType = ERoomType.Piano
        };
        PopupManager.Instance.ShowPopupCreateRoom(data);
    }
    private void OnClickButtonOAnQuan()
    {
        var data = new PopupCreateRoom.InitData()
        {
            gameType = ERoomType.OAnQuan
        };
        PopupManager.Instance.ShowPopupCreateRoom(data);
    }
}
