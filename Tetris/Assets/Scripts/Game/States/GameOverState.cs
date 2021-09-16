using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState
{
    private IGameAction _gameAction;

    public GameOverState(IStateMachine stateMachine, IGameAction gameAction)
        : base(stateMachine)
    {
        _gameAction = gameAction;
    }

    public override void Start()
    {
        var ui = _stateMachine.UIData.GetUIData<GameOverUI>();
        ui.ReplayPressed += OnReplay;
        ui.MenuPressed += OnMenu;

        ui.Show();
    }

    public override void Stop()
    {
        var ui = _stateMachine.UIData.GetUIData<GameOverUI>();
        ui.ReplayPressed -= OnReplay;
        ui.MenuPressed -= OnMenu;

        ui.Hide();
    }

    private void OnReplay()
    {
        _stateMachine.StopState();
        _gameAction.OnReplay();
    }

    private void OnMenu()
    {
        _stateMachine.StopState();
        _gameAction.OnMenu();
    }

}
