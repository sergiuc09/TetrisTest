using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverModified<in T>
{
    void OnNotify(T value);
}
