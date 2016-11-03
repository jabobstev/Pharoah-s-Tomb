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
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.tag == "Mummy")
            {
                print(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<MummyBehavior>().TakeDamage(1);
            }
        }
    }
}
