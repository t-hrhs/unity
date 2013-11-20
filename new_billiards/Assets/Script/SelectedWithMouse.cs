using UnityEngine;
using System.Collections;

public class SelectedWithMouse : MonoBehaviour {
	public static GameObject selectedGameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// when all balls stop
		if (GameController.select_ok)  {
			get_touched_object_by_mouse();
		}
	}
	//Get mouse information
	void get_touched_object_by_mouse(){
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("left click");
			Ray ray;
			RaycastHit hit = new RaycastHit();
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out hit)) {
				selectedGameObject = hit.collider.gameObject;	
			} else {
				selectedGameObject = null;	
			}
		}
	}
}
