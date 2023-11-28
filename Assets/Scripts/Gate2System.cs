using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Gate2System : MonoBehaviour
{
    GameObject gate;

    PlayerController player;
    Ads ads;

    public WinMenu winn;

    float rangeDetection = 25f;

    string text = "";
    private TouchScreenKeyboard keyboard;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gate = GameObject.FindGameObjectWithTag("Gate2");
        ads = GetComponent<Ads>();
    }

    void Update(){
        float distance;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= rangeDetection){
            keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.NumberPad, false, false, true);
            text = keyboard.text;
            if(text == "2105"){
                var _openRotation = new Vector3(-260f, 30f, 995f);
                gate.transform.position = _openRotation;
                winn.Win();
            } 
            //autosave
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.dat";
            FileStream stream = File.Create(path);

            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
            //closeautosave 
            ads.ShowInterstitialAd();
        }
    }
}
