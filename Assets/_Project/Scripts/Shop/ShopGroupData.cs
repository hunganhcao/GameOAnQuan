using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum TypeItem
{
    Coin,
    Avatar_1,
    Avatar_2,
    Avatar_3,
    Avatar_4,
    Avatar_5,
    Avatar_6,
    Avatar_7,
    Avatar_8,
    Avatar_9,
    Avatar_10,
}
[System.Serializable]
public class AItem
{
    public string Id;
    public string Name;
    public string Description;
    public int Cost;
    public TypeItem Type;
    public int Count;
}
public class ShopGroupData : BaseDataItem
{
    public List<AItem> Items;
}

