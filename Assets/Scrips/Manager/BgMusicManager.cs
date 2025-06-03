using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> bgMusicClips;

    private string _currentSceneName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
        PlayBackGroundMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentSceneName = scene.name;
        PlayBackGroundMusic();
    }

    void PlayBackGroundMusic()
    {
        if (_currentSceneName == SceneName.Menu.ToString())
        {
            SoundManager.Instance.PlayBGMusic(bgMusicClips[0]);
        }
        else
        {
            SoundManager.Instance.PlayBGMusic(bgMusicClips[1]);
        }
    }
}

public enum SceneName
{
    GameScene,
    Menu,
}