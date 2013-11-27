using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private bool selected = false;
	private Vector3 screenPoint;
	public int hp;
	public int attack;
	public string team; //temporary a or b
	public bool canUseSkill = false;
    public Skill ballSkill;
	// Use this for initialization
    //constants
    const int SKILL_USABLE_HP_THRESHOLD = 100;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (this.hp < SKILL_USABLE_HP_THRESHOLD) {
            this.CanUseSkill = true;
        }
		if (this.gameObject == SelectedWithMouse.selectedGameObject && this.team ==  GameController.selected_ball_team) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
        else if (this.CanUseSkill() && selected && Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.A) && this.team == GameController.selected_ball_team) {
			GameController.select_ok = false;
            selected = false;
            // this.SkillUseEffect();
			Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
        	Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
			Vector3 first_velocity =  - 5 *(currentPosition -  transform.position);
			first_velocity.y = 0.5f;
            this.UseSkill(first_velocity);
            this.canUseSkill = false;
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
				another_ball.hp = another_ball.hp - this.attack;
			}
		}
	}
    public bool CanUseSkill() {
       return canUseSkill;
    }
    void UseSkill (Vector3 first_velocity) {
        this.ballSkill.BringOut(this, first_velocity);
        //this.SkillUseEffect();
    }

}
