using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private EventName eventName;
    int i = 0;
    private void Start()
    {
        EventManager.Notify(eventName, i);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            i++;
            EventManager.Notify(eventName, i);
        }
    }
}
