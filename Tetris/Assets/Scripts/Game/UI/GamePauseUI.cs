using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : BaseUI
{
    public event Action ResumePressed;
    public event Action MenuPressed;

    [SerializeField] private TMP_Text TxtMessage;

    [SerializeField] private Button BtnResume;
    [SerializeField] private Button BtnMenu;

    private void Awake()
    {
        BtnResume.onClick.AddListener(() => ResumePressed?.Invoke());
        BtnMenu.onClick.AddListener(() => MenuPressed?.Invoke());
    }

}
