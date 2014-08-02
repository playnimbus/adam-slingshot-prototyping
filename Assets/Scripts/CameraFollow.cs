using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - objectToFollow.transform.position;
    }

	void FixedUpdate () 
	{
        UpdatePosition();
	}

    void UpdatePosition()
    {
        Vector3 objectPosition = objectToFollow.transform.position + offset;
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, objectPosition, 0.25f);
        transform.position = lerpedPosition;
    }
}
