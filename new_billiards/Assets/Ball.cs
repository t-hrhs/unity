using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject == SelectedWithMouse.selectedGameObject) {
			this.rigidbody.velocity = new Vector3(15.0f,0.0f,10.0f);
			SelectedWithMouse.selectedGameObject = null;
		}
	}
}
