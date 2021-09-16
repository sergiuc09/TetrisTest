using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : MonoBehaviour
{
    private T[] _items;
    public T[] Items => _items;

    private int _size;

    public GameObjectPool(T prefab, int size, Transform parent = null)
    {
        _size = size;
        _items = new T[size];
        for (int i = 0; i < size; ++i)
        {
            _items[i] = Object.Instantiate(prefab);
            if (parent != null)
                _items[i].transform.parent = parent;

            _items[i].gameObject.SetActive(false);
        }
    }

    public GameObjectPool(T[] prefabs, Transform parent = null)
    {
        _size = prefabs.Length;
        _items = new T[_size];
        for (int i = 0; i < _size; ++i)
        {
            _items[i] = Object.Instantiate(prefabs[i]);
            if (parent != null)
                _items[i].transform.parent = parent;

            _items[i].gameObject.SetActive(false);
        }
    }

    public T Get()
    {
        for (int i = 0; i < _size; i++)
        {
            if (!_items[i].gameObject.activeInHierarchy)
                return _items[i];
        }

        return null;
    }

    public T Get(int index)
    {
        if (!_items[index].gameObject.activeInHierarchy)
            return _items[index];

        return null;
    }
}
