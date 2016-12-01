using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBehavior : MonoBehaviour {

    bool isHandgun;
    bool isShotgun;

    int shotTimes;

    public Sprite handgunSprite;
    public Sprite shotgunSprite;

    AudioSource audioSource;
    public AudioClip HandgunShot;
    public AudioClip ShotgunShot;
    public AudioClip WeaponSwitch;
    public AudioClip ShotGunReload;

    SpriteRenderer weaponSpriteRenderer;

    // Use this for initialization
    void Start () {
        isHandgun = true;
        isShotgun = false;
        shotTimes = 0;
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        swapWeapon();
    }

    IEnumerator PlayReloadSound(AudioClip a)
    {
        yield return new WaitForSeconds(0.25f);

        // Code to execute after the delay
        audioSource.PlayOneShot(a, 1.5f);
    }

    public void Fire()
    {
        shotTimes++;
        if (isHandgun)
        {
            HandgunFire();
            if (shotTimes > 6)
            {
                shotTimes = 0;
                StartCoroutine(PlayReloadSound(WeaponSwitch));
            }
        }
        if (isShotgun)
        {
            ShotgunFire();

            if (shotTimes > 4)
            {
                shotTimes = 0;
                StartCoroutine(PlayReloadSound(ShotGunReload));
            }
        }
    }

    void ShootEnemy(Collider col, int dmg)
    {
        //this is due in 3 days and there's no time to care anymore
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().AddScore(dmg * 10); // ^ agreed, no time to care

        if (col.tag == "Mummy")
            col.gameObject.GetComponent<MummyBehavior>().TakeDamage(dmg);
        if (col.tag == "Sarc")
            col.gameObject.GetComponent<SarcBehavior>().TakeDamage(dmg);
    }

    void HandgunFire()
    {
        audioSource.PlayOneShot(HandgunShot, 0.5f);
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
                hit.collider.gameObject.GetComponent<TowerBehavior>().PowerShot();
                dmg *= 3;
                ray = new Ray(new Vector3(0, 0, 0), fwd);//redraw ray from tower
                Physics.Raycast(ray, out hit, range);
            }
            if (hit.collider != null && (hit.collider.tag == "Mummy" || hit.collider.tag == "Sarc"))
            {
                ShootEnemy(hit.collider, dmg);
            }
        }
    }

    void ShotgunFire()
    {
        audioSource.PlayOneShot(ShotgunShot, 0.5f);
        Vector3 fwd = transform.TransformDirection(Vector3.left);
        Vector3 fwdL = Quaternion.AngleAxis(25, Vector3.up) * fwd;
        Vector3 fwdR = Quaternion.AngleAxis(25, Vector3.down) * fwd;
        float range = 15.0f;
        RaycastHit hit;
        List<Ray> rayList = new List<Ray>();
        rayList.Add(new Ray(transform.position, fwd));
        rayList.Add(new Ray(transform.position, fwdL));
        rayList.Add(new Ray(transform.position, fwdR));
        bool powershot = false;
        foreach (Ray ray in rayList)
        {
            Debug.DrawRay(transform.position, fwd, Color.green);
            if (Physics.Raycast(ray, out hit, range))
            {
                int dmg = 1;
                if (hit.collider.name == "Tower")//shooting through the tower inscreases the damage
                {
                    if (!powershot)
                    {
                        hit.collider.gameObject.GetComponent<TowerBehavior>().PowerShot();
                    }
                    powershot = true;

                    dmg *= 3;
                    Ray tempRay = new Ray(new Vector3(0, 0, 0), fwd);//redraw ray from tower
                    Physics.Raycast(tempRay, out hit, range);
                }
                if (hit.collider != null && (hit.collider.tag == "Mummy" || hit.collider.tag == "Sarc"))
                {
                    ShootEnemy(hit.collider, dmg);
                }
            }
        }

    }

    void swapWeapon()
    {
        if (Input.GetKeyDown("s"))
        {
            audioSource.PlayOneShot(WeaponSwitch, 2f);
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
           shotTimes = 0;
        }
    }

}
