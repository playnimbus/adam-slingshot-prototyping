using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public GameObject ship;
    public float forwardSpeed;
    public float maxStrafeSpeed;
    [Range(0, 1)] public float strafeAcceleration;

    private float movementAngle;
    private float strafeSpeed;
    private Vector2 primaryDirection;
    private Vector2 perpindicularDirection;

    public float MovementAngle
    {
        get { return movementAngle; }
        set
        {
            movementAngle = value;
            primaryDirection = Vector2FromAngle(value);
            perpindicularDirection = PerpindicularVector(primaryDirection);
        }
    }

    public Vector2 PrimaryDirection
    {
        get { return primaryDirection; }
    }

    public Vector2 PerpindicularDirection
    {
        get { return perpindicularDirection; }
    }

    public Vector3 Position { get { return ship.transform.position; } }
   
    void Start()
    {
        MovementAngle = 90f;
    }
    
    void FixedUpdate()
    {
        HandleInput();
        Move();  
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (viewportPoint.x < 0.5f)
            {
                strafeSpeed = Mathf.Lerp(strafeSpeed, -maxStrafeSpeed, strafeAcceleration);
            }
            else
            {
                strafeSpeed = Mathf.Lerp(strafeSpeed, maxStrafeSpeed, strafeAcceleration);
            }
        }
        else
        {
            strafeSpeed = Mathf.Lerp(strafeSpeed, 0f, strafeAcceleration);
        }
    }

    private void Move()
    {
        Vector2 forwardAmount = primaryDirection * forwardSpeed * Time.fixedDeltaTime;
        Vector2 strafeAmount = perpindicularDirection * strafeSpeed * Time.fixedDeltaTime;
        Vector2 deltaPosition = forwardAmount + strafeAmount;

        ship.rigidbody2D.MovePosition((Vector2)ship.transform.position + deltaPosition);
        ship.transform.up = deltaPosition;
    }

    private Vector2 Vector2FromAngle(float angle)
    {
        float x = Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = Mathf.Sin(Mathf.Deg2Rad * angle);
        return new Vector2(x, y);
    }

    // Perpindicular vector clockwise
    private Vector2 PerpindicularVector(Vector2 direction)
    {
        return new Vector2(direction.y, -direction.x);
    }

}
