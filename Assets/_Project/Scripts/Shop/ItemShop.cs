using Assets._Project.Scripts.Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XXX.UI.Popup;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private SOItemCollect soAvatar;
    [SerializeField] private Image avatar;
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private Button btnBuy;
    [SerializeField] private TMP_Text txtBtn;
    private AItem _data;
    public void SetData(object data)
    {
        _data = data as AItem;
        if (_data == null) return;

        if (PopupShop.Mode == PopupShop.TpyeShop.Shop)
        {
            btnBuy.RegisterOnClick(OnClickBuy);
        }
        else
        {
            btnBuy.RegisterOnClick(OnClickUse);
        }

        avatar.sprite = soAvatar.Get(_data.Type);
        txtName.text = _data.Name;

        SetTxtButton();
    }

    private void SetTxtButton()
    {
        if(PopupShop.Mode == PopupShop.TpyeShop.Inventory)
        {
            txtBtn.text = "Mặc";
            return;
        }
        if (_data.Count > 0)
        {
            DisplayBounght();
        }
        else
        {
            DisplayCanBuy();
        }
    }

    private void OnClickUse()
    {
        APIRequest.UseAvatar(_data.Type, HandleUseAvatar);
    }

    private void HandleUseAvatar(UserDTO obj)
    {
        DataManager.GetMe();
    }

    private void OnClickBuy()
    {
        if (DataManager.currentPlayer.Coin < _data.Cost)
        {
            string msg = string.Format("Bạn không có đủ {0} để mua vật phẩm này!", _data.Cost);
            PopupManager.Instance.ShowPopupWarming(msg);
        }
        else
        {
            APIRequest.BuyItem(_data.Id, HandleBuyItem);
        }
    }
    private void HandleBuyItem(int data)
    {
        btnBuy.interactable = false;
        txtBtn.text = "Đã nhận";
        DataManager.GetMe();
    }
    private void DisplayCanBuy()
    {
        btnBuy.interactable = true;
        txtBtn.text = _data.Cost.ToString();
    }
    private void DisplayBounght()
    {
        btnBuy.interactable = false;
        txtBtn.text = "Đã nhận";
    }
}
