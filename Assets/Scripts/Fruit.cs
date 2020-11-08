using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void Start()
    {
    }

    public void OnCollide()
    {
        Destroy(this);
        Destroy(gameObject, 5.0f);
    }
}
