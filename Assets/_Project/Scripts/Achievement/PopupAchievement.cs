using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopupAchievement : XXX.UI.Popup.BasePopup
{
    [SerializeField] private CustomEnhance.EnhanceSrollHeigh enhance;
    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        enhance.Clear();
        APIRequest.GetAchievements(HandleGetAchievements);
    }

    private void HandleGetAchievements(List<APIRequest.AAchievement> data)
    {
        foreach(var item in data)
        {
            var dataInit = new AchievementItemData()
            {
                data = item,
                cellSize = 205
            };
            enhance.AddNewRow(dataInit, false);
        }
    }
}
