using UnityEngine;
using System.Collections;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get 
        {
            if (_instance == null) _instance = new GameManager();
            return _instance; 
        }
    }

    private GameCamera camera;
    private Ship ship;

    private GameManager() 
    {
        _instance = this;

        camera = UnityEngine.Object.FindObjectOfType<GameCamera>();
        ship = UnityEngine.Object.FindObjectOfType<Ship>();
	}
	
	public void PlanetCollision(Planet planet)
    {
        camera.GoToPlanet(planet);
        ship.CollideWithPlanet(planet);
    }

    public void LeavePlanet()
    {
        ship.LeavePlanet();
        camera.FollowShip();
    }
}
