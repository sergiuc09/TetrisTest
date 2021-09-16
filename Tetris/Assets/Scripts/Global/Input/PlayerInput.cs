using System;
using System.Collections.Generic;
using UnityEngine;

/*public interface IPlayerInput
{
    event Action<Vector3> AxisChanged;
}*/

public class PlayerInput : IObservableModified<Vector3>, IDisposable
{
    //public event Action EnterPressed;
    //public event Action BackPressed;

    private IDeviceInput _input;

    private List<IObserverModified<Vector3>> _observers;

    public PlayerInput(IDeviceInput input)
    {
        _observers = new List<IObserverModified<Vector3>>();

        _input = input;

        //_input.EnterPressed += OnEnterPressed;
        //_input.BackPressed += OnBackPressed;

        //
        _input.LeftPressed += OnLeftPressed;
        //_input.LeftReleased += OnReleased;

        _input.RightPressed += OnRightPressed;
        //_input.RightReleased += OnReleased;

        _input.UpPressed += OnUpPressed;
        //_input.UpReleased += OnReleased;

        _input.DownPressed += OnDownPressed;
        //_input.DownReleased += OnReleased;
    }

    public void Register(IObserverModified<Vector3> observer)
    {
        _observers.Add(observer);
    }

    public void Unregister(IObserverModified<Vector3> observer)
    {
        _observers.Remove(observer);
    }

    public void Dispose()
    {
        //_input.EnterPressed -= OnEnterPressed;
        //_input.BackPressed -= OnBackPressed;

        _input.LeftPressed -= OnLeftPressed;
        //_input.LeftReleased -= OnReleased;

        _input.RightPressed -= OnRightPressed;
        //_input.RightReleased -= OnReleased;

        _input.UpPressed -= OnUpPressed;
        //_input.UpReleased -= OnReleased;

        _input.DownPressed -= OnDownPressed;
        //_input.DownReleased -= OnReleased;
    }

    #region Private 

    /*private void OnEnterPressed()
    {
        EnterPressed?.Invoke();
    }

    private void OnBackPressed()
    {
        BackPressed?.Invoke();
    }*/

    private void OnLeftPressed()
    {
        Notify(Vector3.left);
    }

    private void OnRightPressed()
    {
        Notify(Vector3.right);
    }

    private void OnUpPressed()
    {
        Notify(Vector3.up);
    }

    private void OnDownPressed()
    {
        Notify(Vector3.down);
    }

    private void OnReleased()
    {
        Notify(Vector3.zero);
    }

    private void Notify(Vector3 vec3)
    {
        foreach (var observer in _observers)
            observer.OnNotify(vec3);
    }

    #endregion
}
