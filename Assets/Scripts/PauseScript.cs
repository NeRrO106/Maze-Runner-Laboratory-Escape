using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PauseScript : MonoBehaviour{

    public string sceneName;

    public GameObject PauseScreen;
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
    public PlayerController player;

    void Start(){
        click = GetComponent<AudioSource>();
        ads = GetComponent<Ads>();
    }

    public void MainMenu(){
        click.Play();
        //autosave
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        //closeautosave
        SceneManager.LoadScene(sceneName);
    }

    public void Pause(){
        Time.timeScale = 0;
        click.Play();
        PauseScreen.SetActive(true);
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
        //autosave
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        //closeautosave
    }
    public void Resume(){
        Time.timeScale = 1;
        click.Play();
        PauseScreen.SetActive(false);
        Joystick.SetActive(true);
        PauseButton.SetActive(true);
        Armor.SetActive(true);
        Health.SetActive(true);
        hud.SetActive(true);
        attackbutton.SetActive(true);
        reloadbutton.SetActive(true);
        CJoystick.SetActive(true);
        crosshair.SetActive(true);
        ads.ShowInterstitialAd();
    }
    public void SavePlayer(PlayerController player){
        click.Play();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
