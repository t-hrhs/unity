using UnityEngine;
using System.Collections;

public class Main_Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.rigidbody.velocity = new Vector3(8.0f,0.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) { 
			this.rigidbody.velocity = new Vector3(8.0f,0.0f,0.0f);	
		}
	}
}
