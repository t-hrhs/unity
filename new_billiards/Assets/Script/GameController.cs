using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	//Ball Instance Array
	public GameObject ball_prefab;
	public GameObject gauge_prefab;
    public GameObject ballSkillPrefab;

	public static bool select_ok = true;
	public static string selected_ball_team = "A";
	public GUIText text_base;
	public GUIText turn_text_base; 
	private GUIText turn_text;
	//team_A's Ball
	public GameObject[] a = new GameObject[4];
	public GameObject[] a_gauge = new GameObject[4];
	private int[] a_hp = {100,200,300,400};
	private int[] a_attack = {10,10,10,10};
    private int[] a_skill_types = {1,2,3,4};
	private GUIText[] a_hp_text = new GUIText[4];
	private GUIText[] a_hp_text_max = new GUIText[4];
	//team_B's Information
	public GameObject[] b = new GameObject[4];
	public GameObject[] b_gauge = new GameObject[4];
	private int[] b_hp = {10,200,300,400};
	private int[] b_attack = {20,20,20,20};
    private int[] b_skill_types = {1,2,3,4};
	private GUIText[] b_hp_text = new GUIText[5];
	private GUIText[] b_hp_text_max = new GUIText[4];
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
	GameObject make_ball(int x, int z) {
		return Instantiate(ball_prefab, new Vector3(x,0.5f,z),Quaternion.identity) as GameObject;
	}
	GameObject makeBallSkill() {
        return Instantiate(this.ballSkillPrefab) as GameObject;
    }

	// Use this for initialization
	void Start () {
		//turn text instantiation
		turn_text = Instantiate(turn_text_base, new Vector3(0.20f,0.38f,0),Quaternion.identity) as GUIText;
		turn_text.text = "A team turn";
		//team_A's instantiation
		for (int i=0;i<4;i++){
			a[i] = this.make_ball(5,i);
			a_hp_text[i] = Instantiate(text_base, new Vector3(0.14f+0.09f*i,0.12f,0),Quaternion.identity) as GUIText;
			a_hp_text_max[i] = Instantiate(text_base, new Vector3(0.17f+0.09f*i,0.12f,0),Quaternion.identity) as GUIText;
			a_gauge[i] = Instantiate(gauge_prefab, new Vector3( 14.9f- 4.0f*i,0.5f,12.5f),Quaternion.identity) as GameObject;
			a_hp_text[i].text = a_hp[i].ToString();
			a_hp_text_max[i].text = " / " + a_hp[i].ToString();
			a[i].renderer.material.color = Color.red;
			//a[i].renderer.material.mainTexture = block_wall;
			Ball ballscript = a[i].GetComponent<Ball>();
			ballscript.hp = a_hp[i];
			ballscript.hp_max = a_hp[i];
			ballscript.attack = a_attack[i];
			ballscript.gauge_prefab = a_gauge[i];
			ballscript.team = "A";
            ballscript.setSkillType(a_skill_types[i]);
            GameObject ballSkill_a = makeBallSkill();
            Skill ballSkillScript = ballSkill_a.GetComponent<Skill>();
            ballSkillScript.skillType = a_skill_types[i];
            ballscript.ballSkill = ballSkillScript;

		}
		//team_B's instantiation
		for (int i=0;i<4;i++){
			b[i] = this.make_ball(-5,i);
			b_hp_text[i] = Instantiate(text_base, new Vector3(0.58f+0.09f*i,0.12f,0),Quaternion.identity) as GUIText;
			b_hp_text_max[i] = Instantiate(text_base, new Vector3(0.61f+0.09f*i,0.12f,0),Quaternion.identity) as GUIText;
			b_gauge[i] = Instantiate(gauge_prefab, new Vector3( -4.8f- 4.0f*i,0.5f,12.5f),Quaternion.identity) as GameObject;
			b_hp_text[i].text = b_hp[i].ToString();
			b_hp_text_max[i].text = " / " + b_hp[i].ToString();
			b[i].renderer.material.color = Color.blue;
			Ball ballscript = b[i].GetComponent<Ball>();
			ballscript.hp = b_hp[i];
			ballscript.hp_max = b_hp[i];
			ballscript.attack = b_attack[i];
			ballscript.gauge_prefab = b_gauge[i];
			ballscript.team = "B";
            ballscript.setSkillType(b_skill_types[i]);
            GameObject ballSkill_a = makeBallSkill();
            Skill ballSkillScript = ballSkill_a.GetComponent<Skill>();
            ballSkillScript.skillType = a_skill_types[i];
            ballscript.ballSkill = ballSkillScript;

		}
	}
	
	// Update is called once per frame
	void Update () {
		//TODO:first check whether this turn uses the special skill or not
		if (does_all_ball_stop() && !select_ok) {
			change_turn();	
		}
		for (int i=0;i<4;i++){
			if(is_in_a_hole(a[i])) {
				a[i].transform.position = new Vector3(0,-5,0);
			}
			if(is_in_a_hole(b[i])) {
				b[i].transform.position = new Vector3(0,-5,0);
			}
            if(a[i].GetComponent<Ball>().CanUseSkill()) {
                a_hp_text[i].text = "*";
                a_hp_text[i].text += (a[i].GetComponent<Ball>().hp).ToString();
            } else {
                a_hp_text[i].text = (a[i].GetComponent<Ball>().hp).ToString();
            }
            if(b[i].GetComponent<Ball>().CanUseSkill()) {
                b_hp_text[i].text = "*";
                b_hp_text[i].text += (b[i].GetComponent<Ball>().hp).ToString();
            } else {
                b_hp_text[i].text = (b[i].GetComponent<Ball>().hp).ToString();
            }
		}
	}
	bool is_in_a_hole (GameObject ball) {
		for (int i=0;i<6;i++){
			if (Vector3.Distance(ball.transform.position,hall_points[i]) < 1) {
				ball.GetComponent<Ball>().hp = 0;
				return true;	
			}
		}
		return false;
	}
	//check ball status 
	bool does_all_ball_stop () {
		for (int i = 0;i<4;i++){
			//magic number exsits
			if(a[i].rigidbody.velocity != Vector3.zero && a[i].transform.position.y > -1) {
				return false;	
			}
		}
		for (int i = 0;i<4;i++){
			//magic number exsits
			if(b[i].rigidbody.velocity != Vector3.zero && b[i].transform.position.y > -1) {
				return false;	
			}
		}
		return true;
	}
	//explode the ball whose hp is zero
	void explode_the_ball(){
		for(int i = 0;i<4;i++){
			if (a[i].GetComponent<Ball>().hp <= 0 && a[i].transform.position.y > -1){
				a[i].renderer.material.color = Color.grey;
				a[i].rigidbody.velocity = new Vector3(0,40,10);
			}
			if (b[i].GetComponent<Ball>().hp <= 0 && b[i].transform.position.y > -1){
				b[i].renderer.material.color = Color.grey;
				b[i].rigidbody.velocity = new Vector3(0,40,10);
			}
		}
	}
	//change the turn
	void change_turn() {
		if (selected_ball_team == "A") {
			selected_ball_team = "B";
			turn_text.text = "B team turn";
			//Debug.Log("B team turn");
		} else {
			selected_ball_team = "A";
			turn_text.text = "A team turn";
			//Debug.Log("A team turn");
		}
		//explode the ball whose hp is zero.
		explode_the_ball();
		select_ok = true;
	}
}
