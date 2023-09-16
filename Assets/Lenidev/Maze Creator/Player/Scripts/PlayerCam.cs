using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Range(0f, 15f)] public float horizontalSensitivity;
    [Range(0f, 15f)] public float verticalSensitivity;

    public Transform orientaion;
    public Transform head;

    InputManager inputMgr;

    float yRotation;
    float xRotation;
    float sensitivityMultiplier=100f;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
        
    }

    private void OnEnable()
    {
        inputMgr = new InputManager();
        inputMgr.playerControls.Enable();
    }

    private void OnDestroy()
    {
        inputMgr.playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * horizontalSensitivity * sensitivityMultiplier;
        //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * verticalSensitivity * sensitivityMultiplier;


        float mouseX = inputMgr.MouseInput().x * .05f * Time.fixedDeltaTime * horizontalSensitivity * sensitivityMultiplier;
        float mouseY = inputMgr.MouseInput().y * .05f * Time.fixedDeltaTime * verticalSensitivity * sensitivityMultiplier;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Using the rotation values
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientaion.rotation = Quaternion.Euler(0f, yRotation, 0f);
        head.rotation =Quaternion.Euler(xRotation, orientaion.eulerAngles.y, 0f);
    }
}
