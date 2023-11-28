using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour{
    float rangeDetection = 25f;
    PlayerController player;
    NavMeshAgent agent;
    public Animator animator;
    public AudioSource zombie;
    public GameObject enemy;
    float stopDistance;
    float damage = 10f;
    public float health = 100f;
    
    float attackDelay = 3f;
    bool canAttack = true;
    bool alreadydeath = false;
    bool doOnce = false;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        stopDistance = agent.stoppingDistance + 50f;
        float distance;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance > rangeDetection){
            if(alreadydeath == false){
                agent.SetDestination(player.transform.position);
                animator.SetFloat("Walking", 1f);
                if(!doOnce){
                    zombie.Play();
                    doOnce = true;
                }
            }
        }
        if(distance < stopDistance){
            animator.SetFloat("Walking", 0f);
            if(canAttack == true){
               StartCoroutine(AttackDelay());
                if(!doOnce){
                    zombie.Play();
                    doOnce = true;
                }
            }
        }
        if(health == 0){
            if(alreadydeath == false){
                Die();
            }
        }
    }
    IEnumerator AttackDelay(){
        canAttack = false;
        player.TakeDamage(damage);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    public void TakeDamage(float _damage){
        if(_damage <= health){
            health -= _damage;
        }
        else if(health <= _damage){
            health = 0f;
            Die();
        }
    }

    public void Die(){
        alreadydeath = true;
        enemy.SetActive(false);
        zombie.Stop();
        player.mobkill = player.mobkill+1;
    }
}