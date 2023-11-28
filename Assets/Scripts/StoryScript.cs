using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StoryScript : MonoBehaviour{
    public GameObject StoryScreen;
    public GameObject pausebutton;
    public GameObject attackbutton;
    public GameObject reloadbutton;
    public GameObject hud;
    public GameObject health;
    public GameObject armor;
    public GameObject joystick;
    public GameObject cjoystick;
    public GameObject crosshair;

    public GameObject weapon;

    PlayerController player;
    AudioSource click;
    Ads ads;

    void Start(){
        click = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ads = GetComponent<Ads>();
    }
    public void Story(){
        player.transform.position = new Vector3(11547f, 263f, -2566f);
        player.transform.rotation = Quaternion.Euler(0f, 45f, -30f);
        StoryScreen.SetActive(true);
        joystick.SetActive(false);
        pausebutton.SetActive(false);
        reloadbutton.SetActive(false);
        attackbutton.SetActive(false);
        hud.SetActive(false);
        health.SetActive(false);
        armor.SetActive(false);
        cjoystick.SetActive(false);
        weapon.SetActive(false);
        crosshair.SetActive(false);
        ads.ShowInterstitialAd();

    }
    public void Play(){
        click.Play();
        StoryScreen.SetActive(false);
        joystick.SetActive(true);
        pausebutton.SetActive(true);
        reloadbutton.SetActive(true);
        attackbutton.SetActive(true);
        hud.SetActive(true);
        health.SetActive(true);
        armor.SetActive(true);
        cjoystick.SetActive(true);
        weapon.SetActive(true);
        crosshair.SetActive(true);

        player.transform.position = new Vector3(-257f, -5f, 1030f);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.story = 1;
        player.bulletsInTheGun = 50f;
        player.bulletInMagasin = 400f;
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
}
