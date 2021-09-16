using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSystem<T>
{
    void Save(T data);
    T Load();
}
