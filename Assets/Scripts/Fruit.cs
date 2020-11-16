using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private bool isCollided = false;
    private Rigidbody rb;
    private Collider collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();

        rb.isKinematic = true;
        collider.enabled = false;   // default disable collider
    }

    public bool IsCollided()
    {
        return isCollided;
    }

    public void OnThrow(Vector3 force, Vector3 torque)
    {
        rb.isKinematic = false;
        rb.AddForce(force);
        rb.AddTorque(torque);
        collider.enabled = true;
    }

    public void OnCollide()
    {
        if (isCollided)
            return;

        isCollided = true;
        Destroy(this);
        Destroy(gameObject, 5.0f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PropertyMap>())
        {
            OnCollide();
        }
    }
}
