using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{
   [SerializeField]
   MovementController myMovmentController;
    private Vector3 inputDirection;
    internal Vector3 direction;

    // The method that gets called to handle any player movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the latest value for the input from the Input System
        inputDirection = context.ReadValue<Vector2>();  // This is already normalized for us

        // Send that new direction to the Vehicle class
        myMovmentController.SetDirection(inputDirection);
    }
}
