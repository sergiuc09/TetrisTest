using System.Collections;
using TMPro;
using UnityEngine;

public class LinesHUD : BaseGameHUD
{
    [SerializeField] private TMP_Text TxtValue;

    public override void SetValue(int value)
    {
        TxtValue.text = value.ToString("D3");
    }
}
