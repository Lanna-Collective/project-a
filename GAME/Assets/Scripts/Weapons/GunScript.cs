using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public bool isAuto;

    public float damage = 10f;
    public float range = 200f;
    public float fireRatePerSecond = 6f;
    public ParticleSystem muzzleFlash;
    public float clipSize = 30;
    public float currAmmo = 30f;
    public float ammoStored = 60f;
    public Camera playerCam;
    public GameObject m_shotPrefab;

    public AudioSource[] sounds;
    public AudioSource gunAudio;
    public AudioSource gunClick;

    private float timer = 0f;
    //private float autoTimer = 0f;
    private float lastShot = 0f;
    //private bool firstShot = true;
    bool onCooldown = false;

    private void Start()
    {
        sounds = GetComponents<AudioSource>();
        gunAudio = sounds[0];
        gunClick = sounds[1];
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        gunAudio.Play();

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name);
            Shootable targetObject = hit.transform.GetComponent<Shootable>();
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<BulletBehavior>().setTarget(hit.point, targetObject, damage, gameObject);
            GameObject.Destroy(laser, 1.5f);
        }
        currAmmo--;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isAuto)
        {
            FullAutoUpdate();
        }
        else
        {
            SemiAutoUpdate();
        }
    }

    // For full auto guns
    void FullAutoUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            FireTimer();
        }
    }

    // For semi auto guns
    void SemiAutoUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireTimer();
        }
    }

    void FireTimer()
    {
        if (currAmmo > 0)
        {
            if (timer > ((1.0f / fireRatePerSecond) + lastShot))
            {
                Shoot();
                lastShot = timer;
            }
        }
        else
        {
            gunEmpty();
        }
    }

    void gunEmpty()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunClick.Play();
        }
    }
}