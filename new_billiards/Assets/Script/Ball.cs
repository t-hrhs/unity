using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private bool selected = false;
	private Vector3 screenPoint;
	public int hp;
	public int attack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		if (this.gameObject == SelectedWithMouse.selectedGameObject && this.renderer.material.color ==  GameController.selected_ball_color) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
		else if (Input.GetMouseButtonUp(0) && selected && this.renderer.material.color ==  GameController.selected_ball_color){
			GameController.select_ok = false;
			selected = false;
			Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
        	Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
			Vector3  first_velocity =  - 5 *(currentPosition -  transform.position);
			first_velocity.y = 0.5f;
			this.rigidbody.velocity = first_velocity;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball" && collision.gameObject.renderer.material.color !=  GameController.selected_ball_color) {
			GameObject another_ball_prefab = collision.gameObject;
			Ball another_ball = another_ball_prefab.GetComponent<Ball>();
			//make the ball small
			if (another_ball_prefab.transform.localScale.x >= 0.8) {
				another_ball_prefab.transform.localScale = new Vector3(
					another_ball_prefab.transform.localScale.x * 0.9f,
					another_ball_prefab.transform.localScale.y * 0.9f,
					another_ball_prefab.transform.localScale.z * 0.9f
					);
				Debug.Log(another_ball_prefab.transform.localScale.x);
			}
			if (another_ball.hp - this.attack > 0){
				another_ball.hp = another_ball.hp - this.attack;

			} else {
				another_ball.hp = 0;
			}
		}
	}
}
