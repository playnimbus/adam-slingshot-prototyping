using UnityEngine;
using System.Collections;

public class ShipPointAndClickMovement : MonoBehaviour {

    public float ShipVelocity;
    public Vector3 directionToTravel = Vector3.up;
    Vector3 mouseWorldPoint = Vector3.zero;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            directionToTravel = (mouseWorldPoint - gameObject.transform.position).normalized;
            directionToTravel = new Vector3(directionToTravel.x, directionToTravel.y, 0);
        }
        gameObject.transform.LookAt(directionToTravel + gameObject.transform.position);
 //       gameObject.transform.position += (directionToTravel * ShipVelocity) * Time.deltaTime;
        gameObject.rigidbody2D.MovePosition( gameObject.transform.position + (directionToTravel * ShipVelocity) * Time.deltaTime);
	}
}
