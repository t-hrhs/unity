using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public const float INTERVAL = 0.1f;
	public float timer = INTERVAL;
	private bool minus =  false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = INTERVAL;
			float temp = this.transform.position.z;
			if (temp <= 5.0f) {
				minus =  false;
			} else if (temp >= 7.5f) {
				minus = true;
			}
			if (minus) {
				temp = temp - INTERVAL;
			} else {
				temp = temp + INTERVAL;
			}
			this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,temp);
		}
	}
}
