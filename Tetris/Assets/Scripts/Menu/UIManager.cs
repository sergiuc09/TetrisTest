using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseUIManager
{
    public MainMenu Prefab_MainMenu;
    public SettingsMenu Prefab_SettingsMenu;
    public LevelsMenu Prefab_LevelsMenu;

    private void Awake()
    {
        Open<MainMenu>();
    }

}
