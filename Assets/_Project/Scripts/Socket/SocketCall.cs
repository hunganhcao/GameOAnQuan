using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using WorldForKid.ConnectSocket;

public static class SocketCall
{
    #region First Connect

    internal class CMessageFirstConnect
    {
        public Player player;
    }
    internal class SMessageFirstConnect
    {
        public int IdRoomServer;
    }
    public static void FistConnect(string idMine)
    {
        var data = new CMessageFirstConnect()
        {
            player = new Player()
            {
                Id = idMine,
            }
        };
        SocketManager.Instance.SendToServer(Code.FIRSTCONNECT, data);
    }
    #endregion


    #region Create Room
    public class CMessageCreateRoom
    {
        public ERoomType RoomType;
    }
    public class SRoomStatus
    {
        public int Id;
        public ERoomType Type;
        public List<Player> PlayerList;
        public string IdOwner;

        public Player GetMine()
        {
            return PlayerList.FirstOrDefault(x => DataManager.CheckMine(x.Id));
        }
        public Player GetOther()
        {
            return PlayerList.FirstOrDefault(x => !DataManager.CheckMine(x.Id));
        }
    }
    public class Player
    {
        public string Id;

    }
    public static void CreatRoom(ERoomType gameType)
    {
        var data = new CMessageCreateRoom()
        {
            RoomType = gameType,
        };
        SocketManager.Instance.SendToServer(Code.CREATEROOM, data);
    }
    #endregion

    #region Join room

    internal class CMessageJoinRoom
    {
        public int RoomId;
    }
    public static void JoinRoom(int idRoom)
    {
        var data = new CMessageJoinRoom()
        {
            RoomId = idRoom,
        };
        SocketManager.Instance.SendToServer(Code.JOINROOM, data);
    }
    #endregion

    #region Start Game
    public class CMessageStartGame : DataRoom
    {
    }
    public static void StartGame()
    {
        var data = new CMessageStartGame();
        data.IdRoom = DataManager.idRoomGame;
        SocketManager.Instance.SendToServer(Code.STARTGAME, data);
    }
    public static void HandleStartGame()
    {
    }
    #endregion

    #region Complete One Quest
    public class CMessageCompleteOneQuest : DataRoom
    {
    }
    public class SMessageCompleteOneQuest
    {
        public string IdPlayerCompelete;
        public int score;
    }
    public static void CallCompeteOneQuest()
    {
        var data = new CMessageCompleteOneQuest()
        {
            IdRoom = DataManager.idRoomGame
        };
        SocketManager.Instance.SendToServer(Code.COMPELETEONEQUEST, data);
    }
    #endregion

    #region Fall One Quest
    public class CMessageFallOneQuest : DataRoom
    {

    }
    public class SMessageFallOneQuest
    {
        public string IdPlayerFall;
    }
    public static void CallFallOneQuest()
    {
        var data = new CMessageFallOneQuest()
        {
            IdRoom = DataManager.idRoomGame
        };
        SocketManager.Instance.SendToServer(Code.FALLONEQUEST, data);
    }
    #endregion

    #region Complete All Quest
    internal class CMessageCompleteAllQuest : DataRoom
    {
    }
    public class SMessageCompleteAllQuest
    {
        public int rankIndex;
    }
    public static void CallCompelteAllQuest()
    {
        var data = new CMessageCompleteAllQuest()
        {
            IdRoom = DataManager.idRoomGame
        };
        SocketManager.Instance.SendToServer(Code.COMPELETEALLQUEST, data);
    }
    #endregion
    
    #region Leave Room
    public class CMessageLeaveRoom : DataRoom
    {
    }
    public static void CallLeaveRoom()
    {
        var data = new CMessageLeaveRoom()
        {
            IdRoom = DataManager.idRoomGame
        };
        SocketManager.Instance.SendToServer(Code.LEAVEROOM, data);
    }
    #endregion

    #region Match game
    public class CMessageMatchGame : DataRoom
    {
    }
    public static void CallMatchRoom()
    {
        var data = new CMessageMatchGame()
        {
            IdRoom = DataManager.idRoomGame
        };
        SocketManager.Instance.SendToServer(Code.MATCHGAME, data);
    }
    #endregion

    #region Send ChooseTile O An Quan
    public class CMessageChooseTile : DataRoom
    {
        public int Index;
        public bool IsLeft;
        public string Username;
    }
    public class SMessageChooseTile : DataRoom
    {
        public int Index;
        public bool IsLeft;
        public string Username;
    }
    public static void CallChooseTile(int indexTile, bool isLeft)
    {
        var data = new CMessageChooseTile()
        {
            IdRoom = DataManager.idRoomGame,
            Username = DataManager.currentPlayer.Username,
            Index = indexTile,
            IsLeft = isLeft
        };
        SocketManager.Instance.SendToServer(Code.CHOOSETILE, data);
    }
    #endregion

    #region ChatRoom
    public class SChatRoom : DataRoom
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime TimeSend { get; set; }
    }
    public class CChatRoom : DataRoom
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime TimeSend { get; set; }
    }
    public static void SendChatRoom(int roomId, string message)
    {
        var data = new CChatRoom()
        {
            IdRoom = roomId,
            Message = message,
            TimeSend = DateTime.Now,
            Sender = DataManager.currentPlayer.Username
        };
        SocketManager.Instance.SendToServer(Code.CHATROOM, data);
    }
    #endregion

    #region Chat Private
    public class ChatPrivateDTO
    {
        public string Sender { get; set; }
        public string Message;
        public string Recevier;
        public DateTime TimeSend { get; set; }
    }
    public static void SendChatPrivate(string otherUsername, string message)
    {
        var data = new ChatPrivateDTO()
        {
            Message = message,
            TimeSend = DateTime.Now,
            Sender = DataManager.currentPlayer.Username,
            Recevier = otherUsername
        };
        SocketManager.Instance.SendToServer(Code.CHATPRIVATE, data);
    }
    #endregion
}
