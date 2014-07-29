using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public float turningPower;
    public float turningThrust;
    public float thrustingPower;

	void FixedUpdate () 
	{
        ApplyForwardForce();

	    if(Input.GetMouseButton(0))
        {
            ApplySteeringForce(Camera.main.ScreenToViewportPoint(Input.mousePosition));        
        }
	}

    void ApplyForwardForce()
    {
        rigidbody2D.AddForce(Vector3.up, ForceMode.Force);
    }

    void ApplySteeringForce(Vector3 normalizedScreen)
    {
        if(normalizedScreen.y < 0.2f)
        {
            rigidbody2D.AddForce(Vector2.up * thrustingPower, ForceMode.Force, Space.Self);
        }
        else if(normalizedScreen.x < 0.2f)
        {
            rigidbody2D.AddTorque(turningPower);
            rigidbody2D.AddForce(Vector2.up * turningThrust, ForceMode.Force, Space.Self);
        }
        else if(normalizedScreen.x > 0.8f)
        {
            rigidbody2D.AddTorque(-turningPower);
            rigidbody2D.AddForce(Vector2.up * turningThrust, ForceMode.Force, Space.Self);
        }
    }
}
