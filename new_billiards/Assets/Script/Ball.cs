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
    // ray : shot navigation (test);
    private Ray ray;
    LineRenderer lineRenderer;
	// Use this for initialization
    public int skillType;
	public bool canUseSkill = false;
    public Skill ballSkill;
    private float lineLength = 0;
    private float speed = 300.0f;

    const int SKILL_USABLE_HP_THRESHOLD = 100;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (this.hp < SKILL_USABLE_HP_THRESHOLD) {
            this.canUseSkill = true;
        }

        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        Vector3 first_velocity =  - 5 *(currentPosition -  transform.position);
        first_velocity.y = 0.5f;


        if (selected) {
            if(Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0){
                //Code for action on mouse moving left
                Debug.Log("Mouse moved");
                ray = new Ray(this.transform.position, first_velocity);
                //RaycastHit hit;
                Vector3 hitPosition;
                //if (Physics.Raycast(ray, out hit)) {
                Debug.Log("--------------ray----------------");
                Debug.Log(ray.origin);
                lineLength += Time.deltaTime * speed;
                //GameObject hitObject = hit.transform.gameObject;
                //hitPosition = hitObject.transform.position;
                this.lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.SetWidth(0.1f, 0.1f);
                lineRenderer.SetColors(Color.red, Color.blue);
                lineRenderer.renderer.enabled = true;
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.GetPoint(lineLength));
                Debug.Log(lineRenderer);
            }
        }
		if (this.gameObject == SelectedWithMouse.selectedGameObject && this.team ==  GameController.selected_ball_team) {
			selected = true;
			SelectedWithMouse.selectedGameObject = null;
		}
        else if (this.CanUseSkill() && selected && Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.A) && this.team == GameController.selected_ball_team) {
            Destroy(this.lineRenderer);
			GameController.select_ok = false;
            selected = false;
            // this.SkillUseEffect();
            this.UseSkill(first_velocity);
            this.canUseSkill = false;
        }
		else if (Input.GetMouseButtonUp(0) && selected && this.team ==  GameController.selected_ball_team){
            Destroy(this.lineRenderer);
			GameController.select_ok = false;
			selected = false;
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
                    //Debug.Log(another_ball_prefab.transform.localScale.x);
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
	public void update_gauge() {
		double rate = (double)this.hp/this.hp_max;
		this.gauge_prefab.transform.localScale  =  new Vector3(3.5f * (float)rate,0.1f,0.1f);
		if (rate <= 0.5f) {
			this.gauge_prefab.renderer.material.color = Color.yellow;
		}
	}
    public bool CanUseSkill() {
       return canUseSkill;
    }
    void UseSkill (Vector3 first_velocity) {
        this.ballSkill.BringOut(this, first_velocity);
        //this.SkillUseEffect();
    }
    public void setSkillType(int skillType) {
        this.skillType = skillType;
    }

}
