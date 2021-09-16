using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    protected bool isEnabled;

    public virtual void Show()
    {
        canvas.enabled = true;
        isEnabled = true;
        /*if (canvas != null)
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);

            canvas.enabled = true;
            CanvasEnabled?.Invoke();
        }
        else gameObject.SetActive(true);*/
    }

    public virtual void Hide()
    {
        canvas.enabled = false;
        isEnabled = false;

        /*if (canvas != null)
            canvas.enabled = false;
        else gameObject.SetActive(false);*/
    }
}
