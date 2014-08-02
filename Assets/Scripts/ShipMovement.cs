using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public GameObject ship;
    public float forwardSpeed;
    public float maxStrafeSpeed;

    [Range(0, 1)]
    public float strafeAcceleration;

    private float primaryDirection = 90f;
    private float strafeSpeed;

    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            HandleInput(viewportPoint);
        }
        else
        {
            NoInput();
        }        

        Move();  
    }

    private void HandleInput(Vector3 viewportPoint)
    {
        if(viewportPoint.x < 0.5f)
        {
            strafeSpeed = Mathf.Lerp(strafeSpeed, -maxStrafeSpeed, strafeAcceleration);
        }
        else
        {
            strafeSpeed = Mathf.Lerp(strafeSpeed, maxStrafeSpeed, strafeAcceleration);
        }

        strafeSpeed = Mathf.Clamp(strafeSpeed, -maxStrafeSpeed, maxStrafeSpeed);
    }

    private void NoInput()
    {
        strafeSpeed = Mathf.Lerp(strafeSpeed, 0f, strafeAcceleration / 2f);
    }

    private void Move()
    {
        Vector2 forwardAxis = Vector2FromAngle(primaryDirection);
        Vector2 forwardAmount = forwardAxis * forwardSpeed * Time.fixedDeltaTime;
        this.transform.position = (Vector2)this.transform.position + forwardAmount;

        Vector2 strafeAxis = Vector2FromAngle(primaryDirection - 90f);
        Vector2 strafeAmount = strafeAxis * strafeSpeed * Time.fixedDeltaTime;
        ship.rigidbody2D.MovePosition((Vector2)ship.transform.position + strafeAmount);

        Vector2 deltaPosition = forwardAmount + strafeAmount;
        ship.transform.up = deltaPosition.normalized;
    }

    private Vector2 Vector2FromAngle(float angle)
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));        
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
