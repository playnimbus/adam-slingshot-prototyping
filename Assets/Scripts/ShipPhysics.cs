using UnityEngine;
using System.Collections;

// Class that determines the physics of movement for the ship
public class ShipPhysics : MonoBehaviour 
{
    public float forwardSpeed;
    public float forwardAcceleration;
    
    public float sideSpeed;
    public float sideAcceleration;
	
	void FixedUpdate () 
    {
        ThrustForward();

        if(Input.GetMouseButton(0))
        {
            Vector3 viewPortCoords = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (viewPortCoords.x < 0.2f) ThrustLeft();
            if (viewPortCoords.x > 0.8f) ThrustRight();
        }
	}

    void ThrustLeft()
    {
        rigidbody2D.AddForce(Mathf.Min(-sideSpeed, rigidbody2D.velocity.x + sideAcceleration) - rigidbody2D.velocity.x, 0, ForceMode.Impulse);
    }

    void ThrustRight()
    {
        rigidbody2D.AddForce(Mathf.Max(sideSpeed, rigidbody2D.velocity.x - sideAcceleration) - rigidbody2D.velocity.x, 0, ForceMode.Impulse);
    }

    void ThrustForward()
    {
        rigidbody2D.AddForce(0, Mathf.Max(forwardSpeed, rigidbody2D.velocity.y - forwardAcceleration) - rigidbody2D.velocity.y, ForceMode.Impulse);
    }
}
