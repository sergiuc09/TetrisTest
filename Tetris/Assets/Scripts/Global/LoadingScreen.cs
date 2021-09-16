using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text TxtInfo;
    [SerializeField] private Image ProgressFill;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public async void LoadAsync(List<ILoadingOperation> loadingOperations)
    {
        if (!_canvas.enabled)
            _canvas.enabled = true;

        foreach(var operation in loadingOperations)
        {
            TxtInfo.text = operation.Description;

            await operation.LoadAsync(OnProgress);
        }

        _canvas.enabled = false;
        OnProgress(0);
        TxtInfo.text = "";
    }

    private void OnProgress(float progress)
    {
        ProgressFill.fillAmount = progress;
    }

} // End Class
