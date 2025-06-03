using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Mixer (Master)")]
    public AudioMixer masterMixer; // Kéo duy nhất file AudioMixer (Master) vào đây

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePrefab;   // Output là SFX group

    // PlayerPrefs keys
    private const string MASTER_VOL_KEY = "MasterVolume";
    private const string MUSIC_VOL_KEY = "BGMusicVolume";
    private const string SFX_VOL_KEY = "SFXVolume";

    private AudioSource _currentBgMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        float masterVol = PlayerPrefs.GetFloat(MASTER_VOL_KEY, 0.8f);
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 0.8f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_VOL_KEY, 0.8f);

        ApplyMasterVolume(masterVol);
        ApplyMusicVolume(musicVol);
        ApplySFXVolume(sfxVol);
    }


    public void SetMasterVolume(float value)
    {
        ApplyMasterVolume(value);
        PlayerPrefs.SetFloat(MASTER_VOL_KEY, value);
    }

    public void SetMusicVolume(float value)
    {
        ApplyMusicVolume(value);
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        ApplySFXVolume(value);
        PlayerPrefs.SetFloat(SFX_VOL_KEY, value);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOL_KEY, 0.8f);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 0.8f);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOL_KEY, 0.8f);
    }

    private void ApplyMasterVolume(float value)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void ApplyMusicVolume(float value)
    {
        masterMixer.SetFloat("BgMusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void ApplySFXVolume(float value)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void PlayBGMusic(AudioClip clip)
    {
        if (musicSource == null) return;

        if(_currentBgMusic != null && _currentBgMusic.isPlaying)
        {
            _currentBgMusic.Stop();
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
        _currentBgMusic = musicSource;
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSourcePrefab == null) return;
        AudioSource sfxSource = Instantiate(sfxSourcePrefab, transform);
        sfxSource.outputAudioMixerGroup = sfxSourcePrefab.outputAudioMixerGroup;
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
        Destroy(sfxSource.gameObject, clip.length);
    }
}