using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public bool DestroyWhenClosed = true;
    public bool DisableMenusUnderneath = true;

    protected BaseUIManager _manager;

    protected virtual void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 1;
    }

    public void Initialization(BaseUIManager manager)
    {
        _manager = manager;
    }

    public abstract void OnBackPressed();
}
