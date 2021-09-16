using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState : BaseState
{
    private IMenuAction _menuAction;

    public SettingsState(IStateMachine stateMachine, IMenuAction menuAction)
        : base(stateMachine)
    {
        _menuAction = menuAction;
    }

    public override void Start()
    {
        var ui = _stateMachine.UIData.GetUIData<SettingsUI>();
        ui.MusicChanged += _menuAction.MusicChanged;
        ui.Show();

        _menuAction.EnterPressed += OnEnter;
        _menuAction.BackPressed += OnBack;

        _menuAction.LeftPressed += ui.OnLeft;
        _menuAction.RightPressed += ui.OnRight;
        _menuAction.UpPressed += ui.OnUp;
        _menuAction.DownPressed += ui.OnDown;

        _menuAction.ActivateAudio(true);
    }

    public override void Stop()
    {
        var ui = _stateMachine.UIData.GetUIData<SettingsUI>();
        ui.MusicChanged -= _menuAction.MusicChanged;
        ui.Hide();

        //
        _menuAction.EnterPressed -= OnEnter;
        _menuAction.BackPressed -= OnBack;

        _menuAction.LeftPressed -= ui.OnLeft;
        _menuAction.RightPressed -= ui.OnRight;
        _menuAction.UpPressed -= ui.OnUp;
        _menuAction.DownPressed -= ui.OnDown;

    }

    private void OnEnter()
    {
        GameData.Instance.SetGameType(_stateMachine.UIData.GetUIData<SettingsUI>().GetGameType());
        _stateMachine.SwitchState<LevelsState>();
    }

    private void OnBack()
    {
        _menuAction.MusicChanged(-1);
        _menuAction.ActivateAudio(false);
        _stateMachine.UIData.GetUIData<SettingsUI>().ResetData();
        _stateMachine.SwitchState<MainState>();
    }

}
