using UnityEngine;
using System.Collections;

public class PlayerArmBehavior : MonoBehaviour {

    //float vHorizontal, vVertical, mSpeed, orbitDistance, orbitDegreesPerSec;
    //public GameObject tower;

    // Use this for initialization
    void Start () {
        //mSpeed = 0.5f;
        //orbitDistance = 2.0f;
        //orbitDegreesPerSec = 200.0f;
    }
	
	// Update is called once per frame
	void Update () {
        //AimAtMouse();
        //Move();
	}

    void Move()
    {
        ////reused from Lab1
        //vHorizontal = Input.GetAxis("Horizontal");
        //if (!Mathf.Approximately(vHorizontal, 0.0f))
        //{
        //    transform.position = tower.transform.position + (transform.position - tower.transform.position).normalized * orbitDistance;
        //    transform.RotateAround(tower.transform.position, Vector3.up, vHorizontal * orbitDegreesPerSec * Time.deltaTime);
        //}
    }

    //void AimAtMouse()
    //{
    //    var mouse = Input.mousePosition;
    //    var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
    //    var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
    //    var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(90, 0, angle);
    //}

}
