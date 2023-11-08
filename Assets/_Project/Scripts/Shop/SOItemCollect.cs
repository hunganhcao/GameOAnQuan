using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SOItem
{
    public TypeItem Type;
    public Sprite Image;
}
[CreateAssetMenu]
public class SOItemCollect : ScriptableObject
{
    public List<SOItem> Items;
    public Sprite Get(TypeItem type)
    {
        var item = Items.FirstOrDefault(x => x.Type == type);
        if (item == null) return Items[0].Image;
        return item.Image;
    }
}
