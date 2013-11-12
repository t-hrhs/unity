using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {
	public GameObject ball_prefab;
	// Use this for initialization
	void Start () {
		for (int i=0;i<10;i++){
			Instantiate(ball_prefab, new Vector3(i,0,0),Quaternion.identity);	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
