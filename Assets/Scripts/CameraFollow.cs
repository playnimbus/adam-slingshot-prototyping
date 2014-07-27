using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    	
	void Update () 
	{
        UpdatePosition();
	}

    void UpdatePosition()
    {
        Vector3 objectPosition = objectToFollow.transform.position;
        objectPosition.z = transform.position.z;
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, objectPosition, 0.25f);
        transform.position = lerpedPosition;
    }
}
