using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomList<T>
{
    private List<T> _elemnts;
    public List<T> Elemnts => _elemnts;
    public CustomList() 
    {
        _elemnts = new List<T>();
    }
    public bool Add(T element)
    {
        if(Elemnts.Contains(element)) return false;
        Elemnts.Add(element);
        return true;
    }
    public bool Remove(T element)
    {
        if (!Elemnts.Contains(element)) return false;   
        Elemnts.Remove(element);    
        return true;
    }
}
