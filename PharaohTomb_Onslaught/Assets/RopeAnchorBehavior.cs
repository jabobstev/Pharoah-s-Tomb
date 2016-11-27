using UnityEngine;
using System.Collections;

public class RopeAnchorBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(-0f, 1.51f, 14.76f);
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
