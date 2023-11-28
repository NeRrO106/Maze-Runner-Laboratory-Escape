using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScrip : MonoBehaviour
{
	EnemyController enemy;
	float damage = 20f;
    void Start(){
		enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
	}
	void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != null){
			enemy.TakeDamage(damage);
		}
    }
}
