using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Friend
{
    public class PopupFriend : XXX.UI.Popup.BasePopup
    {
        public enum FriendMode
        {
            Friend,
            FriendRequest,
            Search
        }
        [SerializeField] private Button btnFriend;
        [SerializeField] private Button btnSearch;
        [SerializeField] private Button btnFriendRequest;
        [SerializeField] private PopupFindFriend popupFindFriend;
        [SerializeField] private EnhancedScroller1 scroller;

        public static FriendMode Mode;

        private Button _currentBtn;
        public static Action _callGetData;
        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);
            scroller.Initialized();

            btnFriend.RegisterOnClick(OnClickFriend);

            btnSearch.RegisterOnClick(OnClickSearch);

            btnFriendRequest.RegisterOnClick(OnClickFriendRequest);

            scroller.Clear();

            EventManager.AddEvent(EventName.friend_RecevieFriend, HandleRecevieFriend);
            EventManager.AddEvent(EventName.friend_LoadAgainScroll, HandleLoadAgainScroll);

            popupFindFriend.Initialized(null);
            btnFriend.onClick.Invoke();
        }
        public override void Close()
        {
            base.Close();
            scroller.Clear();
        }
        private void HandleLoadAgainScroll(object data)
        {
            _callGetData?.Invoke();
        }

        private void HandleRecevieFriend(object data)
        {
            ShowScroller(true);
            scroller.Clear();
            var dataReceive = data as List<AFriendItem>;
            if (dataReceive == null) { return; }
            var friends = new List<FriendItemData>();

            foreach(var item in dataReceive)
            {
                var friend = new FriendItemData();
                friend.cellType = BaseDataItem.CellType.MyText;
                friend.Username = item.Username;
                friend.IsFriend = item.IsFriend;
                
                friends.Add(friend);
                scroller.AddData(friend);
            }
        }

        private void OnClickFriendRequest()
        {
            Mode = FriendMode.FriendRequest;
            ShowScroller(true);
            SelectOneButton(btnFriendRequest);
            _callGetData = delegate() {
                APIRequest.GetAllRequest(HandleGetFriends);
                };
            _callGetData?.Invoke();
        }

        private void HandleGetFriends(List<AFriendItem> obj)
        {
            EventManager.Notify(EventName.friend_RecevieFriend, obj);
        }

        private void OnClickSearch()
        {
            Mode = FriendMode.Search;
            ShowScroller(false); 
            SelectOneButton(btnSearch);
        }

        private void OnClickFriend()
        {
            Mode = FriendMode.Friend;
            ShowScroller(true);
            SelectOneButton(btnFriend);
            _callGetData = delegate () {
                APIRequest.GetFriends(HandleGetFriends);
            };
            _callGetData?.Invoke();
        }

        private void SelectOneButton(Button btn)
        {
            btnFriend.interactable = true;
            btnSearch.interactable = true;
            btnFriendRequest.interactable = true;

            btn.interactable = false;
            _currentBtn = btn;
        }

        private void ShowScroller(bool isActive)
        {
            scroller.scroller.gameObject.SetActive(isActive);
            popupFindFriend.gameObject.SetActive(!isActive);
        }
    }

}
