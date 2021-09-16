using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservableModified<out T>
{
    void Register(IObserverModified<T> observer);
    void Unregister(IObserverModified<T> observer);
}
