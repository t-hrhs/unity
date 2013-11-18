using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Ball disappear
	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball") {
			 Destroy(collision.gameObject);
		}
	}
}
