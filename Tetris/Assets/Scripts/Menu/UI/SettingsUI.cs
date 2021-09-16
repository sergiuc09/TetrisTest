using System;
using System.Collections;
using UnityEngine;

public class SettingsUI : BaseUI
{
    public event Action<int> MusicChanged;

    [SerializeField] private GameType[] _gameTypes;
    [SerializeField] private SelectableImage[] _musicTypes;

    private int _lengthGameTypes;

    private int _indexGameType = 0;
    private int _indexMusicType = 0;

    private void Awake()
    {
        _lengthGameTypes = _gameTypes.Length;

        ResetData();
    }

    #region Public

    public override void Show()
    {
        base.Show();

        StartCoroutine(AnimateSelectables());
        MusicChanged?.Invoke(_indexMusicType);
    }

    public void OnLeft()
    {
        ChangeGameType(_indexGameType - 1);
    }

    public void OnRight()
    {
        ChangeGameType(_indexGameType + 1);
    }

    public void OnUp()
    {
        ChangeMusicType(_indexMusicType - 1);
    }

    public void OnDown()
    {
        ChangeMusicType(_indexMusicType + 1);
    }

    public string GetGameType()
    {
        return _gameTypes[_indexGameType].gameType;
    }

    public void ResetData()
    {
        _indexGameType = 0;
        _indexMusicType = 0;

        for (int i = 0; i < _lengthGameTypes; i++)
        {
            SelectableChange(ref _gameTypes[i].selectable, 0);
        }

        for (int i = 0; i < _musicTypes.Length; i++)
        {
            SelectableChange(ref _musicTypes[i], 0);
        }
    }

    #endregion

    #region Private

    private void ChangeGameType(int index)
    {
        if (index < 0 || index >= _gameTypes.Length)
            return;

        //SelectableChange(ref Selectable_GameTypes[_indexGameType], 0);
        //SelectableChange(ref Selectable_GameTypes[index], 1);
        SelectableChange(ref _gameTypes[_indexGameType].selectable, 0);
        SelectableChange(ref _gameTypes[index].selectable, 1);

        _indexGameType = index;
    }

    private void ChangeMusicType(int index)
    {
        if (index < 0 || index >= _musicTypes.Length)
            return;

        MusicChanged(index);

        SelectableChange(ref _musicTypes[_indexMusicType], 0);
        SelectableChange(ref _musicTypes[index], 1);

        _indexMusicType = index;
    }

    private void SelectableChange(ref SelectableImage selectable, float alpha)
    {
        Color color = selectable.img1.color;
        color.a = alpha;
        selectable.img1.color = color;
        selectable.img2.color = color;
    }

    #endregion

    #region Coroutine

    private IEnumerator AnimateSelectables()
    {
        float alpha = 0;
        WaitForSeconds wfs = new WaitForSeconds(0.05f);

        while(isEnabled)
        {
            SelectableChange(ref _gameTypes[_indexGameType].selectable, alpha);
            SelectableChange(ref _musicTypes[_indexMusicType], alpha);

            alpha = alpha == 0 ? 1 : 0;

            yield return wfs;
        }
    }

    #endregion

}
