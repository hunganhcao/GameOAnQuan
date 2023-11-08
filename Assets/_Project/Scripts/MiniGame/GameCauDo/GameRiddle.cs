using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.SO.Event;


public enum EAnswer
{
    A = 0, B = 1, C = 2, D = 3
}
public class Quest
{
    public string Question;
    public string AnswerA;
    public string AnswerB;
    public string AnswerC;
    public string AnswerD;
    public EAnswer Answer;
}

public class GameRiddle : MonoBehaviour
{
    public static Action<int> ChooseAnswer { get; set; }
    public static Action<Quest> InitQuest { get; set; }

    [SerializeField] private RiddleItem riddleItem;

    private Queue<Quest> _questStack;
    private Quest _currentQuest;
    private int _score;
    private void Start()
    {
        ChooseAnswer += HandleChooseAnswer;

        _questStack = new Queue<Quest>();   
        _questStack.Enqueue(new Quest() { Question = "Cau 1", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.A });
        _questStack.Enqueue(new Quest() { Question = "Cau 2", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.B });
        _questStack.Enqueue(new Quest() { Question = "Cau 3", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.C });
        _questStack.Enqueue(new Quest() { Question = "Cau 4", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.D });
        _questStack.Enqueue(new Quest() { Question = "Cau 5", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.A });
        _questStack.Enqueue(new Quest() { Question = "Cau 6", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.B });
        _questStack.Enqueue(new Quest() { Question = "Cau 7", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.C });
        _questStack.Enqueue(new Quest() { Question = "Cau 8", AnswerA = "A", AnswerB = "B", AnswerC = "C", AnswerD = "D", Answer = EAnswer.D });
        GetQuest();
    }

    private void GetQuest()
    {
        if (_questStack.Count <= 0) return;
        _currentQuest =  _questStack.Dequeue();
        riddleItem.Initialized(_currentQuest);
    }

    private void HandleChooseAnswer(int choose)
    {
        if (choose == (int)_currentQuest.Answer) _score++;
        DOTween.Sequence().AppendInterval(2)
            .OnComplete(() =>
            {
                GetQuest();
            });
    }
}
