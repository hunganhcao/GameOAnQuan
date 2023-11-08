using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldForKid.ConnectSocket;

public static class ExcuteMessage
{
    public static void Excute(SocketManager client, BaseMessage msg)
    {
        Debug.Log("Received Socket: " + msg.code + ":" + msg.message);

        switch (msg.code)
        {
            case Code.FIRSTCONNECT:
                EventManager.Notify(EventName.Socket_FirstConnect, msg.message);
                break;
            case Code.CREATEROOM:
                EventManager.Notify(EventName.Socket_CreateRoom, msg.message);
                break;
            case Code.JOINROOM:
                EventManager.Notify(EventName.Socket_JoinRoom, msg.message);
                break;
            case Code.STARTGAME:
                EventManager.Notify(EventName.Socket_StartGame, msg.message);
                break;
            case Code.COMPELETEONEQUEST:
                EventManager.Notify(EventName.Socket_CompleteOneQuest, msg.message);
                break;
            case Code.COMPELETEALLQUEST:
                EventManager.Notify(EventName.Socket_CompleteAllQuest, msg.message);
                break;
            case Code.DATAGAMEPIANO:
                EventManager.Notify(EventName.Socket_DataGamePiano, msg.message);
                break;
            case Code.STARTGAMEOANQUAN:
                EventManager.Notify(EventName.Socket_StartGameOAnQuan, msg.message);
                break;
            case Code.CHOOSETILE:
                EventManager.Notify(EventName.Socket_ChooseTile, msg.message);
                break;
            case Code.CHATROOM:
                EventManager.Notify(EventName.Socket_ChatRoom, msg.message);
                break;
            case Code.CHATPRIVATE:
                EventManager.Notify(EventName.Socket_ChatPrivate, msg.message);
                break;
        }
    }
}
