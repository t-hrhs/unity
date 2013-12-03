using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	const int BALL_NUM = 8;
	//Ball Instance Array
	public GameObject ball_prefab;
	public GameObject gauge_a_prefab;
	public GameObject gauge_b_prefab;
    public GameObject ballSkillPrefab;
    public GameObject shotNavigator;

	public static bool select_ok = true;
	public static string selected_ball_team = "A";
	public GUIText text_a;
	public GUIText text_b;
	public GUIText turn_text_base; 
	private GUIText turn_text;

	public GameObject[] balls = new GameObject[BALL_NUM];

	public static int hp_a;
	public static int hp_b;
	public static int hp_a_max;
	public static int hp_b_max;

    //We have to change this style.
    private int[] skill_types = {1,2,3,4,1,2,3,4};
	//hole's information
	private Vector3[] hall_points = {
		new Vector3(-11.5f,0.5f,5.3f),
		new Vector3(-11.5f,0.5f,-5.3f),
		new Vector3(0.0f,0.5f,5.3f),
		new Vector3(0.0f,0.5f,-5.3f),
		new Vector3(11.5f,0.5f,5.3f),
		new Vector3(11.5f,0.5f,-5.3f),
	};
	public Texture block_wall;
	
	//my original method
	void make_ball(int index) {
		balls[index] = Instantiate(
			ball_prefab,
			new Vector3((float)Config.ball_data[index]["x"],(float)Config.ball_data[index]["y"],(float)Config.ball_data[index]["z"]),
			Quaternion.identity
		) as GameObject;
		Ball ballscript = balls[index].GetComponent<Ball>();
		ballscript.hp_max = (int)Config.ball_data[index]["hp"];
		ballscript.attack = (int)Config.ball_data[index]["attack"];
		if (index < 4){
			hp_a  += (int)Config.ball_data[index]["hp"];
			hp_a_max += (int)Config.ball_data[index]["hp"];
			balls[index].renderer.material.color = Color.red;
			ballscript.team = "A";
		} else {
			hp_b  += (int)Config.ball_data[index]["hp"];
			hp_b_max  += (int)Config.ball_data[index]["hp"];
			balls[index].renderer.material.color = Color.blue;
			ballscript.team = "B";
		}
        //NOTE - t_hrhs : move skill initialization
        ballscript.setSkillType(skill_types[index]);
        GameObject ballSkill = makeBallSkill();
        Skill ballSkillScript = ballSkill.GetComponent<Skill>();
        ballSkillScript.skillType = skill_types[index];
        ballscript.ballSkill = ballSkillScript;
	}

	GameObject makeBallSkill() {
        return Instantiate(this.ballSkillPrefab) as GameObject;
    }

	// Use this for initialization
	void Start () {
		//turn text instantiation
		turn_text = Instantiate(turn_text_base, new Vector3(0.20f,0.38f,0),Quaternion.identity) as GUIText;
		turn_text.text = "A team turn";
		gauge_a_prefab = Instantiate(this.gauge_a_prefab) as GameObject;
		gauge_b_prefab = Instantiate(this.gauge_b_prefab) as GameObject;
		for (int i=0;i<BALL_NUM;i++){
			this.make_ball(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		 update_gauge();
		//TODO:first check whether this turn uses the special skill or not
		if (does_all_ball_stop() && !select_ok) {
			change_turn();	
		}
		for (int i = 0;i < BALL_NUM;i++) {
			if(is_in_a_hole(balls[i])) {
				balls[i].transform.position = new Vector3(0,-5,0);
			}
            // TODO : show ball status whether ball can use skill or not by another way
			/* 
            if(balls[i].GetComponent<Ball>().CanUseSkill()) {
                hp_texts[i].text = "*";
                hp_texts[i].text += (balls[i].GetComponent<Ball>().hp).ToString();
            } else {
                hp_texts[i].text = (balls[i].GetComponent<Ball>().hp).ToString();
            }*/
		}
	}
	bool is_in_a_hole (GameObject ball) {
		for (int i=0;i<6;i++){
			if (Vector3.Distance(ball.transform.position,hall_points[i]) < 1 && ball.transform.localScale.x <= 0.8f) {
				update_gauge();
				return true;	
			}
		}
		return false;
	}
	//check ball status 
	bool does_all_ball_stop () {
		for (int i = 0;i<BALL_NUM;i++) {
			if(balls[i].rigidbody.velocity != Vector3.zero && balls[i].transform.position.y > -1) {
				return false;	
			}
		}
		return true;
	}
	//explode the ball whose hp is zero
	/*void explode_the_ball(){
		for(int i = 0;i<BALL_NUM;i++){
			if (balls[i].GetComponent<Ball>().hp <= 0 && balls[i].transform.position.y > -1){
				balls[i].renderer.material.color = Color.grey;
				balls[i].rigidbody.velocity = new Vector3(0,40,10);
			}
		}
	}*/
	//change the turn
	void change_turn() {
		if (selected_ball_team == "A") {
			selected_ball_team = "B";
			turn_text.text = "B team turn";
		} else {
			selected_ball_team = "A";
			turn_text.text = "A team turn";
		}
		//explode the ball whose hp is zero.
		//explode_the_ball();
		select_ok = true;
	}

	public void update_gauge() {
		double rate = (double)hp_a/hp_a_max;
		this.gauge_a_prefab.transform.localScale  =  new Vector3 (0.1f,0.1f,14.5f * (float)rate);
		if (rate <= 0.5f) {
			this.gauge_a_prefab.renderer.material.color = Color.yellow;
		}
		rate = (double)hp_b/hp_b_max;
		this.gauge_b_prefab.transform.localScale  =  new Vector3 (0.1f,0.1f,14.5f * (float)rate);
		if (rate <= 0.5f) {
			this.gauge_b_prefab.renderer.material.color = Color.yellow;
		}
	}
}
