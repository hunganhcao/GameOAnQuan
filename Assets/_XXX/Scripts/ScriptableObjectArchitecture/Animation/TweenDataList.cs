using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.Attribute;


namespace XXX.SO.Animtion
{
    [System.Serializable]
    [CreateAssetMenu(menuName = SOPathConstant.Animation + "TweenDataList")]
    public class TweenDataList : ScriptableObject
    {
        public List<TweenData> tweenDatas;
        public Sequence GetSequence(Transform model)
        {
            var sequence = DOTween.Sequence();
            sequence.SetAutoKill(false);
            foreach (var tween in tweenDatas)
            {
                if (tween.join)
                {
                    sequence.Join(tween.GetTween(model));
                }
                else
                {
                    sequence.Append(tween.GetTween(model));
                }
            }
            return sequence;
        }
    }

    [System.Serializable]
    public class TweenData
    {
        public enum TweenType { Move, Rotate, Scale, From }
        public TweenType tweenType;
        public bool join = false;
        public float timeDelay;
        public float duration = 1;
        public Ease ease;
        [ShowIf(nameof(tweenType), TweenType.Move)] public Vector3 positionOffset;
        [ShowIf(nameof(tweenType), TweenType.Rotate)] public Vector3 targetRotation;
        [ShowIf(nameof(tweenType), TweenType.From)] public Vector3 positionFrom;
        [ShowIf(nameof(tweenType), TweenType.Scale)] public float targetScale = 1f;

        public Tween GetTween(Transform model)
        {
            Tween tween = null;
            switch (tweenType)
            {
                case TweenType.Move:
                    Vector3 targetPosition = model.position + positionOffset;
                    tween = model.DOMove(targetPosition, duration);
                    break;
                case TweenType.Rotate:
                    tween = model.DORotate(targetRotation, duration);
                    break;
                case TweenType.Scale:
                    tween = model.DOScale(targetScale, duration);
                    break;
                case TweenType.From:
                    var position = model.position;
                    model.position += positionFrom;
                    tween = model.DOMove(position, duration);
                    break;
            }
            return tween.SetEase(ease).SetDelay(timeDelay);
        }
    }
}

