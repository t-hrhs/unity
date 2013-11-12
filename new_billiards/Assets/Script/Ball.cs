using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	private bool selected = false;
	private Vector3 screenPoint;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		if (this.gameObject == SelectedWithMouse.selectedGameObject) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
		else if (Input.GetMouseButtonUp(0) && selected){
			Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
        	Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
			Vector3  first_velocity =  - 5 *(currentPosition -  transform.position);
			first_velocity.y = 0.5f;
			this.rigidbody.velocity = first_velocity;
			selected = false;
		}
	}
}
