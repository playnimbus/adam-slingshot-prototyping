using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{

	void FixedUpdate () 
	{
        ApplyForwardForce();

	    if(Input.GetMouseButton(0))
        {
            ApplySteeringForce(Camera.main.ScreenToViewportPoint(Input.mousePosition));        
        }

        AlignToHeading();
	}

    void ApplyForwardForce()
    {
        rigidbody2D.AddForce(Vector3.up, ForceMode.Force);
    }

    void ApplySteeringForce(Vector3 normalizedScreen)
    {
        if(normalizedScreen.y < 0.2f)
        {
            rigidbody2D.AddForce(transform.up, ForceMode.Force);
        }
        else if(normalizedScreen.x < 0.2f)
        {
            rigidbody2D.AddForce(-Vector2.right, ForceMode.Force);
        }
        else if(normalizedScreen.x > 0.8f)
        {
            rigidbody2D.AddForce(Vector2.right, ForceMode.Force);
        }
    }

    void AlignToHeading()
    {
        Vector2 movementHeading = rigidbody2D.velocity;
        transform.up = movementHeading;
    }
}
