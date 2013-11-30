using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private bool selected = false;
	private Vector3 screenPoint;
	public int hp;
	public int hp_max;
	public int attack;
	public string team; //temporary a or b
	public GameObject gauge_prefab;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		if (this.gameObject == SelectedWithMouse.selectedGameObject && this.team ==  GameController.selected_ball_team) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
		else if (Input.GetMouseButtonUp(0) && selected && this.team ==  GameController.selected_ball_team){
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
		if (collision.gameObject.tag == "Ball") {
			GameObject another_ball_prefab = collision.gameObject;
			Ball another_ball = another_ball_prefab.GetComponent<Ball>();
			if (this.team == GameController.selected_ball_team && this.team != another_ball.team) {
            	if (another_ball_prefab.transform.localScale.x >= 0.8) {
                	another_ball_prefab.transform.localScale = new Vector3(
                    	another_ball_prefab.transform.localScale.x * 0.9f,
                        another_ball_prefab.transform.localScale.y * 0.9f,
                        another_ball_prefab.transform.localScale.z * 0.9f
                    );
                    Debug.Log(another_ball_prefab.transform.localScale.x);
                }
                if (another_ball.hp > this.attack) {
				    another_ball.hp = another_ball.hp - this.attack;
                }
                else {
                    another_ball.hp = 0;
                }
				another_ball.update_gauge();
			}
		}
	}
	void update_gauge() {
		double rate = (double)this.hp/this.hp_max;
		this.gauge_prefab.transform.localScale  =  new Vector3(3.5f * (float)rate,0.1f,0.1f);
		if (rate <= 0.5f) {
			this.gauge_prefab.renderer.material.color = Color.yellow;
		}
	}
}
