using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : BaseState
{
    private IGameAction _gameAction;

    public GameState(IStateMachine stateMachine, IGameAction gameAction)
        : base(stateMachine)
    {
        _gameAction = gameAction;
    }

    public override void Start()
    {
        _gameAction.Paused += OnPause;
        _gameAction.GameOver += OnGameOver;
    }

    public override void Stop()
    {
        _gameAction.Paused -= OnPause;
        _gameAction.GameOver -= OnGameOver;
    }

    private void OnPause()
    {
        _stateMachine.SwitchState<PauseState>();
    }

    private void OnGameOver()
    {
        _stateMachine.SwitchState<GameOverState>();
    }
}
