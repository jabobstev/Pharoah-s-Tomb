using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBehavior : MonoBehaviour {

    bool isHandgun;
    bool isShotgun;

    public Sprite handgunSprite;
    public Sprite shotgunSprite;

    SpriteRenderer weaponSpriteRenderer;

    // Use this for initialization
    void Start () {
        isHandgun = true;
        isShotgun = false;
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        swapWeapon();
    }

    public void Fire()
    {
        if(isHandgun)
            HandgunFire();
        if (isShotgun)
            ShotgunFire();
    }

    void HandgunFire()
    {
        //following this: http://answers.unity3d.com/questions/793987/get-component-in-raycasthit-game-object.html
        Vector3 fwd = transform.TransformDirection(Vector3.left);
        float range = 50.0f;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, fwd);
        Debug.DrawRay(transform.position, fwd, Color.green);
        if (Physics.Raycast(ray, out hit, range))
        {
            int dmg = 3;
            if (hit.collider.name == "Tower")//shooting through the tower inscreases the damage
            {
                dmg *= 3;
                ray = new Ray(new Vector3(0, 0, 0), fwd);//redraw ray from tower
                Physics.Raycast(ray, out hit, range);
            }
            if (hit.collider != null && (hit.collider.tag == "Mummy" || hit.collider.tag == "Sarc"))
            {
                //this is due in 3 days and there's no time to care anymore
                if (hit.collider.tag == "Mummy")
                    hit.collider.gameObject.GetComponent<MummyBehavior>().TakeDamage(dmg);
                if (hit.collider.tag == "Sarc")
                    hit.collider.gameObject.GetComponent<SarcBehavior>().TakeDamage(dmg);
            }
        }
    }

    void ShotgunFire()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.left);
        Vector3 fwdL = Quaternion.AngleAxis(25, Vector3.up) * fwd;
        Vector3 fwdR = Quaternion.AngleAxis(25, Vector3.down) * fwd;
        float range = 7.0f;
        RaycastHit hit;
        List<Ray> rayList = new List<Ray>();
        rayList.Add(new Ray(transform.position, fwd));
        rayList.Add(new Ray(transform.position, fwdL));
        rayList.Add(new Ray(transform.position, fwdR));
        foreach (Ray ray in rayList)
        {
            Debug.DrawRay(transform.position, fwd, Color.green);
            if (Physics.Raycast(ray, out hit, range))
            {
                float dmg = 1f;
                if (hit.collider.name == "Tower")//shooting through the tower inscreases the damage
                {
                    dmg *= 3;
                    Ray tempRay = new Ray(new Vector3(0, 0, 0), fwd);//redraw ray from tower
                    Physics.Raycast(tempRay, out hit, range);
                }
                if (hit.collider != null && (hit.collider.tag == "Mummy" || hit.collider.tag == "Sarc"))
                {
                    if(hit.collider.tag == "Mummy")
                        hit.collider.gameObject.GetComponent<MummyBehavior>().TakeDamage(dmg);
                    if (hit.collider.tag == "Sarc")
                        hit.collider.gameObject.GetComponent<SarcBehavior>().TakeDamage(dmg);
                }
            }
        }
    }

    void swapWeapon()
    {
        if (Input.GetKeyDown("s"))
        {
            if (isShotgun)
            {
                print("set to handgun");
                weaponSpriteRenderer.sprite = handgunSprite;
                isShotgun = false;
                isHandgun = true;
                return;
            }
            if (isHandgun)
            {
                print("set to shotgun");
                weaponSpriteRenderer.sprite = shotgunSprite;
                isHandgun = false;
                isShotgun = true;
                return;
            }
        }
    }

}
