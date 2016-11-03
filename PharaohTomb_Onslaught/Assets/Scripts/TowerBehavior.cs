using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    public int health;
    public int charge;
    public static bool isDead;//aka gameover flag

    // Use this for initialization
    void Start () {
        health = 100;
        charge = 0;
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAnimations();
        BuildCharge();
	}

    void OnTriggerEnter(Collider col)
    {
        //TODO
        if (col.gameObject.tag == "Mummy")
        {
			TakeDamage (3);
			Debug.Log (health);
        }
    }

	void TakeDamage(int damage){
		health -= damage;
        if (health < 0)
        {
            isDead = true;
            Destroy(this.gameObject);
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
