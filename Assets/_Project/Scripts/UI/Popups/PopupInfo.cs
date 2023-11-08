using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupInfo : XXX.UI.Popup.BasePopup
{
    [SerializeField] private AvatarDisplay avatarDisplay;

    [Header("Toán học")]
    [SerializeField] private TMP_Text txtPianoVictory;
    [SerializeField] private TMP_Text txtPianoDraw;
    [SerializeField] private TMP_Text txtPianoDefeat;

    [Header("Ô ăn quan")]
    [SerializeField] private TMP_Text txtOAnQuanVictory;
    [SerializeField] private TMP_Text txtOAnQuanDraw;
    [SerializeField] private TMP_Text txtOAnQuanDefeat;

    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);
        avatarDisplay.Initialized(true, "");

        APIRequest.GetRankingForUser(ERoomType.Piano, (x) =>
        {
            txtPianoVictory.text = x.NumberVictory.ToString();
            txtPianoDraw.text = x.NumberDraw.ToString();
            txtPianoDefeat.text = x.NumberDefeat.ToString();
        });
        APIRequest.GetRankingForUser(ERoomType.OAnQuan, (x) =>
        {
            txtOAnQuanVictory.text = x.NumberVictory.ToString();
            txtOAnQuanDraw.text = x.NumberDraw.ToString();
            txtOAnQuanDefeat.text = x.NumberDefeat.ToString();
        });
    }
}
