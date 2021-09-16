using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IStateMachine
{
    IUIData UIData { get; }
    void SwitchState<T>() where T : BaseState;

    void StopState();
}

public abstract class BaseStateMachine : IStateMachine
{
    public IUIData UIData { get; private set; }

    protected BaseState _currentState;
    protected List<BaseState> _allStates;

    protected BaseStateMachine(IUIData uiData)
    {
        UIData = uiData;
    }

    public void SwitchState<T>() where T : BaseState
    {
        var state = _allStates.FirstOrDefault(x => x is T);
        _currentState?.Stop();
        state?.Start();
        _currentState = state;
    }

    public void StopState()
    {
        _currentState?.Stop();
        _currentState = null;
    }

}
