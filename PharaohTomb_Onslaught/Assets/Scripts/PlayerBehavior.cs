using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    Animator animator;
    public int health;
    float vHorizontal, vVertical, mSpeed, orbitDistance, orbitDegreesPerSec;
    public GameObject tower;
    public bool playerShot;
    public WeaponBehavior weapon;

    public GameObject arm;

    void Start () {
        animator = GetComponent<Animator>();
        health = 50;
        mSpeed = 0.5f;
        orbitDistance = 2.0f;
        orbitDegreesPerSec = 200.0f;
        playerShot = false;
    }
	
	void Update () {
        if (TowerBehavior.isDead)
            return;
        Move();
        AimBody();
        Shoot();
        UpdateAnimations();
    }

    void Move()
    {
        //Transform target = tower.transform;
        //var mouse = Input.mousePosition;
        //var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        //var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        //var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(90, 0, angle);



        //reused from Lab1
        vHorizontal = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(vHorizontal, 0.0f))
        {
            transform.position = tower.transform.position + (transform.position - tower.transform.position).normalized * orbitDistance;
            transform.RotateAround(tower.transform.position, Vector3.up, vHorizontal * orbitDegreesPerSec * Time.deltaTime);
        }
    }

    void AimBody()
    {
        //FLIP LOGIC
        //if on the LEFT of tower and aiming RIGHT
        if (transform.position.x < tower.transform.position.x)//on left of tower
        {
            float ninety = Quaternion.Euler(0, 0, 90).z;
            if (transform.rotation.z > ninety)//aiming to the right
            {
                //This doesn't work for some reason, so screw it, Indy can fight upside down
                print("THIS IS WHEN INDY SHOULD FLIP!");
                transform.rotation = Quaternion.Euler(transform.rotation.x + 180, transform.rotation.y + 90, transform.rotation.z + 90);
            }
        }

        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 0, angle + 180);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Fire();
            playerShot = true;
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("playerShot", playerShot);
    }
}
