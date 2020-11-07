using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCarController : SimpleCarController
{
    private const float DISTANCE_CHASE = 5.0f;
    public enum CarState { WaitPlayer, Chase };
    private CarState carState = CarState.WaitPlayer;

    protected override void Start()
    {
        base.Start();
        this.m_horizontalInput = 0.0f;
        this.m_verticalInput = 0.0f;
    }

    void Update()
    {
        if (carState == CarState.WaitPlayer)
        {
            var playerCar = BotCarsManager.Instance.GetPlayerCar();
            if (playerCar.transform.position.x - transform.position.x >= DISTANCE_CHASE)
            {
                carState = CarState.Chase;
                m_verticalInput = 1.0f;
            }
        }
    }

    protected override void Steer()
    {
        if (laneNode && carState == CarState.Chase)
        {
            var lanePos = laneNode.position;
            var relativeVector = transform.InverseTransformPoint(transform.position.x + 3.0f, lanePos.y, lanePos.z);
            m_horizontalInput = (relativeVector.x / relativeVector.magnitude);
        }

        base.Steer();
    }
}
