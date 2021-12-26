using UnityEngine;

/// <summary>
/// Adds a slight lag to camera rotation to make the third person camera a little more interesting.
/// Requires that it starts parented to something in order to follow it correctly.
/// </summary>
[RequireComponent(typeof(Camera))]
public class LagCamera : MonoBehaviour
{    
    public float rotateSpeed = 90.0f;

    public bool usedFixedUpdate = true;

    private Transform target;
    private Vector3 offSet;

    private void Start()
    {
        target = transform.parent;

        if (target == null)
        {
            Debug.LogWarning("Please give a target");
        }
        
        if (transform.parent == null)
        {
            Debug.LogWarning("Please assign an initial offset point");
        }

        offSet = transform.localPosition;
        transform.SetParent(null);
    }

    private void Update()
    {
        if (!usedFixedUpdate)
            UpdateCamera();
    }

    private void FixedUpdate()
    {
        if (usedFixedUpdate)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (target != null)
        {
            transform.position = target.TransformPoint(offSet);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotateSpeed * Time.deltaTime);
        }
    }
}
