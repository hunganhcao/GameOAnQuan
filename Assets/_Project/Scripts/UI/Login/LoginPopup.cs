using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XXX.SO.Event;
using XXX.UI.Popup;

namespace WorldForKid.Login
{
    public class LoginPopup : MonoBehaviour
    {
        private void Start()
        {
            PopupManager.Instance.ShowPopupLogin(null);
            SoundManager.Instance.PlayMusic(ESoundType.Bg_Lobby);
        }
    }
}