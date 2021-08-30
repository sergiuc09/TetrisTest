using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public static bool EnterPressed()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }

    public static bool BackPressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public static bool LeftPressed()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public static bool RightPressed()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    public static bool UpPressed()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    public static bool DownPressed()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public static bool DownPressing()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }
}
