using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    public ShipMovement movement;

    void Start()
    {
        movement.SetShip(this);
        movement.movementAngle = 90f;
    }

    void FixedUpdate()
    {
        movement.Update();
    }
}
