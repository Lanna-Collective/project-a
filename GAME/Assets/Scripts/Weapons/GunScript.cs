using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRatePerSecond = 6f;
    public ParticleSystem muzzleFlash;
    public float clipSize = 30;
    public float currAmmo = 30f;
    public float ammoStored = 60f;

    public Camera playerCam;

    private float timer = 0f;
    private bool firstShot = true;
    private AudioSource gunAudio;

    private void Start() {
        gunAudio = GetComponent<AudioSource>();
    }

    void Shoot() {
        RaycastHit hit;
        muzzleFlash.Play();
        gunAudio.Play();

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);

            Shootable target = hit.transform.GetComponent<Shootable>();
            if (target != null) {
                target.TakeDamage(damage);
                GameObject impactGO = Instantiate(target.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
        currAmmo--;

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButton("Fire1")) {
            //start timer
            timer += Time.deltaTime;
            if (firstShot) {
                Shoot();
                firstShot = false;
            }

            if (timer >= 1.0f / fireRatePerSecond) {
                Shoot();
                timer = 0f;
            }

        } else {
            timer = 0f;
            firstShot = true;
        }

    }
}