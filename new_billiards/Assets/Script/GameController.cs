using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	//Ball Instance Array
	public GameObject ball_prefab;
	public static bool select_ok = true;
	public static Color selected_ball_color = Color.red;
	public GUIText hp_text;
	//team_A's Ball
	public GameObject[] a = new GameObject[4];
	// private int[] a_hp = {100,200,300,400};

	private int[] a_hp = {10,20,30,40};
	private int[] a_attack = {10,10,10,10};
    private int[] a_skill_type = {1,2,3,4};
	private GUIText[] a_hp_text = new GUIText[4];
	//team_B's Information
	public GameObject[] b = new GameObject[4];
	// private int[] b_hp = {10,200,300,400};
	private int[] b_hp = {10,20,30,40};
	private int[] b_attack = {20,20,20,20};
	private GUIText[] b_hp_text = new GUIText[5];
    private int[] b_skill_type = {1,2,3,4};
    private int turnNumber = 0;
    public GameObject floor;
	//hole's information
	private Vector3[] hall_points = {
		new Vector3(-11.5f,0.5f,5.3f),
		new Vector3(-11.5f,0.5f,-5.3f),
		new Vector3(0.0f,0.5f,5.3f),
		new Vector3(0.0f,0.5f,-5.3f),
		new Vector3(11.5f,0.5f,5.3f),
		new Vector3(11.5f,0.5f,-5.3f),
	};

    // CONSTANTS
	
	//my original method
	GameObject make_ball(int x, double z) {
		return Instantiate(ball_prefab, new Vector3(x,0.5f,(float)z),Quaternion.identity) as GameObject;
	}
	
	// Use this for initialization
	void Start () {
		//team_A's instantiation
		for (int i=0;i<4;i++){
			a[i] = this.make_ball(5,i * 1.5);
			a_hp_text[i] = Instantiate(hp_text, new Vector3(0.20f+0.07f*i,0.15f,0),Quaternion.identity) as GUIText;
			a_hp_text[i].text = a_hp[i].ToString();
			a[i].renderer.material.color = Color.red;
			Ball ballscript = a[i].GetComponent<Ball>();
			ballscript.hp = a_hp[i];
			ballscript.attack = a_attack[i];
            Skill skill = new Skill();
            skill.skillType = a_skill_type[i];
            ballscript.ballSkill = skill;

		}
		//team_B's instantiation
		for (int i=0;i<4;i++){
			b[i] = this.make_ball(-5,i * 1.5);
			b_hp_text[i] = Instantiate(hp_text, new Vector3(0.55f+0.07f*i,0.15f,0),Quaternion.identity) as GUIText;
			b_hp_text[i].text = b_hp[i].ToString();
			b[i].renderer.material.color = Color.blue;
			Ball ballscript = b[i].GetComponent<Ball>();
			ballscript.hp = b_hp[i];
			ballscript.attack = b_attack[i];
            Skill skill = new Skill();
            skill.skillType = b_skill_type[i];
            ballscript.ballSkill = skill;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (does_all_ball_stop() && !select_ok) {
            turnNumber++;
            Debug.Log("turn is ");
            Debug.Log(turnNumber);
			if (selected_ball_color == Color.red) {
				selected_ball_color = Color.blue;
				Debug.Log("blue");
			} else {
				selected_ball_color = Color.red;
				Debug.Log("red");
			}
			//explode the ball whose hp is zero.
			explode_the_ball();
			select_ok = true;	
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
			if (Vector3.Distance(ball.transform.position,hall_points[i]) < 1 && ball.transform.localScale.x <= 0.8f) {
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
			if (a[i].GetComponent<Ball>().hp <= 0){
				a[i].renderer.material.color = Color.grey;
				a[i].rigidbody.velocity = new Vector3(0,40,10);
			}
			if (b[i].GetComponent<Ball>().hp <= 0){
				b[i].renderer.material.color = Color.grey;
				b[i].rigidbody.velocity = new Vector3(0,40,10);
			}
		}
	}
    // tmp function
}
