using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.ChatRoom
{
    public class ChatRoomItem : CustomEnhance.BaseCellView
    {
        private ChatRoomData _data;
        [SerializeField] private ChatRoomFillData myChat; 
        [SerializeField] private ChatRoomFillData otherChat; 
        public override void SetData(CustomEnhance.BaseDataItem data)
        {
            base.SetData(data);
            _data = data as ChatRoomData;
            if (_data == null) return;

            if (DataManager.CheckMineByUsername(_data.UsernameSender))
            {
                myChat.SetData(data);
                myChat.gameObject.SetActive(true);
                otherChat.gameObject.SetActive(false);
            }
            else
            {
                otherChat.SetData(data);
                myChat.gameObject.SetActive(false);
                otherChat.gameObject.SetActive(true);
            }
        }
    }
}
