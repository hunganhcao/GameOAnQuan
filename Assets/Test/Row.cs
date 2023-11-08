using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Row : BaseCellView<int>
{
    [SerializeField] private Text txt;
    public override void SetData(int data)
    {
        base.SetData(data);
        txt.text = data.ToString();
    }
}
