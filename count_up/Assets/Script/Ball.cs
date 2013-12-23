using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public int color_id;
	//public Color color = new Color(1,0,1,1);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void draw_ball(int index) {
		if (index == 0) {
			this.renderer.material.color = Color.cyan;
		} else if (index == 1) {
			this.renderer.material.color = Color.yellow;
		//} else if (index == 2) {
		//	this.renderer.material.color = Color.magenta;
		} else {
			this.renderer.material.color = Color.red;
		}
	}
	void increment_and_draw_ball() {
		if (this.color_id == 2) {
			this.color_id = 0;
		}
		else {
			this.color_id = this.color_id + 1;
		}
		draw_ball(this.color_id);
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "MyBall") {
			this.increment_and_draw_ball();
		}
	}
}
