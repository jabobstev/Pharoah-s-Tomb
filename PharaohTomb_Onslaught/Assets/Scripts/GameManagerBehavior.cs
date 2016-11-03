using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {
    public PlayerBehavior player;
    public TowerBehavior tower;
    public EnemySpawnerBehavior spawner;
    public static bool hasToReset = false;
    public static bool isGameOver = false;

	// Use this for initialization
	void Start () {
        Reset();
    }
	
	// Update is called once per frame
	void Update () {
        if (hasToReset)
        {
            Reset();
        }
	    if (TowerBehavior.isDead && !isGameOver)
        {
            GameOver();
        }
	}

    public void Reset()
    {
        var objects = GameObject.FindGameObjectsWithTag("Mummy");
        foreach (GameObject o in objects)
        {
            Destroy(o.gameObject);
        }
        //player.Reset();
        spawner.Reset();
        tower.Reset();
        Time.timeScale = 1.0f;
        isGameOver = false;
        hasToReset = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene("PharaohTomb_Onslaught_GameOver", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }
}
