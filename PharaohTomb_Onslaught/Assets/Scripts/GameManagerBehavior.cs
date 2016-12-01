using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {
    public PlayerBehavior player;
    public TowerBehavior tower;
    public EnemySpawnerBehavior spawner;
    public static bool hasToReset = false;
    public static bool isGameOver = false;
    public Text scoreDisplay;
    public Text artefactDisplay;
    private int score;
    private float lastScoreTime;

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
        scoreDisplay.text = score.ToString();
        artefactDisplay.text = tower.health.ToString();
        
        if (Time.time - lastScoreTime > .350)
        {
            score += 10;
            lastScoreTime = Time.time;
        }
    }

    public void AddScore(int val)
    {
        score += val;
    }

    public void Reset()
    {
        var objects = GameObject.FindGameObjectsWithTag("Mummy");
        foreach (GameObject o in objects)
        {
            Destroy(o.gameObject);
        }

        objects = GameObject.FindGameObjectsWithTag("Sarc");
        foreach (GameObject o in objects)
        {
            Destroy(o.gameObject);
        }
        //player.Reset();
        spawner.Reset();
        tower.Reset();
        score = 0;
        lastScoreTime = Time.time;
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
