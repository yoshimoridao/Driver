using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotCarController : SimpleCarController
{
    public enum MvmState { WaitPlayer, Chase };
    public enum CarState { Well = 0, Damage, Serious, Critical };

    private const float DISTANCE_CHASE = 5.0f;

    private MvmState mvmState = MvmState.WaitPlayer;
    private CarState carState = CarState.Well;

    public Action<GameObject> actOnDestroyed;
    [SerializeField] List<Material> carMaterials = new List<Material>();
    MeshRenderer meshRd;

    protected override void Start()
    {
        meshRd = GetComponent<MeshRenderer>();

        base.Start();
        this.m_horizontalInput = 0.0f;
        this.m_verticalInput = 0.0f;
    }

    void Update()
    {
        if (mvmState == MvmState.WaitPlayer)
        {
            var playerCar = BotCarsManager.Instance.GetPlayerCar();
            if (playerCar.transform.position.x - transform.position.x >= DISTANCE_CHASE)
            {
                mvmState = MvmState.Chase;
                m_verticalInput = 1.0f;
            }
        }
    }

    private void OnDestroy()
    {
        if (actOnDestroyed != null)
        {
            actOnDestroyed.Invoke(gameObject);
            actOnDestroyed = null;
        }
    }

    protected override void Steer()
    {
        if (laneNode && mvmState == MvmState.Chase)
        {
            var lanePos = laneNode.position;
            var relativeVector = transform.InverseTransformPoint(transform.position.x + 3.0f, lanePos.y, lanePos.z);
            m_horizontalInput = (relativeVector.x / relativeVector.magnitude);
        }

        base.Steer();
    }

    private void OnCollisionEnter(Collision col)
    {
        var fruit = col.gameObject.GetComponent<Fruit>();
        if (fruit && !fruit.IsCollided())
        {
            fruit.OnCollide();
            OnDamage();
        }
    }
    public void OnDamage()
    {
        var curState = carState;
        
        switch (curState)
        {
            case CarState.Well:
                curState = CarState.Damage;
                break;
            case CarState.Damage:
                curState = CarState.Serious;
                break;
            case CarState.Serious:
                curState = CarState.Critical;
                m_verticalInput = 0.0f;
                BotCarsManager.Instance.RemoveSpawnCar(gameObject);
                break;
            case CarState.Critical:
                break;
            default:
                break;
        }

        if (curState != carState)
        {
            carState = curState;
            int id = (int)carState - 1;
            if (id >= 0 && id < carMaterials.Count)
            {
                meshRd.material = carMaterials[id];
            }
        }
    }
}
