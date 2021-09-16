using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IDisposable
{
    [SerializeField] private AudioClip[] _songs;

    [SerializeField] private AudioClip _selection;
    [SerializeField] private AudioClip _blockRotate;
    [SerializeField] private AudioClip _blockFalled;
    [SerializeField] private AudioClip _lineRemove;
    [SerializeField] private AudioClip _gameOver;

    private AudioSource _music, _sound;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _music = gameObject.AddComponent<AudioSource>();
        _sound = gameObject.AddComponent<AudioSource>();

        _music.volume = 0.6f;
        _music.loop = true;

        _music.playOnAwake = false;
        _sound.playOnAwake = false;
    }

    public void Dispose()
    {
        if (_music.isPlaying) _music.Stop();
        //if (_sound.isPlaying) _sound.Stop();
    }

    public void PlaySelection() => _sound.PlayOneShot(_selection);
    public void PlayBlockRotation() => _sound.PlayOneShot(_blockRotate);
    public void PlayBlockFalled() => _sound.PlayOneShot(_blockFalled);
    public void PlayLineRemove() => _sound.PlayOneShot(_lineRemove);
    public void PlayGameOver() => _sound.PlayOneShot(_gameOver);

    //
    public void PlayMusic(int index)
    {
        if (index < 0 || index >= _songs.Length)
        {
            if (_music.isPlaying)
            {
                _music.Stop();
                _music.clip = null;
            }
        }
        else
        {
            _music.clip = _songs[index];
            _music.Play();
        }
    }

}
