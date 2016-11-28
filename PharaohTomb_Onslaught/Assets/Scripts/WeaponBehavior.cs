using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Fire()
    {
        //following this: http://answers.unity3d.com/questions/793987/get-component-in-raycasthit-game-object.html
        Vector3 fwd = transform.TransformDirection(Vector3.left);
        float range = 50.0f;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, fwd);
        Debug.DrawRay(transform.position, fwd, Color.green);
        if (Physics.Raycast(ray, out hit, range))
        {
            int dmg = 1;
            if (hit.collider.name == "Tower")//shooting through the tower inscreases the damage
            {
                print("Tower was shot through");
                dmg *= 3;
                ray = new Ray(new Vector3(0,0,0), fwd);//redraw ray from tower
                Physics.Raycast(ray, out hit, range);
            }
            if (hit.collider != null && hit.collider.tag == "Mummy")
            {
                print("hit a Mummy");
                hit.collider.gameObject.GetComponent<MummyBehavior>().TakeDamage(dmg);
            }
        }
    }
}
