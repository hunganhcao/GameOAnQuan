using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using XXX.UI.Popup;

namespace Assets._Project.Scripts.Profile
{
    public class PopupEditName : XXX.UI.Popup.BasePopup
    {
        [SerializeField] private TMP_InputField txtDisplayName;
        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);
            txtDisplayName.onEndEdit.RemoveAllListeners();
            txtDisplayName.onEndEdit.AddListener(HandleOnEndEdit);
        }

        private void HandleOnEndEdit(string newName)
        {
            APIRequest.SendChangeUsername(newName, HandleSuccess);
        }

        private void HandleSuccess(UserDTO obj)
        {
            PopupManager.Instance.ShowPopupLog("Bạn đã thay đổi tên thành công");
        }
    }
}
