using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceInput
{
    event Action EnterPressed;
    event Action BackPressed;

    event Action LeftPressed;
    event Action RightPressed;
    event Action UpPressed;
    event Action DownPressed;

    /*event Action LeftReleased;
    event Action RightReleased;
    event Action UpReleased;
    event Action DownReleased;*/
}

public abstract class DeviceInput : MonoBehaviour
{

}
