using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SocketCall;

public class PlayerGame : MonoBehaviour
{
    [SerializeField] private AvatarDisplay avatarDisplay;
    [SerializeField] private TMP_Text txtScore;
    public void Initialized(bool isMine, string name, int score)
    {
        avatarDisplay.Initialized(isMine, name);
        txtScore.text = score.ToString();
    }
    public void CompleteOneQuest(int score)
    {
        txtScore.text = score.ToString();
    }
}
