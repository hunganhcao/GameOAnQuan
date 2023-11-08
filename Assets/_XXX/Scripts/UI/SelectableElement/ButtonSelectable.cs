using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectable : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private SelectableElement selectableElement;

    public SelectableElement SelectableElement => selectableElement;
    public Button Btn => btn;

    public void RegistClickAllwaySelect(Action onClick)
    {
        btn.RegisterOnClick(() =>
        {
            onClick?.Invoke();
            selectableElement.IsSelected = true;
        });
    }
    public void RegistClickChangeState(Action onClick)
    {
        btn.RegisterOnClick(() =>
        {
            onClick?.Invoke();
            selectableElement.IsSelected = !selectableElement.IsSelected;
        });
    }
}
