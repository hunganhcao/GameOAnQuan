using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;
using System;

namespace MathPiano
{
    public class Question : MonoBehaviour
    {
        [SerializeField] private Image imgBg;
        [SerializeField] private TMP_Text txtQuest;
        [SerializeField] private float timeToTarget;

        private Sequence _moveSequence;
        private Action<Question> _onDisableQuestItem;
        private PianoQuest _question;
        public PianoQuest ThisQuestion => _question;
        public void Initialized(Transform spawnPoint, PianoQuest question, Action<Question> onDisableQuestItem)
        {
            imgBg.color = Color.white;

            transform.position = spawnPoint.position;
            _question = question;
            this._onDisableQuestItem = onDisableQuestItem;
            SetTextQuest();
            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOLocalMoveY(spawnPoint.localPosition.y -1000, timeToTarget).
                SetEase(Ease.Linear))
                .OnComplete(HandleMoveToTarget);
        }


        private void SetTextQuest()
        {
            var operatorChar = "";
            switch (_question.t)
            {
                case ETypeOperator.MULTIPLICATION:
                    operatorChar = "x"; break;
                case ETypeOperator.SUMMARTION:
                    operatorChar = "+"; break;
                case ETypeOperator.SUBTRACTION:
                    operatorChar = "-"; break;
            }
            txtQuest.text = _question.a + operatorChar + _question.b;
        }
        public void WrongAnswer()
        {
            imgBg.color = Color.red;
        }
        public void RightAnswer()
        {
            imgBg.color = Color.green;
        }
        public void CurrentAnswer()
        {
            imgBg.color = Color.cyan;
        }
        private void HandleMoveToTarget()
        {
            _onDisableQuestItem?.Invoke(this);
        }
        public void StopMove()
        {
            _moveSequence.Kill();
        }
    }
}