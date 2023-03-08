using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float health;
    public float speed;
    public float speedMax;
    public float attackDistance;

    public float damageMin;
    public float damageMax;
    private bool isKicking;
    public float dieImpuls;


    private Transform player;
    private NavMeshAgent agent;
    private Animator animatorVFX;
    public GameObject attackTrigger;
    public GameObject ragdollPrefab;

    public static Action<int> onScoreChanged;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent=GetComponent<NavMeshAgent>();
        animatorVFX=transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (health > 0)
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= attackDistance && isKicking == false)
            {
                Attack();
            }

            agent.speed = speed;
        }
    }

    public void Attack()
    {
        animatorVFX.Play("ZombieKick");
        isKicking = true;
        speed = 0;

        StartCoroutine("AttackTriggerActivation");
        StartCoroutine("KickCullDown");
    }

    IEnumerator AttackTriggerActivation()
    {
        yield return new WaitForSeconds(1.1f);
        attackTrigger.SetActive(true);
    }

    IEnumerator KickCullDown()
    {
        yield return new WaitForSeconds(2.1f);
        isKicking = false;
        speed = speedMax;
        attackTrigger.SetActive(false);
    }

    public void ChangeZombieHealth(float damage)
    {
        health-=damage;
        if (health <= 0)
        {
            ZombieDie();
        }
    }   

    private void ZombieDie()
    {
        onScoreChanged?.Invoke(1);
        Rigidbody spawnedObject = Instantiate(ragdollPrefab.GetComponent<Rigidbody>(), transform.position, Quaternion.identity); // создание префаба

        Vector3 forceDirection = -spawnedObject.transform.forward; // определение направления силы (назад)

        spawnedObject.AddForce(forceDirection * dieImpuls);

        agent.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject, 10f);
    }

}
