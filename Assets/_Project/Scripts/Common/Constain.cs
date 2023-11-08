using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constain
{
    #region Scenes Name
    public const string SN_LOGIN = "Login";
    public const string SN_LOBBY = "Lobby";
    public const string SN_GAME_CAUDO = "GameCauDo";
    public const string SN_CONNECTSOCKET = "ConnectSocket";
    public const string SN_PianoGame = "PianoGame";
    public const string SN_OAnQuan = "OAnQuan";
    #endregion


    #region ScriptableObject Path
    public const string SOPATH_BASE = "WorldForKid/";
    #endregion

    #region API
    public const string URLAPI = "http://{0}:{1}";
    public const string ep_Login = "/user/login";
    public const string ep_Register = "/user/register";
    public const string ep_GetUserByUsername = "/user/GetOtherUser?username={0}";
    public const string ep_UseAvatar = "/user/UseAvatar?id={0}&indexAvatar={1}";
    public const string ep_GetMe = "/user/GetMe?id={0}";

    public const string ep_Ranking = "/ResultGame/GetResultsRanking?id={0}&gameType={1}";
    public const string ep_GetResultForUser = "/ResultGame/GetResultForUser?id={0}&gameType={1}";
    public const string ep_WinOneGame = "/ResultGame/WinOneGame?id={0}&gameType={1}";
    public const string ep_LoseOneGame = "/ResultGame/LoseOneGame?id={0}&gameType={1}";

    public const string ep_GetFriend = "/Friend/GetFriends/?id={0}";
    public const string ep_FindFriend = "/Friend/FindFriends?id={0}&smallDisplayName={1}";
    public const string ep_AddFriend = "/Friend/Add?id={0}&otherUserName={1}";
    public const string ep_RemoveFriend = "/Friend/Remove?id={0}&otherUserName={1}";

    public const string ep_GetAllRequests = "/FriendRequest/GetAll?id={0}";
    public const string ep_AddRequest = "/FriendRequest/Add?id={0}&otherUserName={1}";
    public const string ep_RemoveRequest = "/FriendRequest/Remove?id={0}&otherUserName={1}";

    public const string ep_GetRequests = "/FriendRequest/GetAll?id={0}";
    public const string ep_GetShop = "/shop/GetShop?id={0}";
    public const string ep_BuyItem = "/shop/BuyItem?id={0}&idItem={1}";

    public const string ep_AddChatPrivate = "/chat/add?id={0}&otherUsername={1}&msg={2}&time={3}";
    public const string ep_GetAllChatPrivate = "/chat/GetAll?id={0}&otherUsername={1}";
    
    public const string ep_GetAchievements = "/Achievement/GetAchievements?id={0}";
    public const string ep_ReceveieAchievement = "/Achievement/ReceveieAchievement?id={0}&idAchievement={1}";
    public const string ep_CompleteOneAchievement = "/Achievement/CompleteOneAchievement?id={0}&type={1}";

    public enum Method
    {
        POST, GET
    }
    #endregion
}
