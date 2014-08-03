using UnityEngine;
using System.Collections.Generic;

public class GeneratePlanets : MonoBehaviour 
{
    public GameObject planetsPrefab;
    public GameObject ship;

    public float gridSize;

    private List<GameObject> planetGroups;
    private int shipX, shipY;
	
    void Start() 
    {
        planetGroups = new List<GameObject>(9);

        for(int i=0; i<9; i++)
        {
            GameObject gObj = Instantiate(planetsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            gObj.transform.parent = this.transform;
            planetGroups.Add(gObj);

            foreach(Transform child in gObj.transform)
            {
                child.Rotate(Random.onUnitSphere, Random.value * 360f);
            }
        }

        shipX = Mathf.FloorToInt(ship.transform.position.x / gridSize);
        shipY = Mathf.FloorToInt(ship.transform.position.y / gridSize);
        PlacePlanetsAt(shipX, shipY);
	}

    void Update()
    {
        int x = Mathf.FloorToInt(ship.transform.position.x / gridSize);
        int y = Mathf.FloorToInt(ship.transform.position.y / gridSize);

        if(x != shipX || y!= shipY)
        {
            PlacePlanetsAt(x, y);
            shipX = x;
            shipY = y;
        }
    }

    void PlacePlanetsAt(int x, int y)
    {
        int index = 0;
        Vector3 position = new Vector3();

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                position.x = (x + dx) * gridSize;
                position.y = (y + dy) * gridSize;
                planetGroups[index].transform.position = position;
                index++;
            }
        }
    }
}
