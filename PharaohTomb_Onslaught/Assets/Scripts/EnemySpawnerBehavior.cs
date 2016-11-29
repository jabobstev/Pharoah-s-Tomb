using UnityEngine;
using System.Collections;

public class EnemySpawnerBehavior : MonoBehaviour {

    float SpawnRate = 6f;
    public GameObject Enemy;
    public GameObject Sarc;

    public static bool hasASarc;
    public static float SarcSpawnDelay;
    public static float nextSarcSpawnDelay;
    float highSpawnRateTime;
    bool highSpawnRateActive;
    float sign = 1f;

	// Use this for initialization
	void Start () {
        SarcSpawnDelay = Time.time;
        nextSarcSpawnDelay = Random.Range(3f, 4f);
        Invoke("SpawnEnemy", 2f);
    }
	
	// Update is called once per frame
	void Update () {
        SpawnSarc();
	}
    
    public void Reset()
    {
        SpawnRate = 6f;
    }

    void SpawnEnemy()
    {
        if (TowerBehavior.isDead)
            return;
        float EnemyY = -0.25f;
        float radius = 12f;
        float angle = Random.value * Mathf.PI * 2;
        Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, EnemyY, Mathf.Sin(angle) * radius);
        GameObject enemy = (GameObject)Instantiate(Enemy);
        enemy.transform.position = pos;

        float newSpeed = Random.Range(0.05f, 0.2f);
        enemy.GetComponent<MummyBehavior>().setSpeed(newSpeed);


        if (SpawnRate - 0.5 >= 1 && !highSpawnRateActive)
        {
            highSpawnRateTime = Time.time;
            SpawnRate = SpawnRate + sign*0.5f;
        }
        else if (highSpawnRateActive && Time.time - highSpawnRateTime <= 20)
        {
            //SpawnRate; no change
        }
        else if (Time.time - highSpawnRateTime > 20)
        {
            sign = -sign;
        }
        NextEnemySpawn(SpawnRate);
        print(SpawnRate);
    }

    void NextEnemySpawn(float rate)
    {
        Invoke("SpawnEnemy", rate);
    }

    void SpawnSarc()
    {
        if (!hasASarc && Time.time - SarcSpawnDelay > nextSarcSpawnDelay)
        {
            Instantiate(Sarc);
        }
    }

}
