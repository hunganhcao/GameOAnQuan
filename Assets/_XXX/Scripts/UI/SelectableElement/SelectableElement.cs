
using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectableElement : MonoBehaviour
{
    [SerializeField] private GameObject bgSelected;
    [SerializeField] private GameObject bgDeselected;

    private Action<SelectableElement> _onSelectThis;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            if (value)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }
    }

    public void OnSelectThis(Action<SelectableElement> onSelectThis)
    {
        _onSelectThis = onSelectThis;
    }
    private void Select()
    {
        _onSelectThis?.Invoke(this);
        bgSelected.SetActive(true);
        bgDeselected.SetActive(false);
    }
    private void Deselect()
    {
        bgSelected.SetActive(false);
        bgDeselected.SetActive(true);
    }
}
