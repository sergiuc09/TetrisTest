using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : BaseState
{
    private IGameAction _gameAction;

    public PauseState(IStateMachine stateMachine, IGameAction gameAction)
        : base(stateMachine)
    {
        _gameAction = gameAction;
    }

    public override void Start()
    {
        Time.timeScale = 0;

        var ui = _stateMachine.UIData.GetUIData<GamePauseUI>();
        ui.ResumePressed += OnResume;
        ui.MenuPressed += OnMenu;
        //ui.MenuPressed += _gameAction.OnMenu;

        _gameAction.Paused += OnResume;

        ui.Show();
    }

    public override void Stop()
    {
        var ui = _stateMachine.UIData.GetUIData<GamePauseUI>();
        ui.ResumePressed -= OnResume;
        ui.MenuPressed -= OnMenu;
        //ui.MenuPressed -= _gameAction.OnMenu;

        _gameAction.Paused -= OnResume;

        ui.Hide();
        Time.timeScale = 1;
    }

    private void OnResume()
    {
        _stateMachine.SwitchState<GameState>();
    }

    private void OnMenu()
    {
        _stateMachine.StopState();
        _gameAction.OnMenu();
    }
}
