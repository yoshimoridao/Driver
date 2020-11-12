using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private bool isCollided = false;

    private void Start()
    {
    }

    public bool IsCollided()
    {
        return isCollided;
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
