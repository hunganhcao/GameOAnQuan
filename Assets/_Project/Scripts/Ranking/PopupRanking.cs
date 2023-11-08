using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PopupRanking : XXX.UI.Popup.BasePopup
{
    [SerializeField] private CustomEnhancedScroller1<RankingItem, SRankingItem> enhanced;
    [SerializeField] private Button btnPiano;
    [SerializeField] private Button btnOAnQuan;
    [SerializeField] private RankingItem itemMine;

    private void Start()
    {
        Initialized();
    }

    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        btnPiano.RegisterOnClick(OnClickPiano);

        btnOAnQuan.RegisterOnClick(OnClickOAnQuan);

        enhanced.Initialized();
        btnPiano.onClick.Invoke();
    }

    private void OnClickOAnQuan()
    {
        ClickButton(ERoomType.OAnQuan);
        SelectOneButton(btnOAnQuan);
    }

    private void OnClickPiano()
    {
        ClickButton(ERoomType.Piano);
        SelectOneButton(btnPiano);
    }
    public void ClickButton(ERoomType type)
    {
        enhanced.LoadData(new List<SRankingItem>());
        APIRequest.GetRanking(type, 0, 10, HandleOnReceiveData);
    }

    private void HandleOnReceiveData(List<SRankingItem> data)
    {
        itemMine.SetData(data[0]);
        data.RemoveAt(0);
        enhanced.LoadData(data);
    }
    private void SelectOneButton(Button btn)
    {
        btnPiano.interactable = true;
        btnOAnQuan.interactable = true;

        btn.interactable = false;
    }
}

