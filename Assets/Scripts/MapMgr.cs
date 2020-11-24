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
                var prop = transform.GetChild(i).GetComponent<PropertyMap>();
                if (prop)
                    propertyMaps.Add(prop);
            }
            if (propertyMaps.Count > 0)
            {
                nearLimitPntT = propertyMaps[0].nearLimitPntT;
                farLimitPntT = propertyMaps[propertyMaps.Count - 1].farLimitPntT;
            }
        }
    }

    void Update()
    {
        if (farLimitPntT && Mathf.Abs(farLimitPntT.position.x - playerCarT.position.x) <= spawnDistance)
        {
            GenerateNextProperties();
        }
        // remove farrest property
        if (propertyMaps.Count > 0 && nearLimitPntT && Mathf.Abs(playerCarT.position.x - nearLimitPntT.position.x) >= destroyDistance)
        {
            var nearProp = propertyMaps[0];
            Destroy(nearProp.gameObject);
            propertyMaps.RemoveAt(0);

            // update near limit pnt
            nearProp = propertyMaps[0];
            nearLimitPntT = nearProp.nearLimitPntT;
        }
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

        this.farLimitPntT = nextProp.farLimitPntT;  // update far limit pnt

        propertyMaps.Add(nextProp);
    }

    public PropertyMap GetLastProperty()
    {
        if (propertyMaps.Count > 0)
            return propertyMaps[propertyMaps.Count - 1];

        return null;
    }
    public PropertyMap GetNextProperty(PropertyMap curProp)
    {
        int id = propertyMaps.FindIndex(x => x.gameObject == curProp.gameObject);
        if (id != -1)
        {
            id++;
            if (id < propertyMaps.Count)
                return propertyMaps[id];
        }

        return GetLastProperty();
    }
}
