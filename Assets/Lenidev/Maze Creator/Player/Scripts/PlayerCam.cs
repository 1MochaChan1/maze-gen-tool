using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Range(0f, 15f)] public float horizontalSensitivity;
    [Range(0f, 15f)] public float verticalSensitivity;

    public Transform orientaion;

    float yRotation;
    float xRotation;
    float sensitivityMultiplier=100f;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * horizontalSensitivity * sensitivityMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * verticalSensitivity * sensitivityMultiplier;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Using the rotation values
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientaion.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
