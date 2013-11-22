using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private bool selected = false;
	private Vector3 screenPoint;
	public int hp;
	public int attack;
	public Skill ballSkill;
    public bool canUseSkill;
		
    //constants
    const int SKILL_USABLE_HP_THRESHOLD = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (this.hp < SKILL_USABLE_HP_THRESHOLD) {
            this.canUseSkill = true;
        }
		if (this.gameObject == SelectedWithMouse.selectedGameObject && this.renderer.material.color ==  GameController.selected_ball_color) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
		else if (this.CanUseSkill() && selected && Input.GetMouseButtonUp(0) && this.renderer.material.color ==  GameController.selected_ball_color){
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
			another_ball.hp = another_ball.hp - this.attack;
		}
	}
    
    public bool CanUseSkill() {
       return canUseSkill;
    }
    void UseSkill (Vector3 first_velocity) {
        this.ballSkill.BringOut(this, first_velocity);
        //this.SkillUseEffect();
    }
    // tmp function 
    void SkillUseEffect () {
        Vector3 effectVector = new Vector3(0.0f, 5.0f, 0.0f);
        this.rigidbody.velocity = effectVector;
    }

    
}
