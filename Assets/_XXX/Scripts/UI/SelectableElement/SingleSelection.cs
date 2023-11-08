using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSelection 
{
    private SelectableElement _selectedElement;
    private CustomList<SelectableElement> _elements;
    private Action<SelectableElement> _onSelectOne;
    public CustomList<SelectableElement> Elements => _elements;

    public SingleSelection()
    {
        _elements = new CustomList<SelectableElement>();
        _onSelectOne = SetSelect;
    }
    public SingleSelection(params SelectableElement[] selectables)
    {
        _elements = new CustomList<SelectableElement>();
        _onSelectOne = SetSelect;
        foreach (var item in selectables)
        {
            Add(item);
        }
    }
    public void SetSelect(SelectableElement element)
    {
        if(_elements.Elemnts.Contains(element))
        {
            _selectedElement = element;
            foreach(var item in _elements.Elemnts)
            {
                if (item.Equals(element)) continue;
                item.IsSelected = false;
            }  
                
        }
    }
    public void Add(SelectableElement element)
    {
        if (Elements.Add(element))
        {
            element.OnSelectThis(_onSelectOne);
        }
    }
    public void Remove(SelectableElement element)
    {
        Elements.Remove(element);
    }
    public void DeselectAll()
    {
        foreach (var element in _elements.Elemnts)
        {
            element.IsSelected = false;
        }
        _selectedElement = null;
    }
    public SelectableElement SelectedElement
    {
        get => _selectedElement;
        set
        {
            foreach(var item in _elements.Elemnts)
            {
                if (item.Equals(value)) continue;
                item.IsSelected = false;
            }
            _selectedElement = value;
        }
    }
    //public bool Contain(ISelectable element) => _elements.Contains(element);

}
