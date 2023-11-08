using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_ImageColection_", menuName = Constain.SOPATH_BASE + "SOImageCollection")]
public class SOImageCollection : ScriptableObject
{
    [SerializeField] private List<Sprite> Images;

    public Sprite Get(int index)
    {
        if (index < 0 || index > Images.Count) return null;
        return Images[index];
    }
}
