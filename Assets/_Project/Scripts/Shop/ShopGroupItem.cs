using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Shop
{

    public class ShopGroupItem : BaseCellView
    {
        [SerializeField] private ItemShop[] items;
        private ShopGroupData _data;
        public override void SetData(BaseDataItem data)
        {
            base.SetData(data);
            _data = data as ShopGroupData;
            if(_data == null)
            {
                return;
            }
            for(int i = 0; i < items.Length; i++)
            {
                if(i < _data.Items.Count)
                {
                    items[i].gameObject.SetActive(true);
                    items[i].SetData(_data.Items[i]);
                }
                else
                {
                    items[i].gameObject.SetActive(false);
                    continue;
                }
            }
        }

        

        
    }
}
