
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataManager : MonoBehaviour
{
    [SerializeField] private ListenMessageSocket<SMessageFirstConnect> l_FirstConnect;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_RecevieJoinRoom;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_RecevieCreateRoom;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_LeaveRoom;
    [SerializeField] private ListenMessageSocket<SocketCall.SRoomStatus> l_StartGame;
    public static int idRoomGlobal { get; private set; }
    public static int idRoomGame => roomWaitStatus.Id;

    #region Test
    public const bool IsAndroid = false;
    #endregion

    #region Connect
    public static string IpServer { get; set; }
    public static int PortSocket { get; set; }
    public static int  PortAPI{ get; set; }
    #endregion


    #region Data in game
    public static UserDTO currentPlayer { get; set; }
    public static List<UserDTO> UsersInGame { get; set; } = new List<UserDTO>();    

    public static void GetUserIngame(string username, Action<UserDTO> handle)
    {
        var user = UsersInGame.FirstOrDefault(x => x.Username.Equals(username));
        if (user != null)
        {
            handle(user);
            return;
        }
        APIRequest.GetUserByUsername(username, (x) => 
        {
            UsersInGame.Add(x);
            handle(x);
        });
    }
    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EventManager.AddEvent(EventName.UpdateUser, UpdateUser);
    }
    private void OnDestroy()
    {
        EventManager.AddEvent(EventName.UpdateUser, UpdateUser);
    }

    private void UpdateUser(object data)
    {
        currentPlayer = (UserDTO)data;
    }

    private void Start()
    {
        l_FirstConnect.RegisterEvent(EventName.Socket_FirstConnect, HandelRecevieFirstConnect);
        l_RecevieJoinRoom.RegisterEvent(EventName.Socket_JoinRoom, SetRoomWaitStatus);
        l_RecevieCreateRoom.RegisterEvent(EventName.Socket_CreateRoom, SetRoomWaitStatus);
        l_LeaveRoom.RegisterEvent(EventName.Socket_LeaveRoom, SetRoomWaitStatus);
        l_StartGame.RegisterEvent(EventName.Socket_StartGame, SetRoomGame);
    }
    public static SocketCall.SRoomStatus roomWaitStatus;
    public static SocketCall.SRoomStatus roomGameStatus;


    private void SetRoomGame(SocketCall.SRoomStatus obj)
    {
        roomWaitStatus = obj;
        roomGameStatus = obj;
    }
    private void SetRoomWaitStatus(SocketCall.SRoomStatus obj)
    {
        roomWaitStatus = obj;
        roomGameStatus = obj;
    }

    public static bool CheckMine(string name)
    {
        return currentPlayer.DisplayName == name;
    }
    public static bool CheckMineByUsername(string name)
    {
        return currentPlayer.Username == name;
    }
    private void HandelRecevieFirstConnect(SMessageFirstConnect data)
    {
        idRoomGlobal = data.IdRoomServer;
    }
    public static void GetMe()
    {
        APIRequest.GetMe((x) =>
        {
            currentPlayer = x;
            for (int i = 0; i < UsersInGame.Count; i++)
            {
                if (UsersInGame[i].Id.Equals(x.Id))
                {
                    UsersInGame[i] = x;
                }
            }
            EventManager.Notify(EventName.UpdateUser, x);
        });
    }
}
