using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shotDistance;
    public float shotTimer;

    public int bullets;
    public int bulletsMax;
    public int bulletsAll;

    public Sprite weaponSprite;
    public string ShotAnimation;

    public float weaponDamage;

    public AudioClip[] shotClips;
    public AudioClip reloadingClip;
}
