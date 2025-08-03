using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    public TMP_Dropdown graphicsDropdown;

    public Slider masterVol, musicVol, sfxVol;

    public AudioMixer mainAudioMixer;

    public GameObject container;

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
    }

    public void closeBtn()
    {
        container.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void settingBtn()
    {
        container.SetActive(true);
        Time.timeScale = 0; // Resume the game
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
