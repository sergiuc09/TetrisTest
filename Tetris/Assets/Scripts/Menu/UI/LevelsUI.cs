using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : BaseUI
{
    [SerializeField] private TMP_Text TxtGameType;

    [SerializeField] private Image[] Levels;

    private int _indexLevel = 0;

    public int Level => _indexLevel;

    private void Awake()
    {
        //TxtGameType.text = GameData.gameType;

        for (int i = 1; i < Levels.Length; i++)
        {
            ChangeLevel(ref Levels[i], 0);
        }
    }

    #region Public

    public override void Show()
    {
        base.Show();

        StartCoroutine(AnimateSelectables());
    }

    public void OnLeft()
    {
        ChangeLevel(_indexLevel - 1);
    }

    public void OnRight()
    {
        ChangeLevel(_indexLevel + 1);
    }

    public void OnUp()
    {
        ChangeLevel(_indexLevel - 5);
    }

    public void OnDown()
    {
        ChangeLevel(_indexLevel + 5);
    }

    public void SetGameType(string gameType)
    {
        TxtGameType.text = gameType;
    }

    #endregion

    #region Private

    private void ChangeLevel(int index)
    {
        if (index < 0 || index >= Levels.Length)
            return;

        ChangeLevel(ref Levels[_indexLevel], 0);
        ChangeLevel(ref Levels[index], 1);

        _indexLevel = index;
    }

    private void ChangeLevel(ref Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    #endregion

    #region Coroutine
    private IEnumerator AnimateSelectables()
    {
        float alpha = 0;
        WaitForSeconds wfs = new WaitForSeconds(0.05f);

        while (isEnabled)
        {
            ChangeLevel(ref Levels[_indexLevel], alpha);

            alpha = alpha == 0 ? 1 : 0;

            yield return wfs;
        }
    }

    #endregion

}
