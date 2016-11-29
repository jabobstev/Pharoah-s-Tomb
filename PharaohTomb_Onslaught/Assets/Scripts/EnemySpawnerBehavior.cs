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
    float sign = -1f;

	// Use this for initialization
	void Start () {
        SarcSpawnDelay = Time.time;
        nextSarcSpawnDelay = 39f;//trust me, it syncs up with the music on the first time
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

        //to make the spawn rate act like a spring
        //THIS IS REALLY WEIRD PLEASE DON'T TOUCH IT!
        if (((SpawnRate - 0.5 >= 1 && sign < 0f) || (SpawnRate + 0.5f <= 6f && sign > 0f)) && !highSpawnRateActive)
        {
            SpawnRate = SpawnRate + sign*0.5f;
        }
        else if ((highSpawnRateActive && Time.time - highSpawnRateTime <= 10) && sign > 0)
        {
           //stay at low rate for 10 seconds
        }
        else if ((highSpawnRateActive && Time.time - highSpawnRateTime <= 20) && sign < 0)
        {
            //stay at high rate for 20 seconds
        }
        else if (Time.time - highSpawnRateTime > 10 && highSpawnRateActive)
        {
            sign = -sign;
            highSpawnRateActive = false;
        }
        else if ((SpawnRate == 1 || SpawnRate == 6) && !highSpawnRateActive)
        {
            highSpawnRateActive = true;
            highSpawnRateTime = Time.time;
        }
        NextEnemySpawn(SpawnRate);
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
