using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	const int BALL_NUM = 8;
	//Ball Instance Array
	public GameObject ball_prefab;
	public GameObject gauge_prefab;
    public GameObject ballSkillPrefab;
    public GameObject shotNavigator;

	public static bool select_ok = true;
	public static string selected_ball_team = "A";
	public GUIText text_base;
	public GUIText turn_text_base; 
	private GUIText turn_text;

	public GameObject[] balls = new GameObject[BALL_NUM];
	public GUIText[] hp_texts = new GUIText[BALL_NUM];
	public GUIText[] hp_max_texts = new GUIText[BALL_NUM];
	public GameObject[] gauges = new GameObject[BALL_NUM];

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
		hp_texts[index] = Instantiate(
			text_base,
			new Vector3((float)Config.ball_data[index]["hp_x"],(float)Config.ball_data[index]["hp_y"],0),Quaternion.identity
		) as GUIText;
		hp_max_texts[index] = Instantiate(
			text_base,
			new Vector3((float)Config.ball_data[index]["hp_x"] + 0.03f,(float)Config.ball_data[index]["hp_y"],0),Quaternion.identity
		) as GUIText;
		gauges[index] = Instantiate(
			gauge_prefab,
			new Vector3((float)Config.ball_data[index]["gauge_x"],(float)Config.ball_data[index]["gauge_y"],(float)Config.ball_data[index]["gauge_z"]),Quaternion.identity
		) as GameObject;
		hp_texts[index].text = ((float)Config.ball_data[index]["hp"]).ToString();
		hp_max_texts[index].text = " / " + ((float)Config.ball_data[index]["hp"]).ToString();
		Ball ballscript = balls[index].GetComponent<Ball>();
		ballscript.hp = (int)Config.ball_data[index]["hp"];
		ballscript.hp_max = (int)Config.ball_data[index]["hp"];
		ballscript.attack = (int)Config.ball_data[index]["attack"];
		ballscript.gauge_prefab = gauges[index];
		if (index < 4){
			balls[index].renderer.material.color = Color.red;
			ballscript.team = "A";
		} else {
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
		for (int i=0;i<BALL_NUM;i++){
			this.make_ball(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//TODO:first check whether this turn uses the special skill or not
		if (does_all_ball_stop() && !select_ok) {
			change_turn();	
		}
		for (int i = 0;i < BALL_NUM;i++) {
			if(is_in_a_hole(balls[i])) {
				balls[i].transform.position = new Vector3(0,-5,0);
			}
            // change the status of the balls
            if(balls[i].GetComponent<Ball>().CanUseSkill()) {
                hp_texts[i].text = "*";
                hp_texts[i].text += (balls[i].GetComponent<Ball>().hp).ToString();
            } else {
                hp_texts[i].text = (balls[i].GetComponent<Ball>().hp).ToString();
            }
		}
	}
	bool is_in_a_hole (GameObject ball) {
		for (int i=0;i<6;i++){
			if (Vector3.Distance(ball.transform.position,hall_points[i]) < 1 && ball.transform.localScale.x <= 0.8f) {
				ball.GetComponent<Ball>().hp = 0;
				ball.GetComponent<Ball>().update_gauge();
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
	void explode_the_ball(){
		for(int i = 0;i<BALL_NUM;i++){
			if (balls[i].GetComponent<Ball>().hp <= 0 && balls[i].transform.position.y > -1){
				balls[i].renderer.material.color = Color.grey;
				balls[i].rigidbody.velocity = new Vector3(0,40,10);
			}
		}
	}
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
		explode_the_ball();
		select_ok = true;
	}
}
