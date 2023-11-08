using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatRoomFillData : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMessgage;
    [SerializeField] private TMP_Text txtTime;
    [SerializeField] private AvatarDisplay avatar;

    private ChatRoomData _data;
    public void SetData(object data)
    {
        _data = data as ChatRoomData;
        if (_data == null) return;

        txtMessgage.text = _data.Message;
        txtTime.text = FormatTime(_data.TimeSend);
        avatar.Initialized(_data.UsernameSender);
    }
    private static string FormatTime(DateTime dateTime)
    {
        TimeSpan timeSince = DateTime.Now.Subtract(dateTime);

        if (timeSince.TotalMinutes < 1)
        {
            return "Vừa xong";
        }
        else if (timeSince.TotalMinutes < 60)
        {
            return $"{timeSince.Minutes} phút trước";
        }
        else if (timeSince.TotalHours < 24)
        {
            return $"{timeSince.Hours} giờ trước";
        }
        else
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
