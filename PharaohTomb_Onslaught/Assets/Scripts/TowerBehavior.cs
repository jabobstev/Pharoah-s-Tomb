using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    Animator animator;

    public int health;
    public int charge;
    public static bool isDead; //aka gameover flag
    public Light towerLight;
    public float MAX_LIGHT_INTENSITY = 2f;
    const int MAX_CHARGE_POWER = 5;
    public bool isAtMaxCharge = false;

    AudioSource audioSource;
    public AudioClip ChargeLaunch;
    public AudioClip PowerUpShot;

    float chargeUpdateTime;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            //Debug.Log (health);
        }
    }

	void TakeDamage(int damage){
		health -= damage;
        GameObject.Find("FloatingText").GetComponent<FloatingTextController>().CreateFloatingText("x3", transform, new Color32(218, 15, 20, 255), new Color32(173, 43, 43, 255));
        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
	}

    public void Reset()
    {
        Start();
    }

    void BuildCharge()
    {
        if ((Time.time - chargeUpdateTime) >= 4)
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
        if (Input.GetKeyDown("w"))
        {
            if (isAtMaxCharge)
            {
                audioSource.PlayOneShot(ChargeLaunch, 5f);
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

    public void PowerShot()
    {
        animator.SetTrigger("PowerUpShot");
        GameObject.Find("FloatingText").GetComponent<FloatingTextController>().CreateFloatingText("x3", transform, new Color32(105, 231, 0, 255), new Color32(45, 185, 0, 255));
        audioSource.PlayOneShot(PowerUpShot, 0.6f);
    }

}
