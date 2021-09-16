using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public interface IUIData
{
    T GetUIData<T>() where T : BaseUI;
}

public abstract class BaseUIData : MonoBehaviour, IUIData
{
    public T GetUIData<T>() where T : BaseUI
    {
        var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var ui = field.GetValue(this) as T;
            if (ui != null)
                return ui;
        }

        return null;
    }
}
