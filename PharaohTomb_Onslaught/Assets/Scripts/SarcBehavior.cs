using UnityEngine;
using System.Collections;

public class SarcBehavior : MonoBehaviour {

    float health;
    Animator animator;
    float deadTime;
    bool isDead;
    float mummySpawnDelay;
    public GameObject Mummy;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        health = 200;
        animator = GetComponent<Animator>();
        mummySpawnDelay = Time.time;
        EnemySpawnerBehavior.hasASarc = true;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead && Time.time - deadTime > 5.0f)
        {
            Destroy(gameObject);
        }
        SpawnMummies();
	}

    void SpawnMummies()
    {
        if (Time.time - mummySpawnDelay > 0.5f)
        {
            float choice = Random.Range(0f, 1.0f);
            if (choice >= 0.4f)
            {
                print("mummy spawned from sarc");
                Vector3 pos = transform.position;
                GameObject enemy = (GameObject)Instantiate(Mummy);
                Mummy.transform.position = new Vector3(pos.x, -0.25f, pos.z);//the y layer for mummies

                float newSpeed = Random.Range(0.05f, 0.1f);
                enemy.GetComponent<MummyBehavior>().setSpeed(newSpeed);
                enemy.GetComponent<MummyBehavior>().health /= 2;
            }
            mummySpawnDelay = Time.time;
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        FloatingTextController.CreateFloatingText((-dmg).ToString(), transform);
        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            deadTime = Time.time;
            EnemySpawnerBehavior.hasASarc = false;
            EnemySpawnerBehavior.SarcSpawnDelay = Time.time;
            EnemySpawnerBehavior.nextSarcSpawnDelay = Random.Range(30f, 45f);
            return;
        }
        animator.SetTrigger("isHit");
    }

}
