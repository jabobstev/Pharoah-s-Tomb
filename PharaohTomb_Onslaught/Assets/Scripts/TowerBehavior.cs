using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    Animator animator;

    public int health;
    public int charge;
    public static bool isDead; //aka gameover flag
    public Light towerLight;
    const float MAX_LIGHT_INTENSITY = 7f;
    const int MAX_CHARGE_POWER = 2;
    public bool isAtMaxCharge = false;

    float chargeUpdateTime;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        health = 100;
        charge = 0;
        isDead = false;
        chargeUpdateTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAnimations();
        BuildCharge();
        UpdateLight();
        ReleaseCharge();
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
        }
	}

    public void Reset()
    {
        Start();
    }

    void BuildCharge()
    {
        if ((Time.time - chargeUpdateTime) >= 2)
        {
            if (health < 98)
                health += 2;
            if (charge < MAX_CHARGE_POWER)
                charge++;
            if (charge >= MAX_CHARGE_POWER)
                isAtMaxCharge = true;
            chargeUpdateTime = Time.time;
        }
    }

    void ReleaseCharge()
    {
        if (Input.GetKeyDown("p"))
        {
            if (isAtMaxCharge)
            {
                animator.SetTrigger("LaunchCharge");
                isAtMaxCharge = false;
                HitAllEnemies();
                charge = 0;
            }
        }
    }

    void HitAllEnemies()
    {
        object[] AllMummy = GameObject.FindObjectsOfType(typeof(MummyBehavior));

        foreach (object mummy in AllMummy)
        {
            ((MummyBehavior)mummy).TakeDamage(50);
        }
    }

    void UpdateLight()
    {
        towerLight.intensity = ((health / 100.0f) * MAX_LIGHT_INTENSITY);
    }

    void UpdateAnimations()
    {
        animator.SetBool("isAtMaxCharge", isAtMaxCharge);
    }
}
