using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Volume Texts")]
    public TextMeshProUGUI masterText;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;

    [SerializeField] private Button _exitBtn;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // Update UI text at start
        UpdateMasterText(masterSlider.value);
        UpdateMusicText(musicSlider.value);
        UpdateSFXText(sfxSlider.value);

        if(_exitBtn != null)
        {
            _exitBtn.onClick.AddListener(OnExitBtnOnclick);
        }
    }

    public void OnMasterVolumeChanged(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
        UpdateMasterText(value);
    }
    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
        UpdateMusicText(value);
    }
    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        UpdateSFXText(value);
    }

    private void UpdateMasterText(float value)
    {
        masterText.text = Mathf.RoundToInt(value * 100) + "%";
    }
    private void UpdateMusicText(float value)
    {
        musicText.text = Mathf.RoundToInt(value * 100) + "%";
    }
    private void UpdateSFXText(float value)
    {
        sfxText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnExitBtnOnclick()
    {
        LevelManager.Instance.IsPause = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}