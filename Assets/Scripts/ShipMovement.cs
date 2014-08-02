using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public GameObject pivot;
    public GameObject ship;

    public float speed;

    [Range(-90f, 90f)]
    public float turnAngle;

    // The primary direction the ship will be moving in, in degrees
    private float primaryDirection = 90f;

    Plane movementPlane;

    [Range(1f, 10f)]
    public float planeDistance = 3f;

    private float serpAmount = 0f;

    [Range(1f, 10f)]
    public float turnSpeed = 3f;

    void Start()
    {

    }
    
	void FixedUpdate () 
	{
	    if(Input.GetMouseButton(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Steer(viewportPoint);        
        }
        else
        {
            Steer(new Vector3(0.5f, 0.5f, 0f));
        }

        Move();
	}

    void Move()
    {
        Vector2 normal = new Vector2(Mathf.Cos(Mathf.Deg2Rad * primaryDirection), Mathf.Sin(Mathf.Deg2Rad * primaryDirection));

        pivot.transform.position = (Vector2)pivot.transform.position + normal * speed * Time.fixedDeltaTime;
        movementPlane.SetNormalAndPosition(normal, (Vector2)pivot.transform.position + normal * planeDistance);

        float movementAngle = primaryDirection + Sinerp(0f, Mathf.Sign(serpAmount) * turnAngle, Mathf.Abs(serpAmount));
        Vector2 movementDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * movementAngle), Mathf.Sin(Mathf.Deg2Rad * movementAngle));
        Ray ray = new Ray(pivot.transform.position, movementDirection);

        pivot.transform.position = pivot.transform.position + Vector3.right * Mathf.Cos(Mathf.Deg2Rad * movementAngle);

        float distance;
        if(movementPlane.Raycast(ray, out distance))
        {
            Vector3 position = ray.GetPoint(distance);
            ship.rigidbody2D.MovePosition(position);
            ship.transform.up = movementDirection;
        }
    }

    void Steer(Vector3 viewportPoint)
    {
        if(viewportPoint.x < 0.4f)
        {
            serpAmount += Time.fixedDeltaTime * turnSpeed;
        }
        else if(viewportPoint.x > 0.6f)
        {
            serpAmount -= Time.fixedDeltaTime * turnSpeed;
        }
        else
        {
            serpAmount = Mathf.MoveTowards(serpAmount, 0f, Time.fixedDeltaTime * turnSpeed);
        }

        serpAmount = Mathf.Clamp(serpAmount, -1f, 1f);
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
