using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : BaseState
{
    private IMenuAction _menuAction;

    public MainState(IStateMachine stateMachine, IMenuAction menuAction)
        : base(stateMachine)
    {
        _menuAction = menuAction;
    }

    public override void Start()
    {
        var ui = _stateMachine.UIData.GetUIData<MainUI>();
        ui.StartPressed += OnEnter;
        //ui.BackPressed += OnBack;

        ui.Show();

        _menuAction.EnterPressed += OnEnter;
        _menuAction.BackPressed += OnBack;
    }

    public override void Stop()
    {
        var ui = _stateMachine.UIData.GetUIData<MainUI>();
        ui.StartPressed -= OnEnter;
        //ui.BackPressed -= OnBack;

        ui.Hide();

        _menuAction.EnterPressed -= OnEnter;
        _menuAction.BackPressed -= OnBack;
    }

    private void OnEnter()
    {
        _stateMachine.SwitchState<SettingsState>();
        //WaitALittle();
    }

    private void OnBack()
    {
        MonoBehaviour.print("App Quit");

        //_stateMachine.StopState();
        //Application.Quit();
    }

    /*
    //if Canvas.Button is focused and we pressed on Enter,
    // then Canvas.Button will be invoked earlier than Update.Input.GetKeyDown(Enter)
    // because of this we need some delay
    // because, if we have next a subscriber onEnterPressed, then it will be called automatically
    private async void WaitALittle()
    {
        await System.Threading.Tasks.Task.Yield();
        _stateMachine.SwitchState<SettingsState>();
    }
    */
}
