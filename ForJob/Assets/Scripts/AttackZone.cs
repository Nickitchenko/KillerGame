using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private Zombie zombie;

    private void Start()
    {
        zombie=transform.parent.GetComponent<Zombie>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().HealthChange(-(Random.Range(zombie.damageMin, zombie.damageMax)));
        }
    }
}
