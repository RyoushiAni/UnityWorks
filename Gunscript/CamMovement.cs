using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform CamBody;

    float xRotation = 0f;
    public Vector3 screenPosition;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation  = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        CamBody.Rotate(Vector3.up * mouseX);

        screenPosition = Input.mousePosition;
    }
}
