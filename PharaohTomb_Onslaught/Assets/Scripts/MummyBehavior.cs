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

	void Start () {
        animator = GetComponent<Animator>();
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
        FloatingTextController.CreateFloatingText((-dmg).ToString(), transform);
        health = health - dmg;
        if (health <= 0)
        {
            //Destroy(gameObject);
            isDead = true;
            deadTime = Time.time;
            return;
        }
        animator.SetTrigger("isHit");
    }

    public void setSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

}
