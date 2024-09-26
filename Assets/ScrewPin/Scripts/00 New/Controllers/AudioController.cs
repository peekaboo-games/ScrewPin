using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : GameMonobehavior
{
    public static AudioController Instance;

    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume = 0.5f;
    /// the sound fx volume
    [Range(0, 1)]
    public float sfxVolume = 1f;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds and musics: ")]
    public AudioClip click;
    public AudioClip closePopup;
    public AudioClip openPopup;
    public AudioClip moveBox;
    public AudioClip closeLid;
    public AudioClip screw;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip collide;
    public AudioClip notScrew;

    public AudioClip[] backgroundMusics;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
            DontDestroyOnLoad(gameObject);
        LoadAudioSettings();
    }
    private void Start()
    {
        PlayBackgroundMusic();
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clips">Array of sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip[] clips, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clips != null && clips.Length > 0 && aus)
        {
            var randomIdx = Random.Range(0, clips.Length);
            aus.PlayOneShot(clips[randomIdx], sfxVolume);
        }
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clip">Sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            aus.PlayOneShot(clip, sfxVolume);
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="musics">Array of musics</param>
    /// <param name="loop">Can Loop</param>
    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus && musics != null && musics.Length > 0)
        {
            var randomIdx = Random.Range(0, musics.Length);

            musicAus.clip = musics[randomIdx];
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="music">music</param>
    /// <param name="canLoop">Can Loop</param>
    public void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Set volume for audiosource
    /// </summary>
    /// <param name="vol">New Volume</param>
    public void SetMusicVolume(float vol)
    {
        if (musicAus) musicAus.volume = vol;
    }

    /// <summary>
    /// Stop play music or sound effect
    /// </summary>
    public void StopPlayMusic()
    {
        if (musicAus) musicAus.Stop();
    }

    public void StopPlaySfx(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            //aus.PlayOneShot(clip, sfxVolume);
            aus.Stop();
        }
    }

    public void PlayBackgroundMusic()
    {
        PlayMusic(backgroundMusics, true);
    }

    public void ToggleMusic()
    {
        musicAus.mute = !musicAus.mute;
        PlayerPrefs.SetInt("MusicMuted", musicAus.mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSFX()
    {
        sfxAus.mute = !sfxAus.mute;
        PlayerPrefs.SetInt("SFXMuted", sfxAus.mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        musicAus.mute = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        sfxAus.mute = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
    }


}
