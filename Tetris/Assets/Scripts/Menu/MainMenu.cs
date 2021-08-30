using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    private void Update()
    {
        if(PlayerInput.EnterPressed())
        {
            _manager.Close(this);
            _manager.Open<SettingsMenu>();
        }
    }

    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
