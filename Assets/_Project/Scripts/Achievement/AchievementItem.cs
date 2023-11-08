using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static APIRequest;

public class AchievementItemData : CustomEnhance.BaseDataItem
{
    public AAchievement data;
}

public class AchievementItem : CustomEnhance.BaseCellView
{
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private TMP_Text txtDescribe;
    [SerializeField] private TMP_Text txtTarget;
    [SerializeField] private TMP_Text txtClaim;
    [SerializeField] private Button btnClaim;


    private AchievementItemData _data;
    public override void SetData(CustomEnhance.BaseDataItem data)
    {
        base.SetData(data);
        _data = data as AchievementItemData;
        if (_data == null) return;

        txtName.text = _data.data.Name;
        txtDescribe.text = _data.data.Description;
        txtTarget.text = string.Format("{0}/{1}", _data.data.CurrentCount, _data.data.MaxCount);

        SetBtnClaim();
    }
    private void SetBtnClaim()
    {
        btnClaim.RegisterOnClick(OnClickClaim);

        if(_data.data.CurrentCount < _data.data.MaxCount)
        {
            btnClaim.interactable = false;
            txtClaim.text = "Chưa xong";
        }
        else if (!_data.data.IsRecevied)
        {
            btnClaim.interactable = true;
            txtClaim.text = "Nhận";
        }
        else
        {
            btnClaim.interactable = false;
            txtClaim.text = "Đã nhận";
        }
    }

    private void OnClickClaim()
    {
        APIRequest.ReceveieAchievement(_data.data.Id, null);
    }
}
