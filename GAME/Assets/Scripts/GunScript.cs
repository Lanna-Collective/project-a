using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;

    public Camera playerCam;

    void Shoot() {
        RaycastHit hitInfo;
        muzzleFlash.Play();

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, range)) {
            // Debug.Log(hitInfo.transform.name);

            Shootable target = hitInfo.transform.GetComponent<Shootable>();
            if (target != null) {
                target.TakeDamage(damage);
                GameObject impactGO = Instantiate(target.impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO, 2f);
            }

        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }

    }
}