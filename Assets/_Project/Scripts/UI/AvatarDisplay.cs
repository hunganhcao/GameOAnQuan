using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XXX.Attribute;

public class AvatarDisplay : MonoBehaviour
{
    [SerializeField] private SOItemCollect so_Avatars;
    [SerializeField] private bool hasTxtName = false;
    [ShowIf(nameof(hasTxtName), true)]
    [SerializeField] private TMP_Text txtName;

    [SerializeField] private bool hasTxtCoin = false;
    [ShowIf(nameof(hasTxtCoin), true)]
    [SerializeField] private TMP_Text txtCoin;

    [SerializeField] private bool hasTxtExp = false;
    [ShowIf(nameof(hasTxtExp), true)]
    [SerializeField] private TMP_Text txtExp;

    [SerializeField] private bool hasTxtLevel = false;
    [ShowIf(nameof(hasTxtLevel), true)]
    [SerializeField] private TMP_Text txtLevel;

    [SerializeField] private bool hasIpfName = false;
    [ShowIf(nameof(hasIpfName), true)]
    [SerializeField] private TMP_InputField ipfName;

    [SerializeField] private bool hasAvatar = false;
    [ShowIf(nameof(hasAvatar), true)]
    [SerializeField] private Image imgAvatar;

    [SerializeField] private bool hasTxtExpRatio = false;
    [ShowIf(nameof(hasTxtExpRatio), true)]
    [SerializeField] private TMP_Text txtExpRatio;

    [SerializeField] private bool hasSliderExpRatio = false;
    [ShowIf(nameof(hasSliderExpRatio), true)]
    [SerializeField] private Slider sliderExp;

    [SerializeField] private bool hasTxtId = false;
    [ShowIf(nameof(hasTxtId), true)]
    [SerializeField] private TMP_Text txtId;

    [SerializeField] private bool hasImgFillExp = false;
    [ShowIf(nameof(hasImgFillExp), true)]
    [SerializeField] private Image imgFillExp;

    private void Awake()
    {
        EventManager.AddEvent(EventName.UpdateUser, UpdateUser);
    }
    private void OnDestroy()
    {
        EventManager.RemoveEvent(EventName.UpdateUser, UpdateUser);
    }
    public void Initialized(bool isMine, string name)
    {
        if (isMine)
        {
            Initialized(DataManager.currentPlayer.Username);
            return;
        }

        Initialized(name);
    }

    private void UpdateUser(object data)
    {
        var user = data as UserDTO;
        //for (int i = 0; i < DataManager.UsersInGame.Count; i++)
        //{
        //    if (DataManager.UsersInGame[i].Id.Equals(user.Id))
        //    {
        //        DataManager.UsersInGame[i] = user;
        //    }
        //}
        Initialized(user.Username);
    }

    public void Initialized(string username)
    {
        DataManager.GetUserIngame(username, (x) =>
        {
            if (hasTxtName) txtName.text = x.DisplayName;
            if (hasIpfName) ipfName.text = x.DisplayName;
            if (hasAvatar) imgAvatar.sprite = so_Avatars.Get((TypeItem)x.IndexAvatar);
            if (hasTxtCoin) txtCoin.text = x.Coin.ToString();
            if (txtLevel) txtLevel.text = x.GetLevel().ToString();
            if (hasTxtExp) txtExp.text = x.Exp.ToString();
            if (hasTxtExpRatio) txtExpRatio.text = x.Exp + "/" + x.GetTargetExp();
            if (hasSliderExpRatio) sliderExp.value = (float)x.Exp/x.GetTargetExp();
            if (hasTxtId) txtId.text = x.Id;
            if (hasImgFillExp) imgFillExp.fillAmount = (float)x.Exp / x.GetTargetExp();
        });
    }
}
