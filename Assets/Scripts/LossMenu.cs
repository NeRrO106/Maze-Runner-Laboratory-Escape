using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class LossMenu : MonoBehaviour{
    
    public string sceneName;
    public string sceneName2;

    public GameObject LossScreen;
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

    public void Loss(){
        LossScreen.SetActive(true);
        Joystick.SetActive(false);
        PauseButton.SetActive(false);
        Armor.SetActive(false);
        Health.SetActive(false);
        hud.SetActive(false);
        attackbutton.SetActive(false);
        reloadbutton.SetActive(false);
        CJoystick.SetActive(false);
        crosshair.SetActive(false);
        ads.ShowInterstitialAd();
    }

    public void TryAgain(){
        click.Play();
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu(){
        click.Play();
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        SceneManager.LoadScene(sceneName2);
    }
}
