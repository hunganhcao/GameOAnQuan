using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SamplePool<T> where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private Transform parent;

    private Stack<T> _pool = new Stack<T>();
    private List<T> _externalPool = new List<T>();
    public List<T> ExternalPool => _externalPool;

    public SamplePool(T prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        _pool = new Stack<T>();
        _externalPool = new List<T>();
    }
    public void Prepare(int countInit)
    {
        _pool = new Stack<T>();
        _externalPool = new List<T>();
        for (int i = 0; i < countInit; i++)
        {
            CreateOne();
        }
    }
    private void CreateOne()
    {
        var go = Object.Instantiate(prefab, parent);
        Return(go);
    }
    public void Return(T go)
    {
        _pool.Push(go);
        if (_externalPool.Contains(go)) _externalPool.Remove(go);
        go.gameObject.SetActive(false);
    }
    public T Get()
    {
        if (_pool.Count == 0)
        {
            CreateOne();
        }
        var go = _pool.Pop();
        if (!_externalPool.Contains(go)) _externalPool.Add(go);
        go.gameObject.SetActive(true);
        return go;
    }
    public void ReturnAll()
    {
        var newExternalPool = new List<T>();
        newExternalPool.AddRange(_externalPool);
        foreach (var item in newExternalPool)
        {
            Return(item);
        }
    }
}