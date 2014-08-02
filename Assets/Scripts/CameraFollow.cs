using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    Vector3 offset;

    [Range(0f, 1f)]
    public float lerpAmount = 0.25f;
    public bool useFixedUpdate = true;

    void Start()
    {
        offset = transform.position - objectToFollow.transform.position;
    }

    void Update()
    {
        if (!useFixedUpdate) UpdatePosition();
    }

	void FixedUpdate () 
	{
        if(useFixedUpdate) UpdatePosition();
	}

    void UpdatePosition()
    {
        Vector3 objectPosition = objectToFollow.transform.position + offset;
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, objectPosition, lerpAmount);
        transform.position = lerpedPosition;
    }
}
