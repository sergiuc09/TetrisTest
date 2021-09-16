using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    public event Action ReplayPressed;
    public event Action MenuPressed;

    [SerializeField] private TMP_Text TxtMessage;

    [SerializeField] private Button BtnReplay;
    [SerializeField] private Button BtnMenu;

    private void Awake()
    {
        BtnReplay.onClick.AddListener(() => ReplayPressed?.Invoke());
        BtnMenu.onClick.AddListener(() => MenuPressed?.Invoke());
    }

}
