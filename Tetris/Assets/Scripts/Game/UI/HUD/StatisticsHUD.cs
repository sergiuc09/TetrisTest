using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct BlockStatistic
{
    public GameObject block;
    public TMP_Text TxtCount;
}

public class StatisticsHUD : BaseGameHUD, IColorChangeable
{
    //[SerializeField] private GameObject[] _blocks;
    [SerializeField] private BlockStatistic[] _blockStatistics;

    public override void SetValue(int index, int value)
    {
        _blockStatistics[index].TxtCount.text = value.ToString("D3");
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < _blockStatistics.Length; i++)
        {
            foreach (Transform child in _blockStatistics[i].block.transform)
            {
                Image img = child.GetComponent<Image>();
                if (img != null)
                    img.color = color;
            }
        }
    }

} // End Class
