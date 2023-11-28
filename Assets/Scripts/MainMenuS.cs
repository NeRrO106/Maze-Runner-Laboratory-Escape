using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuS : MonoBehaviour{
    public string sceneName;
    AudioSource click;

    public GameObject SettingMenu;

    public AudioMixer themixer;
    public Slider masterslide, musicslide, sfxslide;

    void Start(){
        click = GetComponent<AudioSource>();

        //audio
        if(PlayerPrefs.HasKey("MasterVolume")){
            themixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            masterslide.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if(PlayerPrefs.HasKey("MusicVolume")){
            themixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            musicslide.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if(PlayerPrefs.HasKey("SFXVolume")){
            themixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            sfxslide.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void PlayGame(){
        click.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings(){
        click.Play();
        SettingMenu.SetActive(true);
    }
    public void CloseSettings(){
        click.Play();
        SettingMenu.SetActive(false);
    }

    public void ApplySetting(){
        click.Play();
        PlayerPrefs.SetFloat("MasterVolume", masterslide.value);
        PlayerPrefs.SetFloat("MusicVolume", musicslide.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxslide.value);
    }
    public void SetMaster(){
        click.Play();
        themixer.SetFloat("MasterVolume", masterslide.value);
    }
    public void SetMusic(){
        click.Play();
        themixer.SetFloat("MusicVolume", musicslide.value);
    }
    public void SetSFX(){
        click.Play();
        themixer.SetFloat("SFXVolume", sfxslide.value);
    }

    public void QuitGame(){
        click.Play();
        Application.Quit();
    }
}
