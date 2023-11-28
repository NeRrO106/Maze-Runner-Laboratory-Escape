using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    string gameId = "4068109";
    string adid = "video";
    bool testMode = false;
    bool adstart = false;
    
    void Start(){
        Advertisement.Initialize(gameId, testMode);
    }


    public void ShowInterstitialAd() {
        if (Advertisement.IsReady(adid) && adstart==false) {
            Advertisement.Show(adid);
            adstart = true;
        } 
    }
    
}
