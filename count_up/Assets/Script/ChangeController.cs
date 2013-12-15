using UnityEngine;
using System.Collections;

public class ChangeController : MonoBehaviour {
	public GameObject coin;
	// Use this for initialization
	void Start () {
		float i,j;
		//lower stage
		for (i=-3.1f;i<5.3f;i++) {
			for (j= 0.5f;j>-3.3f;j--) {
				Instantiate(coin, new Vector3(i, 1.0f,j),Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentScreenPoint = new Vector3(0.0f, 10.0f,  0.0f);
		if (Input.GetMouseButtonUp(0)) {
			 Instantiate(coin, currentScreenPoint,Quaternion.identity);
		}
	}
}
