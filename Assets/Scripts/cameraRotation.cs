using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour {

    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp = 0f;

    void Awake() {
        lockCursor();
    }

    // Update is called once per frame
    void Update() {
        CameraRotation();
    }

    private void lockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CameraRotation() {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90f) {
            xAxisClamp = 90f;
            mouseY = 0f;
            clampXAxisRotationToValue(270f);
        } else if (xAxisClamp < -90f) {
            xAxisClamp = -90f;
            mouseY = 0f;
            clampXAxisRotationToValue(90f);
        }
        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void clampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
