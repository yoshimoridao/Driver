using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMap : MonoBehaviour
{
    public enum LaneId { right = 0, middle, left };
    public Transform lane1T;
    public Transform lane2T;
    public Transform lane3T;
    [SerializeField] bool isRandomPolicePosMode = false;
    [SerializeField] Transform policePosParent;
    public Transform farLimitPntT;
    public Transform nearLimitPntT;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Vector3 GetRandomPolicePosition()
    {
        var policePos = policePosParent.GetChild(Random.Range(0, policePosParent.childCount)).position;
        // random X
        if (isRandomPolicePosMode)
        {
            policePos.x = Random.Range(policePosParent.GetChild(0).position.x, policePosParent.GetChild(policePosParent.childCount - 1).position.x);
        }

        return policePos;
    }

    public Transform GetLanePosition(PropertyMap.LaneId lane)
    {
        if (lane == PropertyMap.LaneId.left)
            return lane1T;
        else if (lane == PropertyMap.LaneId.middle)
            return lane2T;
        else
            return lane3T;
    }
}
