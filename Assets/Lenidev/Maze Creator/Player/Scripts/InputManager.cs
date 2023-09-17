using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{ 
    public PlayerControls playerControls = new PlayerControls();

    public event Action<InputAction.CallbackContext> JumpPerformed
    {
        add
        {
            playerControls.GroundMovement.Jump.performed += value;
        }

        remove
        {
            playerControls.GroundMovement.Jump.performed -= value;
        }
    }

    public event Action<InputAction.CallbackContext> ShootStart
    {
        add
        {
            playerControls.GroundMovement.Shoot.performed += value;
        }

        remove
        {
            playerControls.GroundMovement.Shoot.performed -= value;
        }
    }

    public event Action<InputAction.CallbackContext> ShootStop
    {
        add
        {
            playerControls.GroundMovement.Shoot.canceled += value;
        }

        remove
        {
            playerControls.GroundMovement.Shoot.canceled -= value;
        }
    }

    public Vector2 MovementInput()
    {
        return playerControls.GroundMovement.HorizontalMovement.ReadValue<Vector2>();
    }




    public Vector2 MouseInput()
    {
        //return new Vector2(
        //    playerControls.MouseMovement.Horizontal.ReadValue<float>(),
        //    playerControls.MouseMovement.Vertical.ReadValue<float>());

        return playerControls.MouseMovement.Look.ReadValue<Vector2>();
    }
}
