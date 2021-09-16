using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private DeviceInput _deviceInput;
    [SerializeField] private MenuUIData _menuUIData;

    private AudioManager _audioManager;

    private Action OnDispose;

    private void Awake()
    {
        if (_menuUIData == null)
        {
            Debug.LogError("Assign Menu UI Data");
            return;
        }
        if(_deviceInput == null)
        {
            Debug.LogError("Assign Device Input");
            return;
        }

        _audioManager = FindObjectOfType<AudioManager>();

        new GameData();

        MenuAction menuAction = new MenuAction(_audioManager);

        MenuStateMachine menuStateMachine = new MenuStateMachine(_menuUIData, menuAction);

        //
        IDeviceInput deviceInput = (IDeviceInput)_deviceInput;
        deviceInput.EnterPressed += menuAction.OnEnter;
        deviceInput.BackPressed += menuAction.OnBack;

        deviceInput.LeftPressed += menuAction.OnLeft;
        deviceInput.RightPressed += menuAction.OnRight;
        deviceInput.UpPressed += menuAction.OnUp;
        deviceInput.DownPressed += menuAction.OnDown;
        //

        OnDispose = () =>
        {
            deviceInput.EnterPressed -= menuAction.OnEnter;
            deviceInput.BackPressed -= menuAction.OnBack;

            deviceInput.LeftPressed -= menuAction.OnLeft;
            deviceInput.RightPressed -= menuAction.OnRight;
            deviceInput.UpPressed -= menuAction.OnUp;
            deviceInput.DownPressed -= menuAction.OnDown;

            ((IDisposable)menuAction)?.Dispose();
        };
    }

    private void OnDisable()
    {
        OnDispose?.Invoke();
    }

}
