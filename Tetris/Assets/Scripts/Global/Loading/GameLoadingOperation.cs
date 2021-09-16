using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameLoadingOperation : ILoadingOperation
{
    public string Description => "Loading Game...";

    public async Task LoadAsync(Action<float> OnProgress)
    {
        var loading = SceneManager.LoadSceneAsync(Scenes.Game.ToString());

        while (!loading.isDone)
        {
            OnProgress(loading.progress);

            await Task.Delay(10);
        }

        OnProgress(1);
        await Task.Delay(100);
    }
}
