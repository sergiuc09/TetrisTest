using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameAction
{
    event Action Paused;
    event Action GameOver;
    
    void OnMenu();
    void OnReplay();
}

public class GameAction : IGameAction
{
    public event Action Paused;
    public event Action GameOver;

    public void OnMenu()
    {
        LoadingScreen.Instance.LoadAsync(new List<ILoadingOperation>
        {
            new MenuLoadingOperation()
        });
    }

    public void OnReplay()
    {
        LoadingScreen.Instance.LoadAsync(new List<ILoadingOperation>
        {
            new GameLoadingOperation()
        });
    }

    //
    public void OnPaused()
    {
        Paused?.Invoke();
    }

    public void OnGameOver()
    {
        GameOver?.Invoke();
    }
}
