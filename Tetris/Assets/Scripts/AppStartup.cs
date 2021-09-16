using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        var loadingOperations = new List<ILoadingOperation>
        {
            new MenuLoadingOperation()
        };
        LoadingScreen.Instance.LoadAsync(loadingOperations);
    }
}
