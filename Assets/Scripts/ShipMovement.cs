using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{

	void FixedUpdate () 
	{
	    if(Input.GetMouseButton(0))
        {
            UpdateMovement(Camera.main.ScreenToViewportPoint(Input.mousePosition));        
        }

        AlignToHeading();
	}

    void UpdateMovement(Vector3 normalizedScreen)
    {
        if(normalizedScreen.y < 0.2f)
        {
            rigidbody2D.AddForce(Vector2.up, ForceMode.Force);
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
