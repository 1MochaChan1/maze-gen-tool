using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
    public LayerMask groundMask;


    bool grounded;

    float hInput;
    float vInput;

    Vector3 moveDir;

    Rigidbody rb;

    InventoryManager inventoryMgr;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        inventoryMgr = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.5f, groundMask);
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
        //rb.transform.rotation = Vector3.Lerp();
        MovePlayer();
        Jump();
    }


    void MovePlayer()
    {
        moveDir = ((orientation.forward * vInput) + (orientation.right * hInput));
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
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }

    void Jump()
    {
        if (Input.GetKey(jumpKey) & grounded)
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
