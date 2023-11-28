using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public float health;
    public float maxhealth;
    public float armor;
    public float maxarmor;
    public float bulletsInTheGun;
    public float bulletInMagasin;
    public int story;

    public bool gang1activ;
    public bool gang2activ;
    public bool gang3activ;
    public bool gang4activ;
    public bool gang5activ;
    public bool gang6activ;
    public bool gang7activ;
    public bool gang8activ;
    public bool gang9activ;
    public bool gang10activ;


    public float[] position;

    public PlayerData (PlayerController player){
        
        health = player.health;
        maxhealth = player.maxhealth;
        armor = player.armor;
        maxarmor = player.maxarmor;
        bulletsInTheGun = player.bulletsInTheGun;
        bulletInMagasin = player.bulletInMagasin;
        story = player.story;

        gang1activ = player.gang1activ;
        gang2activ = player.gang2activ;
        gang3activ = player.gang3activ;
        gang4activ = player.gang4activ;
        gang5activ = player.gang5activ;
        gang6activ = player.gang6activ;
        gang7activ = player.gang7activ;
        gang8activ = player.gang8activ;
        gang9activ = player.gang9activ;
        gang10activ = player.gang10activ;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}
