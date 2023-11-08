using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.UI.Popup;
using XXX.SO.Event;


namespace XXX.UI.Popup
{
    public partial class PopupManager
    {
        [SerializeField] private PopupReferencer popupWarming;
        [SerializeField] private PopupReferencer popupLog;
        [SerializeField] private PopupReferencer popupLogin;
        [SerializeField] private PopupReferencer popupRegister;
        [SerializeField] private PopupReferencer popupSetting;
        [SerializeField] private PopupReferencer popupCreateRoom;
        [SerializeField] private PopupReferencer popupInfo;
        [SerializeField] private PopupReferencer popupRoom;
        [SerializeField] private PopupReferencer popupVs;
        [SerializeField] private PopupReferencer popupWin;
        [SerializeField] private PopupReferencer popupLose;
        [SerializeField] private PopupReferencer popupLoadingContent;
        [SerializeField] private PopupReferencer popupLoading;
        [SerializeField] private PopupReferencer popupFriend;
        [SerializeField] private PopupReferencer popupShop;
        [SerializeField] private PopupReferencer popupInventory;
        [SerializeField] private PopupReferencer popupAchievement;
        [SerializeField] private PopupReferencer popupRanking;
        [SerializeField] private PopupReferencer popupChatPrivate;



        public void ShowPopupChatPrivate(object data)
        {
            popupChatPrivate.ShowPopup(data);
        }
        public void ShowPopupAchievement(object data)
        {
            popupAchievement.ShowPopup(data);
        }
        public void ShowPopupInventory(object data)
        {
            popupInventory.ShowPopup(data);
        }
        public void ShowPopupShop(object data)
        {
            popupShop.ShowPopup(data);
        }
        public void ShowPopupFriend(object data)
        {
            popupFriend.ShowPopup(data);
        }
        public void ShowPopupSetting(object data)
        {
            popupSetting.ShowPopup(data);
        }
        public void ShowPopupInfo(object data)
        {
            popupInfo.ShowPopup(data);
        }
        public void ShowPopupRoom(object data)
        {
            popupRoom.ShowPopup(data);
        }
        public void ShowPopupCreateRoom(object data)
        {
            popupCreateRoom.ShowPopup(data);
        }
        public void ShowPopupRanking(object data)
        {
            popupRanking.ShowPopup(data);
        }
        public void ShowPopupVs(object data)
        {
            popupVs.ShowPopup(data);
        }
        public void ShowPopupWin(object data)
        {
            popupWin.ShowPopup(data);
        }
        public void ShowPopupLose(object data)
        {
            popupLose.ShowPopup(data);
        }
        public void ShowPopupLoseLoadingContent(object data)
        {
            popupLoadingContent.ShowPopup(data);
        }
        public void ShowPopupWarming(object data)
        {
            popupWarming.ShowPopup(data);
        }
        public void ShowPopupLog(object data)
        {
            popupLog.ShowPopup(data);
        }
        public void ShowPopupLogin(object data)
        {
            popupLogin.ShowPopup(data);
        }
        public void ShowPopupRegister(object data)
        {
            popupRegister.ShowPopup(data);
        }
        public void HidePopupLoseLoadingContent()
        {
            popupLoadingContent.Close();
        }
        public void ShowPopupLoading(object data)
        {
            if (!DataManager.IsAndroid)
            {
                popupLoading.ShowPopup(data);
                popupLoading.SetOrderCanvas(5000);
            }
        }
        public void HidePopupLoading()
        {
            popupLoading.Close();
        }
    }
}