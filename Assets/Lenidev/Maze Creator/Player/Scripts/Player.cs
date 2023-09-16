using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IPickup
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    public float groundDrag;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Jump")]
    public float jumpForce;
    [Range(0f, 1f)] public float airMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask mask;


    bool grounded=true;

    float hInput;
    float vInput;

    Vector3 moveDir;

    Rigidbody rb;

    InventoryManager inventoryMgr;
    InputManager inputMgr;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        inventoryMgr = GetComponent<InventoryManager>();
    }

    private void OnEnable()
    {
        inputMgr = new InputManager();
        inputMgr.playerControls.Enable();
        inputMgr.JumpPerformed += Jump;
    }

    private void OnDestroy()
    {
        inputMgr.playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {


        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.5f, mask);


        HandleInput();
        ClampSpeed();


        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //Jump();
    }


    void MovePlayer()
    {
        moveDir = ((orientation.forward * inputMgr.MovementInput().y) + (orientation.right * inputMgr.MovementInput().x));
        if (grounded) 
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        rb.rotation = orientation.rotation;
    }

    void HandleInput()
    {
        //hInput = Input.GetAxisRaw("Horizontal");
        //vInput = Input.GetAxisRaw("Vertical");
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //if (Input.GetKey(jumpKey) & grounded)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //    rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
        //}

        if(grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ClampSpeed()
    {
        Vector3 groundVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(groundVel.magnitude > moveSpeed)
        {
            Vector3 lim = groundVel.normalized * moveSpeed;
            rb.velocity = new Vector3(lim.x, rb.velocity.y, lim.z);
        }
    }



    public void Pick(SOPickup pickup)
    {
        inventoryMgr.addItem(pickup);
    }


}
