using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    public int health;
    public int charge;

    // Use this for initialization
    void Start () {
        health = 100;
        charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAnimations();
        BuildCharge();
	}

    void OnCollisionEnter(Collision col)
    {
        //TODO
        if (col.gameObject.name == "mummy?")
        {
            //take damage
        }
    }

    void BuildCharge()
    {
        //TODO
    }

    void UpdateAnimations()
    {
        //TODO
        //handles all animator variable
    }
}
