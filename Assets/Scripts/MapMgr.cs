using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    public static MapMgr Instance;
    public Transform playerCarT;
    public float spawnDistance = 10.0f;
    public float destroyDistance = 100.0f;

    [SerializeField] GameObject prefGround;
    private Transform nearLimitPntT;
    private Transform farLimitPntT;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (transform.childCount > 0)
        {
            var nearGround = transform.GetChild(0);
            nearLimitPntT = nearGround.GetChild(0);

            var farGround = transform.GetChild(transform.childCount - 1);
            farLimitPntT = farGround.GetChild(farGround.childCount - 1);
        }
    }

    void Update()
    {
        if (farLimitPntT && Mathf.Abs(farLimitPntT.position.x - playerCarT.position.x) <= spawnDistance)
        {
            GenerateNextGround();
        }
        // destroy first ground
        if (nearLimitPntT && Mathf.Abs(playerCarT.position.x - nearLimitPntT.position.x) >= destroyDistance)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        if (nearLimitPntT == null && transform.childCount > 0)
            nearLimitPntT = transform.GetChild(0);
    }

    private void GenerateNextGround()
    {
        var nextGround = Instantiate(prefGround, transform).transform;
        float offsetX = nextGround.position.x - nextGround.GetChild(0).position.x;
        var genPos = farLimitPntT.position;
        genPos.x += offsetX;
        nextGround.position = genPos;

        this.farLimitPntT = nextGround.GetChild(nextGround.childCount - 1);
    }

    public Transform GetLastGround()
    {
        return transform.GetChild(transform.childCount - 1);
    }
}
