using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int currentWeapon;
    public KeyCode[] keysToSwitch;

    public Weapon[] weapons;
    public bool canShoot=true;
    [SerializeField] private Transform hand;

    private Animator animator;

    public GameObject shotEffectPrefab;
    private ParticleSystem shotVFX;

    public AudioSource audioSource;

    public TMP_Text bulletsText;
    public TMP_Text allBulletsText;
    public Image weaponImage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        for(int i=0; i<hand.childCount;i++)
        {
            weapons[i]=hand.GetChild(i).GetComponent<Weapon>();
        }
        weaponImage.sprite = weapons[currentWeapon].weaponSprite;
        shotVFX = weapons[currentWeapon].transform.GetChild(0).Find("VFX").GetComponent<ParticleSystem>();
        RefreshUI();
    }

    private void Update()
    {
        CheckSwitch();
        if(Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        if(Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }

    private void Shot()
    {
        if (canShoot)
        {
            if (weapons[currentWeapon].bullets > 0)
            {
                shotVFX.Play();
                audioSource.PlayOneShot(weapons[currentWeapon].shotClips[Random.Range(0, weapons[currentWeapon].shotClips.Length)]);
                weapons[currentWeapon].bullets--;
                RefreshUI();

                //animator.CrossFade("KalashShot", 0.05f);
                animator.Play(weapons[currentWeapon].ShotAnimation);

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hit, weapons[currentWeapon].shotDistance))
                {
                    GameObject newParticle = Instantiate(shotEffectPrefab, hit.point, Quaternion.identity, hit.transform);
                    newParticle.transform.LookAt(transform.position);
                    Destroy(newParticle, 10f);

                    if (hit.collider.GetComponent<Zombie>())
                    {
                        hit.collider.GetComponent<Zombie>().ChangeZombieHealth(weapons[currentWeapon].weaponDamage);
                    }

                }
                CheckBullets();
            }
        }
    }

    private void CheckBullets()
    {
        if (weapons[currentWeapon].bullets == 0)
        {
            //if (weapons[currentWeapon].bulletsAll >= weapons[currentWeapon].bulletsMax)
            //{
            //    //animator.Play("ReloadGun");
            //    audioSource.PlayOneShot(weapons[currentWeapon].reloadingClip, 0.8f);
            //    weapons[currentWeapon].bullets = weapons[currentWeapon].bulletsMax;
            //    weapons[currentWeapon].bulletsAll -= weapons[currentWeapon].bulletsMax;
            //}
            //else
            //{
            //    //animator.Play("ReloadGun");
            //    audioSource.PlayOneShot(weapons[currentWeapon].reloadingClip, 0.8f);
            //    weapons[currentWeapon].bullets = weapons[currentWeapon].bulletsAll;
            //    weapons[currentWeapon].bulletsAll = 0;
            //}
            Reload();
            //RefreshUI();
        }
    }

    private void Reload()
    {
        if (weapons[currentWeapon].bulletsAll == 0)
        {
            return;
        }
        else
        {
            //animator.Play("ReloadGun");
            audioSource.PlayOneShot(weapons[currentWeapon].reloadingClip, 0.8f);
            StartCoroutine(CantShot(3f));
            weapons[currentWeapon].bulletsAll += weapons[currentWeapon].bullets;
            weapons[currentWeapon].bulletsAll -= weapons[currentWeapon].bulletsMax;
            weapons[currentWeapon].bullets = weapons[currentWeapon].bulletsMax; RefreshUI();
        }
    }

    private IEnumerator CantShot(float timer)
    {
        canShoot = false;
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }


    private void CheckSwitch()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentWeapon++;
                //animator.Play("SwitchWeapon");
                StartCoroutine(SwitchTimer());
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentWeapon--;
                //animator.Play("SwitchWeapon");
                StartCoroutine(SwitchTimer());
            }

            currentWeapon = Mathf.Clamp(currentWeapon, 0, 1);

            SwitchWeapon();
        }


        for (int i = 0; i < keysToSwitch.Length; i++)
        {
            if (Input.GetKeyDown(keysToSwitch[i]))
            {
                if (currentWeapon != i)
                {
                    currentWeapon = i;
                    //animator.Play("SwitchWeapon");
                    StartCoroutine(SwitchTimer());
                }
                break;
            }
        }
    }

    private IEnumerator SwitchTimer()
    {
        yield return new WaitForSeconds(0.5f);
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        for (int j = 0; j < weapons.Length; j++)
        {
            weapons[j].gameObject.SetActive(false);
        }

        weapons[currentWeapon].gameObject.SetActive(true);
        weaponImage.sprite = weapons[currentWeapon].weaponSprite;
        shotVFX = weapons[currentWeapon].transform.GetChild(0).Find("VFX").GetComponent<ParticleSystem>();

        RefreshUI();
    }

    public void RefreshUI()
    {
        bulletsText.text = weapons[currentWeapon].bullets + "/" + weapons[currentWeapon].bulletsMax;
        allBulletsText.text = weapons[currentWeapon].bulletsAll.ToString();
    }
}
