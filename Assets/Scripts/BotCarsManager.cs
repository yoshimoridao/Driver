﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCarsManager : MonoBehaviour
{
    public static BotCarsManager Instance;
    [SerializeField] PlayerCarController playerCar;
    [SerializeField] int amountCars = 3;
    [SerializeField] Transform lane1T;
    [SerializeField] Transform lane2T;
    [SerializeField] Transform lane3T;
    [SerializeField] List<GameObject> carPrefs = new List<GameObject>();
    [SerializeField] List<Transform> spawnCarTrans = new List<Transform>();

    private const int MIN_SPAWN_CAR = 1;
    private const float DELAY_SPAWN_NEXT_CAR = 7.0f;
    private Vector2 RANGE_EULER_Y = new Vector2(-60.0f, 60.0f);

    private List<BotCarController> spawnCars = new List<BotCarController>();
    private float spawnTime;

    public PlayerCarController GetPlayerCar()
    {
        return playerCar;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnTime = DELAY_SPAWN_NEXT_CAR;
    }

    void Update()
    {
        UpdateSpawnCars();
    }

    private void UpdateSpawnCars()
    {
        if (spawnCars.Count >= MIN_SPAWN_CAR)
            return;

        if (spawnTime > 0.0f)
            spawnTime -= Time.deltaTime;

        if (spawnTime <= 0.0f && spawnCars.Count < MIN_SPAWN_CAR)
        {
            var lastGround = MapMgr.Instance.GetLastProperty();
            if (lastGround.position.x >= playerCar.transform.position.x)
            {
                spawnTime = DELAY_SPAWN_NEXT_CAR;
                SpawnCar();
            }
        }
    }

    public void SpawnCar()
    {
        // random cars
        var carpref = carPrefs[Random.Range(0, carPrefs.Count)];
        var genCar = Instantiate(carpref, transform).GetComponent<BotCarController>();
        genCar.SetLane(lane2T);     // default = mid lane

        // random position
        var spawnPos = spawnCarTrans[Random.Range(0, spawnCarTrans.Count)].position;
        spawnPos.x = MapMgr.Instance.GetRandomPolicePosition().x;
        genCar.transform.position = spawnPos;

        // random rotation y
        genCar.transform.eulerAngles = new Vector3(0.0f, Random.Range(RANGE_EULER_Y.x, RANGE_EULER_Y.y), 0.0f);

        spawnCars.Add(genCar);
    }

    public void RemoveSpawnCar(GameObject car)
    {
        int id = spawnCars.FindIndex(x => x.gameObject == car.gameObject);
        if (id != -1)
            spawnCars.RemoveAt(id);
    }
}
