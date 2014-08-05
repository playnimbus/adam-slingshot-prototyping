using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    public ShipMovement movement;
    public ShipOrbit orbit;

    bool move = true;

    void Start()
    {
        movement.SetShip(this);
        movement.movementAngle = 90f;
        orbit.SetShip(this);
    }

    void FixedUpdate()
    {
        if (move) movement.Update();
        else orbit.Update();
    }

    public void CollideWithPlanet(Planet planet)
    {
        move = false;
        orbit.SetPlanet(planet);
    }

    public void LeavePlanet()
    {
        float angle = orbit.direction;
        movement.movementAngle = angle;
        move = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (move && collision.gameObject.CompareTag("Asteroid"))
        {
            movement.ReduceSpeed();
        }
    }
}
