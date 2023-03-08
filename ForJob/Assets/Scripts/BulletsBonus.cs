using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerShooting player = other.gameObject.GetComponent<PlayerShooting>();
            //other.gameObject.GetComponent<PlayerShooting>().weapons[other.gameObject.
            //    GetComponent<PlayerShooting>().currentWeapon].bulletsAll += 30;
            player.weapons[player.currentWeapon].bulletsAll += player.weapons[player.currentWeapon].bulletsMax;
            player.RefreshUI();
            Destroy(gameObject);
        }
    }
}
