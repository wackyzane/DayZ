using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform barrel;

    [SerializeField] private float bulletForce;
    [SerializeField] private float recoil;
    [SerializeField] private float attackSpeed;

    [SerializeField] private int magazineSize;
    private int currentMagazine;

    [SerializeField] private recoilScript recoilScript;

    private string fireModeHotkey = "t";
    [SerializeField] private List<string> fireMode;
    private int fireModeCount = 0;

    private bool burst = false;

    void Awake() {
        List<string> fireMode = new List<string>();
        currentMagazine = magazineSize;
        addFireModes();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (currentMagazine > 0) {
                Debug.Log(fireModeCount);
                if (fireModeCount == 0) {
                    singleAutoFire(.25f);
                } else if (fireModeCount == 1) {
                    if (!burst) {
                        StartCoroutine(burstFire());
                    }
                }
            } else {
                Debug.Log("Click, Click, Click");
            }
        }

        if (Input.GetMouseButton(0)) {
            if (currentMagazine > 0) {
                if (fireModeCount == 2) {
                    singleAutoFire(.05f);
                }
            }
        }

        if (Input.GetKeyDown(fireModeHotkey)) {
            if (fireModeCount < 2) {
                fireModeCount += 1;
                Debug.Log(fireMode[fireModeCount]);
            } else {
                fireModeCount = 0;
                Debug.Log(fireMode[fireModeCount]);
            }
        }

        // Reload, Add Customize Reload Button
        if (Input.GetKey("r")) {
            currentMagazine = magazineSize;
            Debug.Log(currentMagazine);
        }

        if (attackSpeed > 0f) {
            attackSpeed -= Time.deltaTime;
        } else {
            attackSpeed = 0f;
        }
    }

    private void singleAutoFire(float add) {
        if (attackSpeed <= 0f) {
            shoot();
            recoilScript.addRecoil(recoil);
            // Change to attackSpeed of weapon
            if (fireModeCount == 1) {
                attackSpeed += add;
            } else {
                attackSpeed += add;
            }
            
            currentMagazine -= 1;
        }
    }

    private IEnumerator burstFire() {
        burst = true;
        int count = 0;
        while (count < 3) {
            if (attackSpeed <= 0f) {
                shoot();
                recoilScript.addRecoil(recoil);
                attackSpeed += 0.05f;
                currentMagazine -= 1;
                count += 1;
            }
            yield return new WaitForEndOfFrame();
        }

        burst = false;
        yield return null;
    }

    private void autoFire() {

    }

    private void addFireModes() {
        fireMode.Add("Semi");
        fireMode.Add("Burst");
        fireMode.Add("Auto");
    }

    private void shoot() {
        GameObject spawn = Instantiate(BulletPrefab, barrel.transform.position, Quaternion.identity);
        Rigidbody spawnRb = spawn.GetComponent<Rigidbody>();
        spawnRb.AddForce(transform.forward * bulletForce);
    }

}
