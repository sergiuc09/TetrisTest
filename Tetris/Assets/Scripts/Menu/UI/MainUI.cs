using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public abstract class BaseMenuUI : BaseUI
{
    public event Action EnterPressed;
    public event Action BackPressed;

    public void OnEnterPressed()
    {
        EnterPressed?.Invoke();
    }

    public void OnBackPressed()
    {
        BackPressed?.Invoke();
    }
}*/

public class MainUI : BaseUI
{
    public event Action StartPressed;

    [SerializeField] private Button BtnStart;

    private void Awake()
    {
        BtnStart.onClick.AddListener(() => StartPressed?.Invoke());
    }
}
