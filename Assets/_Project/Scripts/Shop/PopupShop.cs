using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


namespace Assets._Project.Scripts.Shop
{
    public class PopupShop : XXX.UI.Popup.BasePopup
    {
        public enum TpyeShop
        {
            Shop,
            Inventory
        }
        public static TpyeShop Mode;

        [SerializeField] private EnhancedScroller1 scroller;
        [SerializeField] private TpyeShop type;
        [SerializeField] private TMP_Text txtType;
        private List<ShopGroupData> _data;
        private const int MaxColum = 4;

        public override void Initialized(object data = null, Action actionClose = null)
        {
            base.Initialized(data, actionClose);
            Mode = type;
            _data = new List<ShopGroupData>();
            scroller.Initialized();
            scroller.Clear();

            APIRequest.GetShop(HandleGetShop);
            if (type == TpyeShop.Shop) txtType.text = "Cửa hàng";
            if (type == TpyeShop.Inventory) txtType.text = "Túi đồ";
        }

        private void HandleGetShop(List<AItem> data)
        {
            data = data.OrderBy(x => x.Count).ToList();
            var tamData = new List<AItem>();
            tamData.AddRange(data);
            tamData.OrderBy(x => x.Count);
            if(Mode == TpyeShop.Inventory) 
            {
                data.Clear();
                foreach(var item in tamData)
                {
                    if (item.Count > 0)
                        data.Add(item);
                }
            }
            _data.Clear();
            var groupData = data.ChunkBy(MaxColum);
            foreach (var group in groupData)
            {
                var item = new ShopGroupData();
                item.Items = group;
                scroller.AddData(item);
            }
        }
    }
}
