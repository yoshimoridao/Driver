using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : SimpleCarController
{
    List<BotCarController> hitCars = new List<BotCarController>();

    protected override void Start()
    {
        base.Start();
        this.m_horizontalInput = 0.0f;
        this.m_verticalInput = 1.0f;
    }

    void Update()
    {
        if (m_verticalInput != 1.0f)
        {
            m_verticalInput = Mathf.Lerp(m_verticalInput, 1.0f, 0.1f);
            if (m_verticalInput <= 1.001f)
            {
                m_verticalInput = 1.0f;
            }
        }
    }

    protected override void Accelerate()
    {
        base.Accelerate();
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

    private void OnCollisionEnter(Collision col)
    {
        // collide with bot
        var botCar = col.gameObject.GetComponent<BotCarController>();
        if (!botCar)
            return;

        int id = hitCars.FindIndex(x => x.gameObject == col.gameObject);
        if (id != -1)
            return;

        m_verticalInput += botCar.motorForce;
        base.Accelerate();

        hitCars.Add(botCar);

        botCar.actOnDestroyed += OnBotCarDestroy;
    }

    public void OnBotCarDestroy(GameObject obj)
    {
        int id = hitCars.FindIndex(x => x.gameObject == obj);
        if (id != -1)
        {
            hitCars.RemoveAt(id);
        }
    }
}
