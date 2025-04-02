using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAction : MonoBehaviour
{
    private InputSystem inputSystem;
    private PlayerMovement playerMovement;
    public Vector2 moveInput;

    private void Awake()
    {
        inputSystem = new InputSystem();
        playerMovement = GetComponent<PlayerMovement>();

        inputSystem.Player.Jump.performed += Jump;
        inputSystem.Player.Fire.performed += Fire;
        inputSystem.Player.Dash.performed += Dash;
        inputSystem.Player.Camouflage.performed += Camouflage;
    }

    private void OnEnable()
    {
        inputSystem.Player.Enable();

    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }

    public Vector2 GetNormalizedDirection()
    {
        Vector2 inputVector = inputSystem.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }



    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !playerMovement.isCamouflage)
        {
            playerMovement.Jump();
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && playerMovement.currentCooldownShuriken <= 0 && !playerMovement.isCamouflage)
        {
            playerMovement.Fire();
        }
    }

    private void Camouflage(InputAction.CallbackContext context)
    {
        if (context.performed && playerMovement.currentCooldownCamouflage <= 0 && !playerMovement.isCamouflage)
        {
            playerMovement.Camouflage();
        }
        else if (context.performed && playerMovement.isCamouflage)
        {
            playerMovement.TurnOffCamouflage();
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && playerMovement.currentCooldownDashKill <= 0 && !playerMovement.isCamouflage)
        {
            playerMovement.DashKill();
        }
    }


}
