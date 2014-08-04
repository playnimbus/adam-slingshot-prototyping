using UnityEngine;
using System;

public class GameCamera : MonoBehaviour
{
    public FollowShipCameraMode followShipCamera;
    public PlanetCameraMode planetCamera;

    [HideInInspector] public Ship ship;

    private GameCameraMode mode;

    void Start()
    {
        ship = FindObjectOfType<Ship>();
        SetMode(planetCamera);
    }

    public void SetMode(GameCameraMode mode)
    {
        mode.SetGameCamera(this);
        this.mode = mode;
    }

    void FixedUpdate()
    {
        mode.Update();
    }
}

#region CameraModes

public abstract class GameCameraMode
{
    protected GameCamera camera;

    public void SetGameCamera(GameCamera cam)
    {
        this.camera = cam;
    }

    virtual public void Start() { }

    abstract public void Update();
}

[Serializable]
public class FollowShipCameraMode : GameCameraMode
{
    [Range(0f, 1f)] public float shipLerpAmount;
    public float shipLeadDistance = 5f;
    public float cameraHeight;

    override public void Update()
    {
        ShipMovement shipMovement = camera.ship.movement;
        Transform transform = camera.transform;

        Vector3 shipPosition = camera.ship.transform.position;
        shipPosition.z = cameraHeight;

        Vector3 projectedPosition = Vector3.Project(shipPosition - transform.position, shipMovement.forwardDirection);
        transform.Translate(projectedPosition, Space.World);

        transform.position = Vector3.Lerp(transform.position, shipPosition, shipLerpAmount);
        transform.up = shipMovement.forwardDirection;

        transform.position = transform.position + (Vector3)shipMovement.forwardDirection * shipLeadDistance;
    }
}

[Serializable]
public class PlanetCameraMode : GameCameraMode
{
    public GameObject planet;
    public float cameraHeight;

    public override void Update()
    {
        camera.transform.position = planet.transform.position + Vector3.forward * cameraHeight;
        camera.transform.up = planet.transform.position;
    }
}

#endregion
