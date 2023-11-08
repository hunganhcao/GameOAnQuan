using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupWinLose : XXX.UI.Popup.BasePopup
{
    [SerializeField] private bool isWin;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        if(isWin)
        {
            SoundManager.Instance.PlayEffect(ESoundType.Win);
            APIRequest.CompleteOneAchievement((APIRequest.EAchievementType)DataManager.roomGameStatus.Type, null);
            APIRequest.WinOneGame(DataManager.roomGameStatus.Type, null);
        }
        else
        {
            SoundManager.Instance.PlayEffect(ESoundType.Los);
            APIRequest.LoseOneGame(DataManager.roomGameStatus.Type, null);
        }
    }
    private void HandleEndGameRequest(int i)
    {
        APIRequest.GetMe((x) =>
        {
            EventManager.Notify(EventName.UpdateUser, x);
        });
    }
    protected override void OnClickButtonClose()
    {
        base.OnClickButtonClose();
        SceneManager.LoadScene(Constain.SN_LOBBY);
    }
}
