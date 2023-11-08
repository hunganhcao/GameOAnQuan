using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemMovement movement;
    [SerializeField] private int score;

    public int Score => score;

    public void MoveToPosition(Vector3 pos, float time, bool hasAnimation, Action onDone)
    {
        if(hasAnimation)
            movement.Initialized(pos, time, onDone);
        else
        {
            transform.position = pos;
            onDone?.Invoke();
        }
    }
}
