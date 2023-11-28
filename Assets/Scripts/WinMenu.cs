using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class WinMenu : MonoBehaviour{

    public string sceneName;

    public GameObject WinScreen;
    public GameObject PauseButton;
    public GameObject hud;
    public GameObject attackbutton;
    public GameObject reloadbutton;
    public GameObject Health;
    public GameObject Armor;
    public GameObject Joystick;
    public GameObject CJoystick;
    public GameObject crosshair;

    AudioSource click;
    Ads ads;

    void Start(){
        click = GetComponent<AudioSource>();
        ads = GetComponent<Ads>();
    }

    public void Win(){
        ads.ShowInterstitialAd();
        WinScreen.SetActive(true);
        Joystick.SetActive(false);
        PauseButton.SetActive(false);
        Armor.SetActive(false);
        Health.SetActive(false);
        hud.SetActive(false);
        attackbutton.SetActive(false);
        reloadbutton.SetActive(false);
        CJoystick.SetActive(false);
        crosshair.SetActive(false);
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    public void MainMenu(){
        click.Play();
        SceneManager.LoadScene(sceneName);
    }


}
