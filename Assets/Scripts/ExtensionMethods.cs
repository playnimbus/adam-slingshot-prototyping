using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static void AddForce(this Rigidbody2D rigidbody2D, Vector2 force, ForceMode mode = ForceMode.Force, Space space = Space.World)
    {
        if(space == Space.Self)
        {
            force = rigidbody2D.transform.InverseTransformDirection(force);
        }

        switch (mode)
        {
            case ForceMode.Force:
                rigidbody2D.AddForce(force);
                break;
            case ForceMode.Impulse:
                rigidbody2D.AddForce(force / Time.fixedDeltaTime);
                break;
            case ForceMode.Acceleration:
                rigidbody2D.AddForce(force * rigidbody2D.mass);
                break;
            case ForceMode.VelocityChange:
                rigidbody2D.AddForce(force * rigidbody2D.mass / Time.fixedDeltaTime);
                break;
        }
    }

    public static void AddForce(this Rigidbody2D rigidbody2D, float x, float y, ForceMode mode = ForceMode.Force, Space space = Space.World)
    {
        rigidbody2D.AddForce(new Vector2(x, y), mode, space);
    }
}
