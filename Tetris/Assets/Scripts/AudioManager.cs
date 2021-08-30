using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip Selection;
    public AudioClip BlockRotate;
    public AudioClip BlockFalled;
    public AudioClip LineRemove;
    public AudioClip GameOver;

    //public AudioClip Music1, Music2, Music3;
    public AudioClip[] Songs;

    private AudioSource Music, SFX;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        Instance = this;
        Music = gameObject.AddComponent<AudioSource>();
        SFX = gameObject.AddComponent<AudioSource>();

        Music.playOnAwake = false;
        SFX.playOnAwake = false;
    }

    #region Public

    #region Play SFX

    public void PlaySelection()
    {
        PlaySFX(Selection);
    }

    public void PlayBlockRotate()
    {
        PlaySFX(BlockRotate);
    }

    public void PlayBlockFalled()
    {
        PlaySFX(BlockFalled);
    }

    public void PlayLineRemove()
    {
        PlaySFX(LineRemove);
    }

    public void PlayGameOver()
    {
        PlaySFX(GameOver);
    }

    #endregion

    public void PlayMusic(int index)
    {
        if(index < 0 || index >= Songs.Length)
        {
            if(Music.isPlaying)
            {
                Music.Stop();
                Music.clip = null;
            }
        }
        else
        {
            Music.clip = Songs[index];
            Music.Play();
        }
    }

    #endregion

    #region Private

    private void PlaySFX(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.Play();
    }

    #endregion

}
