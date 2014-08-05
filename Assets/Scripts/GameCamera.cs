using UnityEngine;
using System;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    public FollowShipCameraMode followShipCamera;
    public PlanetCameraMode planetCamera;

    [HideInInspector] public Ship ship;

    private GameCameraMode mode;

    void Start()
    {
        ship = FindObjectOfType<Ship>();
        SetMode(followShipCamera);
    }

    public void SetMode(GameCameraMode mode)
    {
        mode.SetGameCamera(this);        
        this.mode = mode;
        mode.Start();
    }

    public void GoToPlanet(Planet planet)
    {
        planetCamera.planet = planet;
        SetMode(planetCamera);
    }
    
    public void FollowShip()
    {
        SetMode(followShipCamera);
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
    bool update;
    public override void Start()
    {
        camera.StartCoroutine(GoToShipCoroutine());
    }

    private IEnumerator GoToShipCoroutine()
    {
        Vector3 startPosition = camera.transform.position;
        Quaternion startRotation = camera.transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(Vector3.forward, camera.ship.movement.forwardDirection);

        update = false;

        float totalTime = 0.5f;
        float time = totalTime;
        while(time > 0f)
        {
            time -= Time.fixedDeltaTime;
            float percent = (totalTime - time) / totalTime;

            Vector3 endPosition = (Vector2)camera.ship.transform.position + camera.ship.movement.forwardDirection * shipLeadDistance;
            endPosition.z = cameraHeight;

            Vector3 lerpedPosition = Vector3.Lerp(startPosition, endPosition, percent);
            Quaternion lerpedRotation = Quaternion.Lerp(startRotation, endRotation, percent);

            camera.transform.position = lerpedPosition;
            camera.transform.rotation = lerpedRotation;

            yield return new WaitForFixedUpdate();
        }

        update = true;
    }
    
    override public void Update()
    {
        if (update)
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
}

[Serializable]
public class PlanetCameraMode : GameCameraMode
{
    public Planet planet;
    public float cameraHeight;
    bool update;

    public override void Start()
    {
        camera.StartCoroutine(MoveToPositionCoroutine());
    }
    
    private IEnumerator MoveToPositionCoroutine()
    {
        Quaternion startRotation = camera.transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(Vector3.forward, planet.transform.position.normalized);

        Vector3 startPosition = camera.transform.position;
        Vector3 endPosition = planet.transform.position;
        endPosition.z = cameraHeight;

        update = false;

        float totalTime = 0.5f;
        float time = totalTime;
        while(time > 0f)
        {
            time -= Time.fixedDeltaTime;
            float percent = (totalTime - time) / totalTime;

            Quaternion lerpedRotation = Quaternion.Lerp(startRotation, endRotation, percent);
            Vector3 lerpedPosition = Vector3.Lerp(startPosition, endPosition, percent);

            camera.transform.rotation = lerpedRotation;
            camera.transform.position = lerpedPosition;

            yield return new WaitForFixedUpdate();
        }

        update = true;
    }

    public override void Update()
    {
        if (update)
        {
            camera.transform.position = planet.transform.position + Vector3.forward * cameraHeight;
            camera.transform.up = planet.transform.position;
        }
    }
}

#endregion
