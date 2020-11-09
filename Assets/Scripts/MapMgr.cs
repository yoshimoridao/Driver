using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    public static MapMgr Instance;
    public Transform playerCarT;
    public float spawnDistance = 10.0f;
    public float destroyDistance = 100.0f;

    [SerializeField] List<GameObject> prefProps = new List<GameObject>();
    private Transform nearLimitPntT;
    private Transform farLimitPntT;
    List<PropertyMap> propertyMaps = new List<PropertyMap>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var prop = transform.GetChild(i);
                if (i == 0)
                    nearLimitPntT = prop.GetChild(0);
                if (i == transform.childCount - 1)
                    farLimitPntT = prop.GetChild(prop.childCount - 1);

                propertyMaps.Add(prop.GetComponent<PropertyMap>());
            }
        }
    }

    void Update()
    {
        if (farLimitPntT && Mathf.Abs(farLimitPntT.position.x - playerCarT.position.x) <= spawnDistance)
        {
            GenerateNextProperties();
        }
        // destroy first ground
        if (nearLimitPntT && Mathf.Abs(playerCarT.position.x - nearLimitPntT.position.x) >= destroyDistance)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        if (nearLimitPntT == null && transform.childCount > 0)
            nearLimitPntT = transform.GetChild(0);
    }

    private void GenerateNextProperties()
    {
        var prop = prefProps[Random.Range(0, prefProps.Count)];
        var nextProp = Instantiate(prop, transform).GetComponent<PropertyMap>();
        var nextPropT = nextProp.transform;
        float offsetX = nextPropT.position.x - nextPropT.GetChild(0).position.x;
        var genPos = farLimitPntT.position;
        genPos.x += offsetX;
        nextPropT.position = genPos;

        this.farLimitPntT = nextPropT.GetChild(nextPropT.childCount - 1);

        propertyMaps.Add(nextProp);
    }

    public Transform GetLastProperty()
    {
        return transform.GetChild(transform.childCount - 1);
    }

    public Vector3 GetRandomPolicePosition()
    {
        // get last property
        return propertyMaps[propertyMaps.Count - 1].GetRandomPolicePosition();
    }
}
