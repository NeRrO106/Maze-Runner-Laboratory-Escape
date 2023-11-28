using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GateSystem : MonoBehaviour{

    GameObject gate;

    float rangeDetection = 25f;
    bool isOpen = false;

    PlayerController player;
    Ads ads;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gate = GameObject.FindGameObjectWithTag("Gate1");
        ads = GetComponent<Ads>();
    }

    void Update(){
        float distance;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= rangeDetection){
            gate.transform.position = new Vector3(-254f, 30f, 1096f);
            isOpen = true;
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
        if(isOpen){
            StartCoroutine(GateDelay());
        }
    }

    IEnumerator GateDelay(){
        yield return new WaitForSeconds(30f);
        gate.transform.position = new Vector3(-254f, 10f, 1096f);
        isOpen = false;
        ads.ShowInterstitialAd();
    }
}
