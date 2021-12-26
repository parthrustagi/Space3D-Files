using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : MonoBehaviour
{
    public Vector3 linerF = new Vector3(100.0f, 100.0f, 100.0f);

    public Vector3 angularF = new Vector3(100.0f, 100.0f, 100.0f);

    [Range(0.0f, 1.0f)]
    public float multiplierReverse = 1.0f;

    public float multiplierForce = 100.0f;

    public Rigidbody Rigidbody { get { return rbody; } }

    private Vector3 linearApplied = Vector3.zero;
    private Vector3 linearAngular = Vector3.zero;

    private Rigidbody rbody;

    private Ship ship;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
        {
            Debug.LogWarning("Please add a rigidbody");
        }

        ship = GetComponent<Ship>();
    }

    void FixedUpdate()
    {
        if (rbody != null)
        {
            rbody.AddRelativeForce(linearApplied * multiplierForce, ForceMode.Force);
            rbody.AddRelativeTorque(linearAngular * multiplierForce, ForceMode.Force);
        }
    }

    public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput)
    {
        linearApplied = MultiplyByComponent(linearInput, linerF);
        linearAngular = MultiplyByComponent(angularInput, angularF);
    }

    private Vector3 MultiplyByComponent(Vector3 a, Vector3 b)
    {
        Vector3 ret;

        ret.x = a.x * b.x;
        ret.y = a.y * b.y;
        ret.z = a.z * b.z;

        return ret;
    }
}