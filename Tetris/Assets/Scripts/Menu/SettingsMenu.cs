using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SelectableUI
{
    public Image img1, img2;
}

public class SettingsMenu : Menu
{
    public SelectableUI[] SelectableUIGameTypes;
    public SelectableUI[] SelectableUIMusicTypes;
    //
    private int _indexGameType = 0;
    private int _indexMusicType = 0;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 1; i < SelectableUIGameTypes.Length; i++)
        {
            SelectableUIChange(ref SelectableUIGameTypes[i], 0);
        }

        for (int i = 1; i < SelectableUIMusicTypes.Length; i++)
        {
            SelectableUIChange(ref SelectableUIMusicTypes[i], 0);
        }

        AudioManager.Instance.PlayMusic(0);
    }

    private void Update()
    {
        if (PlayerInput.LeftPressed())
        {
            AudioManager.Instance.PlaySelection();
            if (_indexGameType > 0)
            {
                SelectableUIChange(ref SelectableUIGameTypes[_indexGameType], 0);
                _indexGameType--;
                SelectableUIChange(ref SelectableUIGameTypes[_indexGameType], 1);
            }
        }
        else if (PlayerInput.RightPressed())
        {
            AudioManager.Instance.PlaySelection();
            if (_indexGameType < SelectableUIGameTypes.Length - 1)
            {
                SelectableUIChange(ref SelectableUIGameTypes[_indexGameType], 0);
                _indexGameType++;
                SelectableUIChange(ref SelectableUIGameTypes[_indexGameType], 1);
            }
        }

        //
        if (PlayerInput.UpPressed())
        {
            AudioManager.Instance.PlaySelection();
            if (_indexMusicType > 0)
            {
                SelectableUIChange(ref SelectableUIMusicTypes[_indexMusicType], 0);
                _indexMusicType--;
                SelectableUIChange(ref SelectableUIMusicTypes[_indexMusicType], 1);

                AudioManager.Instance.PlayMusic(_indexMusicType);
            }
        }
        else if (PlayerInput.DownPressed())
        {
            AudioManager.Instance.PlaySelection();
            if (_indexMusicType < SelectableUIMusicTypes.Length - 1)
            {
                SelectableUIChange(ref SelectableUIMusicTypes[_indexMusicType], 0);
                _indexMusicType++;
                SelectableUIChange(ref SelectableUIMusicTypes[_indexMusicType], 1);

                AudioManager.Instance.PlayMusic(_indexMusicType);
            }
        }

        if(PlayerInput.EnterPressed())
        {
            AudioManager.Instance.PlaySelection();
            GameData.gameType = _indexGameType == 0 ? "A - TYPE" : "B - TYPE";

            _manager.Close(this);
            _manager.Open<LevelsMenu>();
        }
    }

    #region Private

    private void SelectableUIChange(ref SelectableUI selectable, float alpha)
    {
        Color color = selectable.img1.color;
        color.a = alpha;
        selectable.img1.color = color;
        selectable.img2.color = color;
    }

    #endregion

    public override void OnBackPressed()
    {
        _manager.Close(this);
        _manager.Open<MainMenu>();
    }
}
