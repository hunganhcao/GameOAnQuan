using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Friend
{
    public class PopupFindFriend : XXX.UI.Popup.BasePopup
    {
        [SerializeField] private TMP_InputField ipfDisplayName;
        [SerializeField] private Button btnSearch;

        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);

            ipfDisplayName.onEndEdit.RemoveAllListeners();
            ipfDisplayName.onEndEdit.AddListener(HandleEndEdit);

            btnSearch.RegisterOnClick(OnClickSearch);
        }

        private void OnClickSearch()
        {
            Search();
        }

        private void HandleEndEdit(string arg0)
        {
            Search();
        }
        public void Search()
        {
            PopupFriend._callGetData = delegate () {
                APIRequest.FindFriend(ipfDisplayName.text, HandleSearchFriend);
            };
            PopupFriend._callGetData?.Invoke();
        }

        private void HandleSearchFriend(List<AFriendItem> data)
        {
            Close();
            EventManager.Notify(EventName.friend_RecevieFriend, data);
        }
    }
}
