using UnityEngine;
using System.Collections;

public class EnemySpawnerBehavior : MonoBehaviour {

    float SpawnRate = 6f;
    public GameObject Enemy;

	// Use this for initialization
	void Start () {
        Invoke("SpawnEnemy", 2f);
    }
	
	// Update is called once per frame
	void Update () {
	
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


        if (SpawnRate - 0.5 >= 1)
        {
            SpawnRate -= 0.5f;
        }
        NextEnemySpawn(SpawnRate);
    }

    void NextEnemySpawn(float rate)
    {
        Invoke("SpawnEnemy", rate);
    }
}
