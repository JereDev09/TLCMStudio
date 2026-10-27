using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManagerControiller
{
    private static NewActions inputs;


    public static Action <Vector2> OnMove;
    public static Action OnJump;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        inputs = new NewActions();
        inputs.Enable();

        inputs.Player.Movement.performed += OnMovementPerformed;
        inputs.Player.Movement.canceled += OnMovementCanceled;
        inputs.Player.Jump.performed += OnJumpPerformed;
    }
    private static void OnMovementPerformed(InputAction.CallbackContext context)
    {
       Vector2 movementInput = context.ReadValue<Vector2>();
       OnMove.Invoke(movementInput);
    }

    private static void OnMovementCanceled(InputAction.CallbackContext context)
    {
        OnMove.Invoke(Vector2.zero);
    }
    private static void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump.Invoke();
    }
}
