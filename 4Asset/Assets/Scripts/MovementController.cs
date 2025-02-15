using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Where is the vehicle
    Vector3 objectPosition = new Vector3(0, 0, 0);

    // How fast it should move in units per second
    float speed = 3f;

    // Direction vehicle is facing, must be normalized
    Vector3 direction = new Vector3(1, 0, 0);   // or Vector3.right
    internal Vector3 Direction
    {
        get { return direction; } // Provide it if needed
        set // Only set a normalized copy!
        {
            direction = value.normalized;
        }
    }
    // The delta in position for a single frame
    Vector3 velocity = new Vector3(0, 0, 0);   // or Vector3.zero


    // Start is called before the first frame update
    void Start()
    {
        Camera camObj = FindObjectOfType<Camera>();
        float totalCamHeight = camObj.orthographicSize * 2f;
        float totalCamWidth = totalCamHeight * camObj.aspect;
        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to position 
        objectPosition += velocity;

        // Validate new calculated position
        if (objectPosition.y > 5)
        {
            objectPosition.y = -3;
        }
        else if (objectPosition.y <-3)
        {
            objectPosition.y = -3;
        }
      
        if (objectPosition.x > 4)
        {
            objectPosition.x = -3;
        }
        else if(objectPosition.x <-3) 
        {
            objectPosition.x = 3;
        }

        // “Draw” this vehicle at that position
        transform.position = objectPosition;

    }

    public void SetDirection(Vector2 input)
    {
        if (input != null)
        {
            direction = input.normalized;

            // Set the vehicle’s rotation to match the direction
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction); // for 2D rotation
        }
    }
}
