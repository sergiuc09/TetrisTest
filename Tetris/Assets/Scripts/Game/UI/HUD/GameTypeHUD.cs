using System.Collections;
using TMPro;
using UnityEngine;

public class GameTypeHUD : BaseGameHUD
{
    [SerializeField] private TMP_Text TxtValue;

    public override void SetValue(string value)
    {
        TxtValue.text = value;
    }
}
