using System.Collections.Generic;

public class GameStateMachine : BaseStateMachine
{
    public GameStateMachine(IUIData uiData, IGameAction gameAction)
        : base(uiData)
    {
        _allStates = new List<BaseState>()
        {
            new GameState(this, gameAction),
            new PauseState(this, gameAction),
            new GameOverState(this, gameAction)
        };

        SwitchState<GameState>();
    }

}