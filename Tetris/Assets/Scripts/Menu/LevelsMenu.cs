using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : Menu
{
    public TMP_Text TxtGameType;

    public Button[] Levels;

    private Image[] ImgLevels;
    private int _indexLevel = 0;

    private void Start()
    {
        TxtGameType.text = GameData.gameType;

        ImgLevels = new Image[Levels.Length];
        for(int i = 0; i < Levels.Length; i++)
        {
            ImgLevels[i] = Levels[i].GetComponent<Image>();

            if (i > 0) LevelChange(ref ImgLevels[i], 0);
        }
    }

    private void Update()
    {
        if (PlayerInput.LeftPressed())
        {
            AudioManager.Instance.PlaySelection();

            if (_indexLevel > 0)
            {
                LevelChange(ref ImgLevels[_indexLevel], 0);
                _indexLevel--;
                LevelChange(ref ImgLevels[_indexLevel], 1);
            }
        }
        else if (PlayerInput.RightPressed())
        {
            AudioManager.Instance.PlaySelection();

            if (_indexLevel < ImgLevels.Length - 1)
            {
                LevelChange(ref ImgLevels[_indexLevel], 0);
                _indexLevel++;
                LevelChange(ref ImgLevels[_indexLevel], 1);
            }
        }

        if(PlayerInput.EnterPressed())
        {
            AudioManager.Instance.PlaySelection();

            GameData.level = _indexLevel;
            SceneLoader.Load(Scenes.Game);
        }
    }

    private void LevelChange(ref Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public override void OnBackPressed()
    {
        _manager.Close(this);
        _manager.Open<SettingsMenu>();
    }
}
