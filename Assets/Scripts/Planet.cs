using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    private Ship ship;
    private static bool shipCaught;
        
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shipCaught) return;
        ship = collision.gameObject.GetComponent<Ship>();
        if(ship != null)
        {
            GameManager.instance.PlanetCollision(this);
            collider2D.isTrigger = true;
            shipCaught = true;
        }
    }

    void OnMouseUpAsButton()
    {
        if(ship != null)
        {
            GameManager.instance.LeavePlanet();
            ship = null;
            shipCaught = false;
            Invoke("TurnColliderBackOn", 1f);
        }
    }

    void TurnColliderBackOn()
    {
        collider2D.isTrigger = false;
    }
}
