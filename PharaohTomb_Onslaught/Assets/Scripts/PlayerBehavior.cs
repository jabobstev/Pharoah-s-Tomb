using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    Animator animator;
    public int health;
    float vHorizontal, vVertical, mSpeed, orbitDistance, orbitDegreesPerSec;
    public GameObject tower;
    public bool playerShot;

    void Start () {
        animator = GetComponent<Animator>();
        health = 50;
        mSpeed = 0.5f;
        orbitDistance = 2.0f;
        orbitDegreesPerSec = 200.0f;
        playerShot = false;
    }
	
	void Update () {
        Move();
        Shoot();
        UpdateAnimations();
    }

    void Move()
    {
        //reused from Lab1
        vHorizontal = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(vHorizontal, 0.0f))
        {
            transform.position = tower.transform.position + (transform.position - tower.transform.position).normalized * orbitDistance;
            transform.RotateAround(tower.transform.position, Vector3.up, vHorizontal*orbitDegreesPerSec * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("FIRE!");
            playerShot = true;
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("playerShot", playerShot);
    }
}
