using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerController : MonoBehaviour{
    public VariableJoystick joystick;
    public VariableJoystick cjoystick;
    public Animator animator;
    public Transform player;
    public PlayerController playersave;
    EnemyController enemy;
    Ads ads;
    public AudioSource shoot;
    public AudioSource reload;
    public AudioSource ambient;
    public Fire fire;

    float speed = 5f;

    public int story = 0;
    public StoryScript sscript;
    public GameObject StoryScreen;

    public Text healthtext;
    public float health = 100f;
    public float maxhealth = 100f;
    public RectTransform Healthbar;

    public Text armortext;
    public float armor = 100f;
    public float maxarmor = 100f;
    public RectTransform ArmorBar;

    public Text guntext;
    public float bulletsInTheGun = 50f;
    public float bulletInMagasin = 400f;
    public GameObject weapon;
    public float damage = 20f;
    public float range = 50f;
    public Camera fpsCam;

    public LossMenu lost;

    bool showads = true;
    float adstimedelay = 40f;

    //mobsystem
    public GameObject Gang1;
    public bool gang1activ = true;
    public GameObject Gang2;
    public bool gang2activ = false;
    public GameObject Gang3;
    public bool gang3activ = false;
    public GameObject Gang4;
    public bool gang4activ = false;
    public GameObject Gang5;
    public bool gang5activ = false;
    public GameObject Gang6;
    public bool gang6activ = false;
    public GameObject Gang7;
    public bool gang7activ = false;
    public GameObject Gang8;
    public bool gang8activ = false;
    public GameObject Gang9;
    public bool gang9activ = false;
    public GameObject Gang10;
    public bool gang10activ = false;

    public int mobkill;

    Vector3 horizontal;


    void Start(){
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        fire = GameObject.FindGameObjectWithTag("BulletSpawn").GetComponent<Fire>();
        animator = GetComponent<Animator>();
        ads = GetComponent<Ads>();
        playersave = GetComponent<PlayerController>();
        if(story == 0){
            player.transform.position = new Vector3(11547f, 263f, -2566f);
            player.transform.rotation = Quaternion.Euler(0f, 45f, -30f);
            sscript.Story();
        }
        Loading();
        ambient.Play();
        mobkill = 0;
    }

    void Awake () {
        Application.targetFrameRate = 60;
    }

    public void Loading(){
        PlayerData data = SavingSystem.LoadPlayer();

        health = data.health;
        maxhealth = data.maxhealth;
        armor = data.armor;
        maxarmor = data.maxarmor;
        story = data.story;

        gang1activ = data.gang1activ;
        gang2activ = data.gang2activ;
        gang3activ = data.gang3activ;
        gang4activ = data.gang4activ;
        gang5activ = data.gang5activ;
        gang6activ = data.gang6activ;
        gang7activ = data.gang7activ;
        gang8activ = data.gang8activ;
        gang9activ = data.gang9activ;
        gang10activ = data.gang10activ;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        if(story == 0){
            player.transform.position = new Vector3(11547f, 263f, -2566f);
            player.transform.rotation = Quaternion.Euler(0f, 45f, -30f);
            sscript.Story();
            enemy.zombie.Stop();
        }
        if(story == 1){
            sscript.Play();
            StoryScreen.SetActive(false);
            player.transform.position = new Vector3(position.x, position.y, position.z);
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bulletsInTheGun = data.bulletsInTheGun;
            bulletInMagasin = data.bulletInMagasin;
            Mobs();
        }
    }

    private void FixedUpdate(){
        transform.Translate(Vector3.right * joystick.Horizontal * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * joystick.Vertical * speed * Time.deltaTime);
    }

    void Update()
    {
        Animation();
        KillMobs();
        Rotation();
        UpdateCanvas();
        ShowInterstitialAdds();

    }
    void Animation(){
        if(joystick.Horizontal != 0 || joystick.Vertical != 0){
            animator.SetFloat("Walking", 1f);
        }
        if(joystick.Horizontal == 0 || joystick.Vertical == 0){
            animator.SetFloat("Walking", 0f);
        }
    }
    void UpdateCanvas(){
        health = Mathf.Clamp(health, 0f, maxhealth);
        Healthbar.localScale = new Vector3(health / maxhealth, 1f, 1f);
        healthtext.text = health +"/" + maxhealth.ToString();

        armor = Mathf.Clamp(armor, 0f, maxarmor);
        ArmorBar.localScale = new Vector3(armor / maxarmor, 1f, 1f);
        armortext.text = armor +"/" + maxarmor.ToString();

        guntext.text = bulletsInTheGun + "/" + bulletInMagasin.ToString();
    }
    void Rotation(){
        if(cjoystick.Horizontal != 0){
            if(cjoystick.Horizontal > 0){
                horizontal.y += Input.GetAxis("Mouse X") * 10f;
            }
            else{
                horizontal.y += Input.GetAxis("Mouse X") * 5f;
            } 
        }
        if(cjoystick.Vertical !=0){
            if(cjoystick.Vertical>0){
                horizontal.x += Input.GetAxis("Mouse Y") * 15f;
            }
            else{
                horizontal.x += Input.GetAxis("Mouse Y") * 15f;
            }
        }
        player.transform.rotation = Quaternion.Euler(+horizontal.x, +horizontal.y, horizontal.z);
        
    }
    public void Shooting(){
        if(bulletsInTheGun > 0){
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
                fire.Shoot();
                if(hit.transform.tag == "Enemy"){
                    enemy.TakeDamage(damage);
                }
            }

            animator.SetTrigger("Fire");
            shoot.Play();
            bulletsInTheGun -= 1;
            guntext.text = bulletsInTheGun + "/" + bulletInMagasin.ToString();
        }
    }
    public void Reloading(){
        if(bulletsInTheGun < 50 && bulletInMagasin >= 50){
            animator.SetTrigger("Reloading");
            if(bulletsInTheGun == 0){
                bulletInMagasin = bulletInMagasin - 50;
            }
            bulletInMagasin = bulletInMagasin - bulletsInTheGun;
            bulletsInTheGun = 50;
            reload.Play();
        }
    }
    void ShowInterstitialAdds(){
        if(showads == true){
            StartCoroutine(ShowAdds());
        }
    }
    void UpdateGun(){
        mobkill = 0;
        health = 100f;
        armor = 100f;
        bulletInMagasin = 400f;
        bulletsInTheGun = 50f;
    }
    void Mobs(){
        if(gang1activ==true){
            Gang1.SetActive(true);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang2activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(true);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang3activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(true);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang4activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(true);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang5activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(true);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang6activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(true);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang7activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(true);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang8activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(true);
            Gang9.SetActive(false);
            Gang10.SetActive(false);
        }
        if(gang9activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(true);
            Gang10.SetActive(false);
        }
        if(gang10activ==true){
            Gang1.SetActive(false);
            Gang2.SetActive(false);
            Gang3.SetActive(false);
            Gang4.SetActive(false);
            Gang5.SetActive(false);
            Gang6.SetActive(false);
            Gang7.SetActive(false);
            Gang8.SetActive(false);
            Gang9.SetActive(false);
            Gang10.SetActive(true);
        }
    }
    void KillMobs(){
        if(mobkill >= 6 && gang1activ == true){
            Gang1.SetActive(false);
            Gang2.SetActive(true);
            gang1activ = false;
            gang2activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 6 && gang2activ == true){
            Gang2.SetActive(false);
            Gang3.SetActive(true);
            gang2activ = false;
            gang3activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 6 && gang3activ == true){
            Gang3.SetActive(false);
            Gang4.SetActive(true);
            gang3activ = false;
            gang4activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 5 && gang4activ == true){
            Gang4.SetActive(false);
            Gang5.SetActive(true);
            gang4activ = false;
            gang5activ = true;
            ads.ShowInterstitialAd();
            UpdateGun();
        }
        if(mobkill >= 5 && gang5activ == true){
            Gang5.SetActive(false);
            Gang6.SetActive(true);
            gang5activ = false;
            gang6activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 5 && gang6activ == true){
            Gang6.SetActive(false);
            Gang7.SetActive(true);
            gang6activ = false;
            gang7activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 6 && gang7activ == true){
            Gang7.SetActive(false);
            Gang8.SetActive(true);
            gang7activ = false;
            gang8activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 5 && gang8activ == true){
            Gang8.SetActive(false);
            Gang9.SetActive(true);
            gang8activ = false;
            gang9activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 14 && gang9activ == true){
            Gang9.SetActive(false);
            Gang10.SetActive(true);
            gang9activ = false;
            gang10activ = true;
            ads.ShowInterstitialAd();
            AutoSave();
            UpdateGun();
        }
        if(mobkill >= 6 && gang10activ == true){
            Gang10.SetActive(false);
            UpdateGun();
            gang10activ = false;
            ads.ShowInterstitialAd();
            AutoSave();
        }
    }
    public void TakeDamage(float _damage){
        if(armor != 0){
            armor -= _damage;
        }
        else if(health != 0 && armor == 0){
            health -= _damage;
        }
        if(health == 0 && armor == 0){
            Die();
        }
    }
    void Die(){
        story = 0;
        enemy.animator.SetFloat("Walking", 0f);
        lost.Loss();
        ambient.Stop();
        reload.Stop();
        shoot.Stop();
        Time.timeScale = 0;
        ads.ShowInterstitialAd();
    }
    void AutoSave(){
        //autosave
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(playersave);

        formatter.Serialize(stream, data);
        stream.Close();
        //closeautosave
    }
    IEnumerator ShowAdds(){
        showads = false;
        ads.ShowInterstitialAd();
        yield return new WaitForSeconds(adstimedelay);
        showads = true;
    }
}
