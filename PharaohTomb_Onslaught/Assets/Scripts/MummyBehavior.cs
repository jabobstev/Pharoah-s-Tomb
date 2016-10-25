using UnityEngine;
using System.Collections;

public class MummyBehavior : MonoBehaviour {

    Animator animator;
    public int health;
    bool isDead;

	void Start () {
        animator = GetComponent<Animator>();
        health = 3;
    }
	
	void Update () {
        UpdateAnimations();
        if (isDead)
            return;
        Move();
        Attack();
	}

    void Move()
    {
        //TODO
        //Move mummy towards tower in center if they are not within range of attacking
    }

    void Attack()
    {
        //TODO
        //if they are close enough to the player to attack, attack the player
        //if they are close enough to the tower to attack, attack the tower
    }

    void UpdateAnimations()
    {
        //TODO
        //update the isDead that the animator will have in order to launch a death sequence
    }

    public void TakeDamage(int dmg)
    {
        health = health - dmg;
        if (health <= 0)
            Destroy(gameObject);
    }

}
