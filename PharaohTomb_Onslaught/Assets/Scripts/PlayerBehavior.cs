using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public int health;
    float vHorizontal, vVertical, mSpeed, orbitDistance, orbitDegreesPerSec;
    public GameObject tower;

    void Start () {
        health = 50;
        mSpeed = 0.5f;
        orbitDistance = 2.0f;
        orbitDegreesPerSec = 200.0f;
    }
	
	void Update () {
        Move();
        //Orbit();
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

    void Orbit()
    {
        if (tower != null)
        {
            // Keep us at orbitDistance from target
            transform.position = tower.transform.position + (transform.position - tower.transform.position).normalized * orbitDistance;
            transform.RotateAround(tower.transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }
}
