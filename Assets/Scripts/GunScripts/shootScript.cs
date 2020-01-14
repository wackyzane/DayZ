using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform barrel;

    [SerializeField] private float bulletForce;
    [SerializeField] private float recoil;

    [SerializeField] private float magazineSize;
    private float currentMagazine;

    [SerializeField] private recoilScript recoilScript;

    void Awake() {
        currentMagazine = magazineSize;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (currentMagazine > 0) {
                GameObject spawn = Instantiate(BulletPrefab, barrel.transform.position, Quaternion.identity);
                Rigidbody spawnRb = spawn.GetComponent<Rigidbody>();
                spawnRb.AddForce(transform.forward * bulletForce);
                // Add recoil
                recoilScript.addRecoil(recoil);
                currentMagazine -= 1;
            } else {
                Debug.Log("Click, Click, Click");
            }
        }

        // Reload, Add Customize Reload Button
        if (Input.GetKey("r")) {
            currentMagazine = magazineSize;
            Debug.Log(currentMagazine);
        }
    }
}
