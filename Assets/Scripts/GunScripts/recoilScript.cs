using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoilScript : MonoBehaviour {

    [SerializeField] private GameObject cam;
    [SerializeField] private AnimationCurve recoilCurve;


    public void addRecoil(float recoil) {
        StopCoroutine(gunRecoil(recoil));
        StartCoroutine(gunRecoil(recoil));
    }

    private IEnumerator gunRecoil(float recoil) {
        float lastNum = 0f;
        float time = 0f;

        do {
            float num = recoilCurve.Evaluate(time);
            num = num * recoil;
            num = num - lastNum;
            Vector3 marker = cam.transform.eulerAngles;
            marker.x -= num;
            cam.transform.eulerAngles = marker;
            lastNum = num + lastNum;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (time < .5f);
        
        yield return null;
    }
}
