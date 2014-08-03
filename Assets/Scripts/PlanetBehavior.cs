using UnityEngine;
using System.Collections;

public class PlanetBehavior : MonoBehaviour {


    public Vector3[] path;


	// Use this for initialization
	void Start () {

        createPath();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void createPath()
    {
        Vector3 point1 = new Vector3(gameObject.transform.position.x - (gameObject.renderer.bounds.size.x ), gameObject.transform.position.y, gameObject.transform.position.z);   //left
        Vector3 point2 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (gameObject.renderer.bounds.size.y ), gameObject.transform.position.z);   //top
        Vector3 point3 = new Vector3(gameObject.transform.position.x + (gameObject.renderer.bounds.size.x ), gameObject.transform.position.y, gameObject.transform.position.z);   //right
        Vector3 point4 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (gameObject.renderer.bounds.size.y ), gameObject.transform.position.z);   //bottom

        path = new Vector3[] { point1, point2, point3, point4, point1 };
    }

    public Vector3[] getPath()
    {
        return path;
    }
}
