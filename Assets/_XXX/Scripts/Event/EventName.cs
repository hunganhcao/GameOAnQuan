public enum EventName
{
    None = 0,
    Value_Int_Test,

    #region ShowPopup

    #endregion


    #region Socket
    Socket_FirstConnect = 1000,
    Socket_CreateRoom = 1001,
    Socket_JoinRoom = 1002,
    Socket_StartGame = 1003,
    Socket_CompleteOneQuest = 1004,
    Socket_CompleteAllQuest = 1005,
    Socket_LeaveRoom = 1006,
    Socket_DataGamePiano = 1007,
    Socket_StartGameOAnQuan = 1008,
    Socket_ChooseTile = 1009,
    Socket_ChatRoom = 1010,
    Socket_ChatPrivate = 1011,

    #endregion


    #region PianoGame
    OnClickButonAnswer = 2000,
    OnUpdateNewResult = 2001,
    OnSetAllowClickButtonAnswer = 2002,
    #endregion

    #region O An Quan
    PutItemsInCart,
    SelectTile,
    ChooseLeft,
    ChooseTile,
    #endregion


    #region Friend
    friend_RecevieFriend,
    friend_LoadAgainScroll,

    #endregion

    #region User
    UpdateUser
    #endregion
}
