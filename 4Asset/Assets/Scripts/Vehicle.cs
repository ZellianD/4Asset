using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Reference to RigidBody on this GameObject
    public Rigidbody rBody;
    public Transform carBody;
    // Fields for Speed
    public float maxSpeed, stopThreshold;

    // Fields for Acceleration/Deceleration
    public float accelerationRate, decelerationRate;

    // Fields for Turning
    public float turnRate;

    // Fields for Input
    Vector3 movementDirection;

    // Fields for Movement Vectors
    Vector3 velocity, acceleration;

    //New forward
    Vector3 newFor;

    // Fields for Quaternions
    Quaternion rotDelta;
    public Quaternion forwardRot;

    bool backwards;
    bool prevBack;

    private void FixedUpdate()
    {

        Vector3 nextPosition = transform.position;
        Quaternion nextRotation = transform.rotation;

        acceleration = Vector3.zero;
        //velocity
        if (movementDirection.z == 0f)
        {
            velocity *= 1f -(decelerationRate * Time.fixedDeltaTime);
            if(velocity.sqrMagnitude < stopThreshold*stopThreshold)
            {
                velocity = Vector3.zero;
            }
        }
        else
        {
            //Calc accel based on direction
            acceleration = newFor * movementDirection.z * accelerationRate;

            //Calc velocity based of acceleration 
            velocity += acceleration * Time.fixedDeltaTime;

            //limit velocity based on accel
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }

        if(velocity.sqrMagnitude>.1)
        {
            //have to change so this only happens when the car is moving
            if (backwards || prevBack)
            { 
                rotDelta = Quaternion.Euler(0,(movementDirection.x * -turnRate * Time.fixedDeltaTime), 0);
            }
            else
            {
                //rotation 
                rotDelta = Quaternion.Euler(0 ,(movementDirection.x * turnRate * Time.fixedDeltaTime), 0);
            }

            nextRotation *= rotDelta;
            nextRotation.Normalize();
        }
        
        velocity = rotDelta * velocity; //Q HAS to be the left 

        nextPosition += (velocity*Time.fixedDeltaTime);
        nextPosition.y += 3;

        //APPLY CALCS
        rBody.Move(nextPosition,nextRotation);
        carBody.rotation = forwardRot;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();
        if(inputDir.y<0)
        {
            backwards = true;
            prevBack = true;
        }
        else if(inputDir.y==0 && prevBack)
        {
            backwards = true; 
            prevBack = false;
        }
        else
        {
            backwards= false;
        }
        
        movementDirection.z = inputDir.y;
        movementDirection.x = inputDir.x;
    }
}
