using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.rigidbody.velocity = new Vector3(-8.0f, 8.0f,  0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// executed when the ball is out of screen
	void OnBecameInvisible() {
		Destroy(this.gameObject);	
	}
}
