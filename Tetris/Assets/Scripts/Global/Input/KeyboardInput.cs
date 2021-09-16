using System;
using System.Collections;
using UnityEngine;

public class KeyboardInput : DeviceInput, IDeviceInput
{
    public event Action EnterPressed;
    public event Action BackPressed;

    public event Action LeftPressed;
    public event Action RightPressed;
    public event Action UpPressed;
    public event Action DownPressed;

    /*public event Action LeftReleased;
    public event Action RightReleased;
    public event Action UpReleased;
    public event Action DownReleased;*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            EnterPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            BackPressed?.Invoke();

        //
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            LeftPressed?.Invoke();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            RightPressed?.Invoke();
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            UpPressed?.Invoke();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            DownPressed?.Invoke();
    }
}
