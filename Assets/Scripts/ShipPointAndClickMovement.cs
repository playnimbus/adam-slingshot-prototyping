using UnityEngine;
using System.Collections;

public class ShipPointAndClickMovement : MonoBehaviour {

    public float ShipVelocity;
    public Vector3 directionToTravel = Vector3.up;
    Vector3 mouseWorldPoint = Vector3.zero;

    enum ShipState { flying, gravitating };

    ShipState shipState = ShipState.flying;

    Vector3[] planetPath;
    GameObject currentPlanet;

	// Use this for initialization
	void Start () {

        ht.Add("speed", 15);
        ht.Add("movetopath", true);
        ht.Add("easetype", "easeInQuad");
        ht.Add("orienttopath", true);
        ht.Add("oncomplete", "changeState");
        ht.Add("oncompleteparams", ShipState.flying);

	}
	
	// Update is called once per frame
	void Update () {
        switch (shipState)
        {
           case ShipState.flying: flyingUpdate(); break;
           case ShipState.gravitating: gravitatingUpdate(); break;
        }
     
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Planet" &&
            shipState != ShipState.gravitating)
        {
            currentPlanet = coll.gameObject;
            changeState(ShipState.gravitating);
        }
    }


    //-------------------------PLAYER STATES-------------------------

    void changeState(ShipState newState)
    {
        switch (shipState)
        {
            case ShipState.flying: flyingExit();break;
            case ShipState.gravitating: gravitatingExit(); break;
        }

        shipState = newState;

        switch (shipState)
        {
            case ShipState.flying: flyingEnter(); break;
            case ShipState.gravitating: gravitatingEnter(); break;
        }
    }

    //-------------------------Flying States-------------------------

    void flyingEnter()
    {

    }

    void flyingUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            directionToTravel = (mouseWorldPoint - gameObject.transform.position).normalized;
            directionToTravel = new Vector3(directionToTravel.x, directionToTravel.y, 0);
        }
        gameObject.transform.LookAt(directionToTravel + gameObject.transform.position);
        //       gameObject.transform.position += (directionToTravel * ShipVelocity) * Time.deltaTime;
        gameObject.rigidbody2D.MovePosition(gameObject.transform.position + (directionToTravel * ShipVelocity) * Time.deltaTime);
    }

    void flyingExit()
    {
    }


    //-------------------------Gravitating States-------------------------
    Hashtable ht = new Hashtable();

    void gravitatingEnter()
    {
  //      planetPath = currentPlanet.GetComponent<PlanetBehavior>().getPath();

        ht.Add("path", currentPlanet.GetComponent<PlanetBehavior>().getPath());
        
        iTween.MoveTo(gameObject, ht);
    }

    void gravitatingUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            iTween.Stop(gameObject);
            changeState(ShipState.flying);
        }
    }

    void gravitatingExit()
    {
        ht.Remove("path");
    }

}
