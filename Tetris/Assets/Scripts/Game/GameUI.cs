using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TMP_Text TxtGameType;
    public TMP_Text TxtLines;
    public TMP_Text TxtTopScore;
    public TMP_Text TxtScore;
    public TMP_Text TxtLevel;

    //
    public TMP_Text TxtBlock_T;
    public TMP_Text TxtBlock_J;
    public TMP_Text TxtBlock_Z;
    public TMP_Text TxtBlock_O;
    public TMP_Text TxtBlock_S;
    public TMP_Text TxtBlock_L;
    public TMP_Text TxtBlock_I;

    public RectTransform Block_T;
    public RectTransform Block_J;
    public RectTransform Block_Z;
    public RectTransform Block_O;
    public RectTransform Block_S;
    public RectTransform Block_L;
    public RectTransform Block_I;

    public TMP_Text[] TxtBlocks;
    public RectTransform[] Blocks;
    //

    public RectTransform NextBlock;

    private RectTransform _block;

    public void SetGameType(string type)
    {
        TxtGameType.text = type;
    }

    public void SetLines(int value)
    {
        TxtLines.text = value.ToString("D3");
    }

    public void SetTopScore(int value)
    {
        TxtTopScore.text = value.ToString("D6");
    }

    public void SetScore(int value)
    {
        TxtScore.text = value.ToString("D6");
    }

    public void SetLevel(int value)
    {
        TxtLevel.text = value.ToString("D2");
    }

    public void SetNextBlock(RectTransform prefabBlock, Color color)
    {
        if (_block != null)
            Destroy(_block.gameObject);

        _block = Instantiate(prefabBlock);
        _block.transform.SetParent(NextBlock.transform);
        _block.anchoredPosition3D = Vector3.zero;
        _block.localScale = Vector3.one;

        SetBlockColor(_block.transform, color);
    }

    public void SetBlockCount(int index, int count)
    {
        TxtBlocks[index].text = count.ToString("D3");
    }

    public void SetBlocksColor(Color color)
    {
        /*SetBlockColor(Block_T.transform, color);
        SetBlockColor(Block_J.transform, color);
        SetBlockColor(Block_Z.transform, color);
        SetBlockColor(Block_O.transform, color);
        SetBlockColor(Block_S.transform, color);
        SetBlockColor(Block_L.transform, color);
        SetBlockColor(Block_I.transform, color);*/
        for(int i = 0; i < Blocks.Length; i++)
        {
            SetBlockColor(Blocks[i].transform, color);
        }
    }

    #region Private

    private void SetBlockColor(Transform block, Color color)
    {
        foreach (Transform item in block)
        {
            item.gameObject.GetComponent<Image>().color = color;
        }
    }

    #endregion
}
