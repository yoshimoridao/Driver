using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMap : MonoBehaviour
{
    [SerializeField] bool isRandomPolicePosMode = false;
    [SerializeField] Transform policePosParent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Vector3 GetRandomPolicePosition()
    {
        Vector3 policePos = Vector3.zero;
        // random X
        if (isRandomPolicePosMode)
        {
            policePos.x = Random.Range(policePosParent.GetChild(0).position.x, policePosParent.GetChild(policePosParent.childCount - 1).position.x);
        }
        else
        {
            policePos = policePosParent.GetChild(Random.Range(0, policePosParent.childCount)).position;
        }

        return policePos;
    }
}
