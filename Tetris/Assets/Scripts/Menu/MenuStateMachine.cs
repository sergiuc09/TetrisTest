using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : BaseStateMachine
{
    public MenuStateMachine(IUIData uiData, IMenuAction menuAction)
        : base(uiData)
    {
        _allStates = new List<BaseState>()
        {
            new MainState(this, menuAction),
            new SettingsState(this, menuAction),
            new LevelsState(this, menuAction)
        };

        SwitchState<MainState>();
    }
}
