using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsState : BaseState
{
    private IMenuAction _menuAction;

    public LevelsState(IStateMachine stateMachine, IMenuAction menuAction)
        : base(stateMachine)
    {
        _menuAction = menuAction;
    }

    public override void Start()
    {
        var ui = _stateMachine.UIData.GetUIData<LevelsUI>();
        ui.SetGameType(GameData.Instance.GameType);
        ui.Show();

        _menuAction.EnterPressed += OnEnter;
        _menuAction.BackPressed += OnBack;

        _menuAction.LeftPressed += ui.OnLeft;
        _menuAction.RightPressed += ui.OnRight;
        _menuAction.UpPressed += ui.OnUp;
        _menuAction.DownPressed += ui.OnDown;
    }

    public override void Stop()
    {
        var ui = _stateMachine.UIData.GetUIData<LevelsUI>();
        ui.Hide();

        _menuAction.EnterPressed -= OnEnter;
        _menuAction.BackPressed -= OnBack;
        _menuAction.UpPressed -= ui.OnUp;
        _menuAction.DownPressed -= ui.OnDown;

        _menuAction.LeftPressed -= ui.OnLeft;
        _menuAction.RightPressed -= ui.OnRight;
    }

    private void OnEnter()
    {
        _stateMachine.StopState();

        GameData.Instance.SetLevel(_stateMachine.UIData.GetUIData<LevelsUI>().Level);
        
        LoadingScreen.Instance.LoadAsync(new List<ILoadingOperation>
        {
            new GameLoadingOperation()
        });
    }

    private void OnBack()
    {
        _stateMachine.SwitchState<SettingsState>();
    }
}
