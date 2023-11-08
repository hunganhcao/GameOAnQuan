using MathPiano;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.UI.Popup;
using static SocketCall;

public class PianoGameUI : MonoBehaviour
{
    [SerializeField] private EventName e_OnUpdateNewResult;
    [SerializeField] private EventName e_SetAllowClickAnswerButton;
    [SerializeField] private ListenMessageSocket<SMessageCompleteOneQuest> l_CompleteOneQuest;
    [SerializeField] private ListenMessageSocket<SMessageCompleteAllQuest> l_CompleteAllQuest;

    [SerializeField] private PlayerGame playerMine;
    [SerializeField] private PlayerGame playerOther;


    [SerializeField] private List<ButtonAnswer> btnsAnswer;
    public bool IsAllowClickButtons
    {
        set
        {
            foreach (var btn in btnsAnswer)
            {
                btn.IsAllowClick = value;
            }
        }
    }
    private void OnEnable()
    {
        EventManager.AddEvent(e_SetAllowClickAnswerButton, HandleSetAllowClickButtons);
        EventManager.AddEvent(e_OnUpdateNewResult, HandleUpdateAnswer);
        l_CompleteOneQuest.RegisterEvent(EventName.Socket_CompleteOneQuest, HandleCompleteOneQuest);
        l_CompleteAllQuest.RegisterEvent(EventName.Socket_CompleteAllQuest,HandleCompleteAllQuest);


        var mine = DataManager.roomGameStatus.GetMine();
        playerMine.Initialized(true, mine.Id, 0);
        var other = DataManager.roomGameStatus.GetOther();
        playerOther.Initialized(false, other.Id, 0);
    }

    private void HandleCompleteAllQuest(SMessageCompleteAllQuest data)
    {
        if(data.rankIndex == 1)
        {
            PopupManager.Instance.ShowPopupWin(null);
        }
        else
        {
            PopupManager.Instance.ShowPopupLose(null);
        }
    }

    private void HandleCompleteOneQuest(SMessageCompleteOneQuest data)
    {
        bool isMine = DataManager.CheckMine(data.IdPlayerCompelete);
        var prefab = isMine ? playerMine : playerOther;
        prefab.CompleteOneQuest(data.score);
    }

    private void HandleSetAllowClickButtons(object data)
    {
        IsAllowClickButtons = (bool)data;
    }

    private void HandleUpdateAnswer(object data)
    {
        var quest = (PianoQuest)data;
        for(int i = 0; i < btnsAnswer.Count; i++) 
        {
            btnsAnswer[i].Initialized(quest.A[i].ToString(), false, null);
        }
        btnsAnswer[quest.p].Initialized(quest.A[quest.p].ToString(), true, null);
    }
    
}
