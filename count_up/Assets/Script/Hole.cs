using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag == "Ball") {
			collision.gameObject.transform.position = new Vector3(0,-5,0);
			Ball ballscript = collision.gameObject.GetComponent<Ball>();
			GameController.score +=  (ballscript.color_id + 1) * GameController.turn;
		} else if (collision.gameObject.tag == "MyBall") {
			//My Ball goes back to start position
			collision.gameObject.rigidbody.velocity = new Vector3(0,0,0);
			collision.gameObject.transform.position = new Vector3((float)-5,(float)0.5f,1);
		}
	}
}
