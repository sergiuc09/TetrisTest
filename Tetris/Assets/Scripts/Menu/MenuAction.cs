using System;

public interface IMenuAction
{
    event Action EnterPressed;
    event Action BackPressed;

    event Action LeftPressed;
    event Action RightPressed;
    event Action UpPressed;
    event Action DownPressed;

    void ActivateAudio(bool flag);
    void MusicChanged(int index);
}

public class MenuAction : IMenuAction, IDisposable
{
    public event Action EnterPressed;
    public event Action BackPressed;

    public event Action LeftPressed;
    public event Action RightPressed;
    public event Action UpPressed;
    public event Action DownPressed;

    private AudioManager _audioManager;

    private bool _isActiveAudio = false;

    public MenuAction(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public void Dispose()
    {
        if (_isActiveAudio) ActivateAudio(false);
    }

    public void ActivateAudio(bool flag)
    {
        if (_isActiveAudio == flag) return;

        _isActiveAudio = flag;

        if (_isActiveAudio)
        {
            LeftPressed += _audioManager.PlaySelection;
            RightPressed += _audioManager.PlaySelection;
            UpPressed += _audioManager.PlaySelection;
            DownPressed += _audioManager.PlaySelection;

            EnterPressed += _audioManager.PlaySelection;
        }
        else
        {
            LeftPressed -= _audioManager.PlaySelection;
            RightPressed -= _audioManager.PlaySelection;
            UpPressed -= _audioManager.PlaySelection;
            DownPressed -= _audioManager.PlaySelection;

            EnterPressed -= _audioManager.PlaySelection;
        }
    }

    public void MusicChanged(int index)
    {
        _audioManager.PlayMusic(index);
    }

    public void OnEnter()
    {
        EnterPressed?.Invoke();
    }

    public void OnBack()
    {
        BackPressed?.Invoke();
    }

    public void OnLeft()
    {
        LeftPressed?.Invoke();
    }

    public void OnRight()
    {
        RightPressed?.Invoke();
    }

    public void OnUp()
    {
        UpPressed?.Invoke();
    }

    public void OnDown()
    {
        DownPressed?.Invoke();
    }
}
