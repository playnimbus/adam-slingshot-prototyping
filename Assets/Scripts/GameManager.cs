using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    private GameCamera camera;
    private Ship ship;

	void Start () 
    {
        camera = FindObjectOfType<GameCamera>();
        ship = FindObjectOfType<Ship>();
	}
	
	void Update ()
    {
	
	}
}
