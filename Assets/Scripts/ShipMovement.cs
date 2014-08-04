using UnityEngine;
using System;

[Serializable]
public class ShipMovement
{
    public float forwardSpeed;
    public float maxStrafeSpeed;
    [Range(0, 1)] public float strafeAcceleration;

    private Ship ship;
    private float _movementAngle;
    private float strafeSpeed;
    private Vector2 _forwardDirection;
    private Vector2 _perpindicularDirection;

    public float movementAngle
    {
        get { return _movementAngle; }
        set
        {
            _movementAngle = value;
            _forwardDirection = Vector2FromDegrees(value);
            _perpindicularDirection = PerpindicularVector(_forwardDirection);
        }
    }

    public Vector2 forwardDirection
    {
        get { return _forwardDirection; }
    }

    public Vector2 perpindicularDirection
    {
        get { return _perpindicularDirection; }
    }

    public void SetShip(Ship ship)
    {
        this.ship = ship;
    }
    
    public void Update()
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
        Vector2 forwardAmount = _forwardDirection * forwardSpeed * Time.fixedDeltaTime;
        Vector2 strafeAmount = _perpindicularDirection * strafeSpeed * Time.fixedDeltaTime;
        Vector2 deltaPosition = forwardAmount + strafeAmount;

        ship.rigidbody2D.MovePosition((Vector2)ship.transform.position + deltaPosition);
        ship.transform.up = deltaPosition;
    }

    private Vector2 Vector2FromDegrees(float degrees)
    {
        return Vector2FromRadians(Mathf.Deg2Rad * degrees);
    }

    private Vector2 Vector2FromRadians(float radians)
    {
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        return new Vector2(x, y);
    }

    // Perpindicular vector clockwise
    private Vector2 PerpindicularVector(Vector2 direction)
    {
        return new Vector2(direction.y, -direction.x);
    }

}
