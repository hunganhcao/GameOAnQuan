using MathPiano;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.UI.Popup;
public enum EMathPianoGameType
{
    Summation,
    Multiplication,
    Typing
}
public class PianoGameManager : Singleton<PianoGameManager>
{
    public static List<PianoQuest> pianoQuests;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SoundManager.Instance.PlayMusic(ESoundType.Bg_Game);
    }


    [SerializeField] private EventName e_OnUpdateNewResult;
    [SerializeField] private EventName e_OnClickButtonAnswer;
    [SerializeField] private EventName e_SetAllowClickAnswerButton;
    [SerializeField] private BasePopup popupTapToContinute;

    private int so_Score;

    [SerializeField] private List<Transform> tran_Spawns;
    [SerializeField] private SamplePool<Question> pool_Quest;
    private float timeToInitNextQuest = 2f;
    private Queue<Question> questNotAnswer;
    private void StopAllQuestItem()
    {
        foreach (var item in pool_Quest.ExternalPool)
        {
            item.StopMove();
        }
    }
    private Question _currentQuest;
    private Question CurrentQuest
    {
        get => _currentQuest;
        set
        {
            _currentQuest = value;
            if (value != null)
            {
                value.CurrentAnswer();
                this.Notify(e_OnUpdateNewResult, value.ThisQuestion);
                this.Notify(e_SetAllowClickAnswerButton, true);
                return;
            }
            this.Notify(e_SetAllowClickAnswerButton, false);
        }
    }
    private void Start()
    {
        pool_Quest.Prepare(0);
        questNotAnswer = new Queue<Question>();
        var data = new PopupTapToContinute.InitData()
        {
            tapToContinute = StartGame
        };
        popupTapToContinute.Initialized(data);
    }
    private void OnEnable()
    {
        EventManager.AddEvent(e_OnClickButtonAnswer, HandleOnRightOneQuest);
    }
    private void OnDisable()
    {
        EventManager.RemoveEvent(e_OnClickButtonAnswer, HandleOnRightOneQuest);
    }


    private void HandleOnRightOneQuest(object data)
    {
        var isRight = (bool)data;
        if (isRight)
        {
            CurrentQuest.RightAnswer();
            so_Score++;
            SocketCall.CallCompeteOneQuest();
        }
        else
        {
            CurrentQuest.WrongAnswer();
            SocketCall.CallFallOneQuest();
        }
        StartCoroutine(IESetNextForCurrentQuest());
    }
    private void HandleOnDisableQuestItem(Question quest)
    {
        quest.StopMove();
        pool_Quest.Return(quest);
        if (quest.Equals(CurrentQuest))
        {
            // đoạn này chạy 2 coroutine giống nhau
            StartCoroutine(IESetNextForCurrentQuest());
        }
    }
    public void StartGame()
    {
        so_Score = 0;
        pool_Quest.ExternalPool.ForEach(x => x.StopMove());
        pool_Quest.ReturnAll();
        questNotAnswer.Clear();

        StopAllCoroutines();

        StartCoroutine(IEWaitAndSpawn());
        StartCoroutine(IESetNextForCurrentQuest());
    }
    private IEnumerator IEWaitAndSpawn()
    {
        yield return new WaitForSeconds(timeToInitNextQuest);
        foreach (var quest in pianoQuests)
        {
            SpawnOneQuest(quest);
            yield return new WaitForSeconds(timeToInitNextQuest);
        }
        pianoQuests.Clear();

        while (true) 
        {
            yield return null;
            if(questNotAnswer.Count == 0 && CurrentQuest == null)
            {
                SocketCall.CallCompelteAllQuest();
                break;
            }
        }
    }
    private void SpawnOneQuest(PianoQuest question)
    {
        var quest = pool_Quest.Get();
        var randPos = tran_Spawns[question.p];
        quest.Initialized(randPos, question, HandleOnDisableQuestItem);
        questNotAnswer.Enqueue(quest);
    }
    private IEnumerator IESetNextForCurrentQuest()
    {
        while (true)
        {
            if (questNotAnswer.Count == 0)
            {
                CurrentQuest = null;
                yield return null;
            }
            else
            {
                CurrentQuest = questNotAnswer.Dequeue();
                break;
            }
        }
    }

}
