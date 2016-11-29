using UnityEngine;
using System.Collections;

public class RopeBheavior : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
	}
}
