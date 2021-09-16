using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameHUD : MonoBehaviour
{
    public virtual void SetValue(string value) { }
    public virtual void SetValue(int value) { }
    public virtual void SetValue(int index, int value) { }
}
