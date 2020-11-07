using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : SimpleCarController
{
    protected override void Start()
    {
        base.Start();
        this.m_horizontalInput = 0.0f;
        this.m_verticalInput = 1.0f;
    }

    void Update()
    {
        
    }

    protected override void Steer()
    {
        if (laneNode)
        {
            var lanePos = laneNode.position;
            var relativeVector = transform.InverseTransformPoint(transform.position.x + 3.0f, lanePos.y, lanePos.z);
            m_horizontalInput = (relativeVector.x / relativeVector.magnitude);
        }

        base.Steer();
    }
}
