using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public void Initialized(Vector3 posTarget, float time, Action onDone)
    {
        DOTween.Kill(this);
        transform.DOMove(posTarget, time)
            .OnComplete(() => { onDone?.Invoke(); })
            .SetId(this);
    }
}
