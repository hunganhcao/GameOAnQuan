using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using WorldForKid.ConnectSocket;

public class PopupChat : XXX.UI.Popup.BasePopup
{
    [SerializeField] private SOEventMessage soEvtMessageAll;
    [SerializeField] private SamplePool<ChatItem> poolMineMessage;
    [SerializeField] private SamplePool<ChatItem> poolOtherMessage;
    [SerializeField] private Button btnSingleMessage;
    [SerializeField] private Button btnCloseContentPanel;
    [SerializeField] private TMP_Text txtSingleMessgae;
    [SerializeField] private TMP_InputField inputMessgae;
    [SerializeField] private Button btnSend;
    [SerializeField] private GameObject objContentPanel;


    public override void Initialized(object data = null, Action actionClose = null)
    {
        base.Initialized(data, actionClose);

        btnSingleMessage.RegisterOnClick(OnClickBtnSingleMessgae);
        btnCloseContentPanel.RegisterOnClick(OnClickBtnCloseContentPanel);
        btnSend.RegisterOnClick(OnClickBtnSend);
        txtSingleMessgae.text = "";

        inputMessgae.onValueChanged.AddListener(ChangeInput);
    }
    private void ChangeInput(string msg)
    {
        btnSend.interactable = msg.Length > 2;
    }
    private void OnClickBtnSend()
    {
        var msg = new MessageChatRoom(inputMessgae.text);
        inputMessgae.text = "";
        SocketManager.Instance.SendToServer(msg);
    }
    private void OnEnable()
    {
        poolMineMessage.Prepare(1);
        poolOtherMessage.Prepare(1);
        Initialized();
        soEvtMessageAll.OnEventRaised += HandleReceiveMessageChatAll;
    }
    private void OnDisable()
    {
        soEvtMessageAll.OnEventRaised += HandleReceiveMessageChatAll;
    }

    private void HandleReceiveMessageChatAll(BaseMessage msg)
    {
        var model = MessageChatRoom.FromJson(msg);
        if (model == null) return;
        var contentMessage = model.Message;
        var mineMessgae = poolMineMessage.Get();


        mineMessgae.Initialized(contentMessage);
        txtSingleMessgae.text = contentMessage;
    }

    private void OnClickBtnCloseContentPanel()
    {
        ShowContentPanel(false);
    }
    private void OnClickBtnSingleMessgae()
    {
        ShowContentPanel(true);
    }
    private void ShowContentPanel(bool isShow)
    {
        objContentPanel.SetActive(isShow);
        btnSingleMessage.gameObject.SetActive(!isShow);
        txtSingleMessgae.gameObject.SetActive(!isShow);
    }
}
