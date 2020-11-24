using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCarsManager : MonoBehaviour
{
    public static BotCarsManager Instance;
    [SerializeField] PlayerCarController playerCar;
    [SerializeField] int amountCars = 3;
    [SerializeField] List<GameObject> carPrefs = new List<GameObject>();

    private const int MIN_SPAWN_CAR = 1;
    private const float DELAY_SPAWN_NEXT_CAR = 7.0f;

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
            if (lastGround.transform.position.x >= playerCar.transform.position.x)
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

        // set property road for car
        genCar.OnSpawn(MapMgr.Instance.GetLastProperty());         // default = mid lane

        spawnCars.Add(genCar);
    }

    public void RemoveSpawnCar(GameObject car)
    {
        int id = spawnCars.FindIndex(x => x.gameObject == car.gameObject);
        if (id != -1)
            spawnCars.RemoveAt(id);
    }
}
