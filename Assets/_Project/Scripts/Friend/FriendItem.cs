using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XXX.UI.Popup;

namespace Assets._Project.Scripts.Friend
{
    public class FriendItem : BaseCellView
    {
        [SerializeField] private AvatarDisplay avatar;
        [SerializeField] private Button btnRemoveFriend;
        [SerializeField] private Button btnAcceptFriend;
        [SerializeField] private Button btnRemoveRequest;
        [SerializeField] private Button btnSendRequest;
        [SerializeField] private Button btnChatPrivate;

        private FriendItemData _data;
        public override void SetData(BaseDataItem data)
        {
            base.SetData(data);
            _data = data as FriendItemData;
            if (_data == null) return;

            btnRemoveFriend.RegisterOnClick(OnClickRemoveFriend);

            btnAcceptFriend.RegisterOnClick(OnClickAddFriend);

            btnRemoveRequest.RegisterOnClick(OnClickRemoveRequest);

            btnChatPrivate.RegisterOnClick(OnClickChatPrivate);

            btnSendRequest.RegisterOnClick(OnClickAddRequest);


            avatar.Initialized(_data.Username);
            SetButtons();
        }

        private void OnClickChatPrivate()
        {
            var dataInit = new PopupChatPrivate.DataInit()
            {
                UsernameFriend = _data.Username
            };
            PopupManager.Instance.ShowPopupChatPrivate(dataInit);
        }

        private void OnClickAddRequest()
        {
            APIRequest.AddRequest(_data.Username, LoadAgainScroll);
        }

        private void OnClickRemoveRequest()
        {
            APIRequest.RemoveRequest(_data.Username, LoadAgainScroll);
        }

        private void SetButtons()
        {
            btnRemoveFriend.gameObject.SetActive(_data.IsFriend);
            btnSendRequest.gameObject.SetActive(!_data.IsFriend);
            btnChatPrivate.gameObject.SetActive(_data.IsFriend);

            var inRequest = PopupFriend.Mode == PopupFriend.FriendMode.FriendRequest;
            btnRemoveRequest.gameObject.SetActive(inRequest);
            btnAcceptFriend.gameObject.SetActive(inRequest);
        }
        private void OnClickAddFriend()
        {
            APIRequest.AddFriend(_data.Username, LoadAgainScroll);
        }
        private void OnClickRemoveFriend()
        {
            APIRequest.RemoveFriend(_data.Username, LoadAgainScroll);
        }
        private void LoadAgainScroll(int data)
        {
            EventManager.Notify(EventName.friend_LoadAgainScroll, null);
        }
    }
}
