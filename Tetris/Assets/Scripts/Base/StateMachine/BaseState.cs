using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected readonly IStateMachine _stateMachine;

    protected BaseState(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void Start();
    public abstract void Stop();
}
