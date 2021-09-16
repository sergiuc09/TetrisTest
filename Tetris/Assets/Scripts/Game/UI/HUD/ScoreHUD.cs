using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreHUD : BaseGameHUD
{
    [SerializeField] private TMP_Text TxtValue;

    public override void SetValue(int value)
    {
        TxtValue.text = value.ToString("D6");
    }
}
