using UnityEngine;
using System.Collections;

public class MummyBehavior : MonoBehaviour {

	public float moveSpeed = 1.0f;

	Vector3 towerPosition;
	Vector3 walkDirection;

    Animator animator;
    public float health;
    bool isDead;
    float deadTime;

    AudioSource audioSource;
    public AudioClip MummySounds1;
    public AudioClip MummySounds2;
    public AudioClip MummyHit1;
    public AudioClip MummyHit2;
    public AudioClip MummyHit3;
    public AudioClip MummyHit4;
    public AudioClip MummyDie;

    void Start () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = 12;

		//Find the tower and remember its location
		towerPosition = GameObject.FindGameObjectWithTag ("Tower").transform.position;

        //if tower is to the LEFT of their position, flip the sprite
        if (transform.position.x > 0)//assumption that tower will always remain at (0,0)
        {
            SpriteRenderer SpriteRend = GetComponent<SpriteRenderer>();
            SpriteRend.flipX = false;//since original mummy sprite is backwards
        }

		walkDirection = towerPosition - transform.position;
		//Flatten it in 2D (xz plane)
		walkDirection = new Vector3(walkDirection.x, 0, walkDirection.z);

        //set up audio
        float choice = Random.Range(0f, 1.0f);
        if (choice < 0.5f)
            audioSource.clip = MummySounds1;
        else
            audioSource.clip = MummySounds2;
        audioSource.Play();
    }
	
	void Update () {
        if (TowerBehavior.isDead)
            return;
        UpdateAnimations();
        if (isDead)
            return;
        Move();
        Attack();
	}

    void Move()
    {
        //Move mummy towards tower in center if they are not within range of attacking

		//Move mummy relative to world axis
		transform.Translate(Time.deltaTime * moveSpeed * walkDirection, Space.World);

    }

    void Attack()
    {
        //TODO
        //if they are close enough to the player to attack, attack the player
        //if they are close enough to the tower to attack, attack the tower
    }

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Tower")
			Destroy (this.gameObject);
	}

    void UpdateAnimations()
    {
        //TODO
        animator.SetBool("isDead", isDead);
        if (isDead && Time.time - deadTime > 5.0f)
            Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        if (isDead)
            return;
        PlayHitSound();
        GameObject.Find("FloatingText").GetComponent<FloatingTextController>().CreateFloatingText((-dmg).ToString(), transform);
        health = health - dmg;
        if (health <= 0)
        {
            //Destroy(gameObject);
            isDead = true;
            deadTime = Time.time;
            audioSource.PlayOneShot(MummyDie, 0.6f);
            return;
        }
        animator.SetTrigger("isHit");
    }

    public void setSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void PlayHitSound()
    {
        int choice = (int)Random.Range(1f, 5f);
        if (choice == 1)
            audioSource.PlayOneShot(MummyHit1, 0.4f);
        else if(choice == 2)
            audioSource.PlayOneShot(MummyHit2, 0.4f);
        else if (choice == 3)
            audioSource.PlayOneShot(MummyHit3, 0.4f);
        else
            audioSource.PlayOneShot(MummyHit4, 0.4f);
    }

}
