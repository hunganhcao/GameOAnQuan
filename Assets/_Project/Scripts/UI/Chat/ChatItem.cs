using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatItem : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMessage;
    public void Initialized(string msg)
    {
        txtMessage.text = msg;
    }
}
