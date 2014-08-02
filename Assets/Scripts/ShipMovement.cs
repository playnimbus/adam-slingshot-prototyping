using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    // Speed in m/s
    [Range(0f, 25f)] public float speed;

    // Amount at which to turn in degrees
    [Range(0f, 90f)] public float turnAngle;

    [Range(0f, 20f)]
    public float turnSpeed;

    Plane movementPlane;

    // The primary direction the ship will be moving in, in degrees
    private float primaryDirection = 90f;

    private float herpAmount = 0f;
    
    void Start()
    {
        UpdatePlane();
    }
    
	void FixedUpdate () 
	{
        movementPlane.distance = transform.position.magnitude + speed;

	    if(Input.GetMouseButton(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Steer(viewportPoint);        
        }
        else
        {
            Steer();
        }

        Move();
	}

    void UpdatePlane()
    {
        movementPlane.normal = new Vector2(Mathf.Cos(Mathf.Deg2Rad * primaryDirection), Mathf.Sin(Mathf.Deg2Rad * primaryDirection));
        movementPlane.distance = movementPlane.distance + speed;
    }

    void Move()
    {
        float angle = primaryDirection + Hermite(0f, turnAngle * Mathf.Sign(herpAmount), Mathf.Abs(herpAmount));
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        Vector2 velocity = direction * speed * Time.fixedDeltaTime;

        rigidbody2D.MovePosition((Vector2)transform.position + velocity);
        transform.up = direction;
    }

    void Steer()
    {
        Steer(new Vector3(0.5f, 0.5f, 0f));
    }

    void Steer(Vector3 viewportPoint)
    {
        if(viewportPoint.x < 0.2f)
        {
            herpAmount += Time.fixedDeltaTime * turnSpeed;
        }
        else if(viewportPoint.x > 0.8f)
        {
            herpAmount -= Time.fixedDeltaTime * turnSpeed;
        }
        else
        {
            herpAmount = Mathf.MoveTowards(herpAmount, 0f, Time.fixedDeltaTime * turnSpeed);
        }

        herpAmount = Mathf.Clamp(herpAmount, -1f, 1f);
    }

    // *****************************************************
    // From: http://wiki.unity3d.com/index.php?title=Mathfx
    // *****************************************************

    public float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }

    public static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }
}
