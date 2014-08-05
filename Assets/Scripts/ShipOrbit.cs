using UnityEngine;
using System;

[Serializable]
public class ShipOrbit
{
    public float orbitSpeed;

    private Ship ship;
    private Planet planet;
    private float currentAngle;
    private float currentDistance;

    public float direction
    {
        get
        {
            return currentAngle * Mathf.Rad2Deg - 90f;
        }
    }

    public void SetShip(Ship ship)
    {
        this.ship = ship;
    }

    public void SetPlanet(Planet planet)
    {
        this.planet = planet;
        currentAngle = Mathf.Atan2(planet.transform.position.y - ship.transform.position.y, planet.transform.position.x - ship.transform.position.x);
        if (planet.transform.position.y - ship.transform.position.y > 0) currentAngle += Mathf.PI;
        currentDistance = Vector3.Distance(planet.transform.position, ship.transform.position);
    }

    public void Update()
    {
        currentAngle += orbitSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime;
        Vector3 position = (Vector2)planet.transform.position + HelperMethods.Vector2FromRadians(currentAngle) * currentDistance;
        ship.rigidbody2D.MovePosition(position);
        ship.transform.up = HelperMethods.PerpindicularVector(HelperMethods.Vector2FromRadians(currentAngle));
    }
}
