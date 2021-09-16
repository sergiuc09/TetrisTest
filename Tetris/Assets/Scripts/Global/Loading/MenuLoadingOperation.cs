using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadingOperation : ILoadingOperation
{
    public string Description => "Loading...";

    public async Task LoadAsync(Action<float> OnProgress)
    {
        var audio = UnityEngine.Object.FindObjectOfType<AudioManager>();
        if (audio != null) ((IDisposable)audio)?.Dispose();

        var loading = SceneManager.LoadSceneAsync(Scenes.Menu.ToString());
        
        while(!loading.isDone)
        {
            OnProgress(loading.progress);

            await Task.Delay(10);
        }

        OnProgress(1);
        await Task.Delay(100);
    }
} // End Class
