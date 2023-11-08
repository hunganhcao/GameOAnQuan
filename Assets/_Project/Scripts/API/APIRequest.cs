using Assets._Project.Scripts.Friend;
using Assets._Project.Scripts.Shop;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldForKid.API;

public static class APIRequest
{
    private static string id => DataManager.currentPlayer.Id;

    #region User
    public static void SendLogin(string username, string password, Action<UserDTO> handle)
    {
        var data = new CLoginDTO()
        {
            username = username,
            password = password
        };
        APIManager.Instance.Call(Constain.ep_Login, Constain.Method.POST, data, handle);
    }
    public static void SendRegister(string username, string password, Action<UserDTO> handle)
    {
        var data = new CLoginDTO()
        {
            username = username,
            password = password
        };
        APIManager.Instance.Call(Constain.ep_Register, Constain.Method.POST, data, handle);
    }
    public static void GetUserByUsername(string username, Action<UserDTO> handle)
    {
        var uri = string.Format(Constain.ep_GetUserByUsername, username);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void UseAvatar(TypeItem type, Action<UserDTO> handle)
    {
        var uri = string.Format(Constain.ep_UseAvatar, id, (int)type);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void GetMe(Action<UserDTO> handle)
    {
        var uri = string.Format(Constain.ep_GetMe, id);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion

    #region Result game
    public static void GetRanking(ERoomType type, int skip, int take, Action<List<SRankingItem>> handle)
    {
        var uri = string.Format(Constain.ep_Ranking, id, (int)type, skip, take);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }

    public class ResultGame
    {
        public string Id { get; set; }
        public ERoomType GameType { get; set; }
        public int NumberVictory { get; set; }
        public int NumberDraw { get; set; }
        public int NumberDefeat { get; set; }
    }
    public static void GetRankingForUser(ERoomType type, Action<ResultGame> handle)
    {
        var uri = string.Format(Constain.ep_GetResultForUser, id, (int)type);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }

    public static void WinOneGame(ERoomType type, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_WinOneGame, id, (int)type);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void LoseOneGame(ERoomType type, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_LoseOneGame, id, (int)type);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion
    public static void SendChangeUsername(string username, Action<UserDTO> handle)
    {
        var uri = string.Format(Constain.ep_GetUserByUsername, username);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }

    #region Friend
    public static void GetFriends(Action<List<AFriendItem>> handle)
    {
        var uri = string.Format(Constain.ep_GetFriend, DataManager.currentPlayer.Id);
        
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void FindFriend(string smallDisplayName, Action<List<AFriendItem>> handle)
    {
        var uri = string.Format(Constain.ep_FindFriend, DataManager.currentPlayer.Id, smallDisplayName);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void AddFriend(string username, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_AddFriend, DataManager.currentPlayer.Id, username);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void RemoveFriend(string username, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_RemoveFriend, DataManager.currentPlayer.Id, username);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion

    #region Request
    public static void GetAllRequest(Action<List<AFriendItem>> handle)
    {
        var uri = string.Format(Constain.ep_GetAllRequests, DataManager.currentPlayer.Id);

        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void AddRequest(string otherUsername, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_AddRequest, DataManager.currentPlayer.Id, otherUsername);

        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void RemoveRequest(string otherUsername, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_RemoveRequest, DataManager.currentPlayer.Id, otherUsername);

        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion

    #region Shop

    public static void GetShop(Action<List<AItem>> handle)
    {
        var uri = string.Format(Constain.ep_GetShop, id);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void BuyItem(string idItem, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_BuyItem, id, idItem);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }

    internal static void UseAvatar(object handleUpdateAvatar)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Chat Private
    public static void AddChatPrivate(string otherUsername, string msg, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_AddChatPrivate, id, otherUsername, msg, DateTime.Now.ToString());
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void GetAllChatPrivate(string otherUsername, Action<List<AChatPrivate>> handle)
    {
        var uri = string.Format(Constain.ep_GetAllChatPrivate, id, otherUsername);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion


    #region Achievements
    public class AAchievement
    {
        public string Id;
        public string Name;
        public string Description;
        public int CurrentCount;
        public int MaxCount;
        public bool IsRecevied;
    }
    public static void GetAchievements(Action<List<AAchievement>> handle)
    {
        var uri = string.Format(Constain.ep_GetAchievements, id);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public static void ReceveieAchievement(string idAchievement, Action<List<AAchievement>> handle)
    {
        var uri = string.Format(Constain.ep_ReceveieAchievement, id, idAchievement);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    public enum EAchievementType
    {
        None = 0,
        WinQuest = 1,
        WinPiano = 2,
        WinOAnQuan = 3
    }
    public static void CompleteOneAchievement(EAchievementType type, Action<int> handle)
    {
        var uri = string.Format(Constain.ep_CompleteOneAchievement, id, type);
        APIManager.Instance.Call(uri, Constain.Method.GET, null, handle);
    }
    #endregion
}


