using UnityEngine;
using System.Collections;

public class SelectedWithMouse : MonoBehaviour {
	public static GameObject selectedGameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)){
			Ray ray;
			RaycastHit hit = new RaycastHit();
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction * 10, Color.yellow);
			if (Physics.Raycast(ray,out hit)) {
				selectedGameObject = hit.collider.gameObject;	
			} else {
				selectedGameObject = null;	
			}
		}
	}
}
