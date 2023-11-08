using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RiddleItem : MonoBehaviour
{
    [SerializeField] TMP_Text txtQuest;
    [SerializeField] TMP_Text txtAnswerA;
    [SerializeField] TMP_Text txtAnswerB;
    [SerializeField] TMP_Text txtAnswerC;
    [SerializeField] TMP_Text txtAnswerD;
    [SerializeField] List<ButtonSelectable> btns;

    private SingleSelection _singleSelectionAnswer;
    private void Start()
    {
        _singleSelectionAnswer = new SingleSelection();
        foreach (var btn in btns)
        {
            _singleSelectionAnswer.Add(btn.SelectableElement);
        }

    }
    public void Initialized(Quest data)
    {
        _singleSelectionAnswer.DeselectAll();
        AllButton(true);

        txtQuest.text = data.Question;
        txtAnswerA.text = data.AnswerA;
        txtAnswerB.text = data.AnswerB;
        txtAnswerC.text = data.AnswerC;
        txtAnswerD.text = data.AnswerD;
        for (int i = 0; i < btns.Count; i++)
        {
            var tmp = i;
            btns[i].RegistClickAllwaySelect(() => 
            {
                GameRiddle.ChooseAnswer?.Invoke(tmp);
                AllButton(false);
            });
        }
    }

    private void AllButton(bool isInteractable)
    {
        foreach (var btn in btns)
        {
            btn.Btn.interactable = isInteractable;
        }
    }
}
