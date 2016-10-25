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



        //if (Physics.Raycast(transform.position, fwd, 10))
        //{
        //    print("There is something in front of the object!");
        //}


        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var hit : RaycastHit;
        //if (Physics.Raycast(ray, hit, 100))
        //{
        //    if (hit.collider.gameObject.find("wormyguy"))
        //    {
        //        Debug.Log("Success");
        //    }
        //}
    }


}
