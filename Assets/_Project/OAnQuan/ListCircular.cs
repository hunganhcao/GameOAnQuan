using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ListCircular<T>
{
    [SerializeField] private List<T> list;
    public List<T> List => list;

    private int _currentIndex;

    public int GetIndex(T item)
    {
        return list.IndexOf(item);
    }
    public void SetIndex(T item)
    {
        _currentIndex = GetIndex(item);
    }
    public void SetIndex(int index)
    {
        _currentIndex = index;
    }
    public T Next()
    {
        _currentIndex += 1;
        if (_currentIndex >= list.Count)
        {
            _currentIndex = 0;
        }
        return list[_currentIndex];
    }
    public T Pre()
    {
        _currentIndex -= 1;
        if (_currentIndex < 0)
        {
            _currentIndex = list.Count - 1;
        }
        return list[_currentIndex];
    }
}
