using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private int ball_num = 4; //the number of default ball is FOUR
	private GUIStyle style;
	public GameObject ball_prefab; //basic of ball;
	public GameObject[] balls;
	public static int turn =  3;
	public static int score = 0;
	public static bool user_touchable = true;
	public static bool is_clear = false;
	public Rect position = new Rect (240, 275, 200, 15);
	public static int[] clear_num = {0,0,0};

	//make a ball
	void make_a_ball(int index) {
		balls[index] = Instantiate (
			ball_prefab,
			new Vector3(
				(float)(Config.ball_data[Config.stage_id][index]["x"]),
				(float)(Config.ball_data[Config.stage_id][index]["y"]),
				(float)(Config.ball_data[Config.stage_id][index]["z"])
			),
			//new Vector3((float)1.0f,(float)0.5f,(float)(5.0f-2.0f*index)),
			Quaternion.identity
		) as GameObject;
		Ball ballscript = balls[index].GetComponent<Ball>();
		ballscript.color_id =  (int)(Config.ball_data[Config.stage_id][index]["color"]-1);
		Debug.Log(ballscript.color_id);
		ballscript.draw_ball((int)(Config.ball_data[Config.stage_id][index]["color"]-1));
	}

	// Use this for initialization
	void Start () {
		//GUI Initialization
		style = new GUIStyle();
		style.fontSize = 20;
		//other
		turn =  Config.max_turn[Config.stage_id];
		ball_num = Config.ball_num[Config.stage_id];
		balls = new GameObject[ball_num];
		int i = 0;
		for (i=0;i<ball_num;i++) {
			make_a_ball(i);
		}
		// TODO : change the array to hash
		clear_num[0] = Config.clear_cond[Config.stage_id]["cyan"];
		clear_num[1] = Config.clear_cond[Config.stage_id]["yellow"];
		clear_num[2] = Config.clear_cond[Config.stage_id]["red"];
	}
	
	// Update is called once per frame
	void Update () {
		//update the score per function called
		//GameObject score_text = GameObject.Find("Score_Text");
		//TextMesh tm = (TextMesh)score_text.GetComponent("TextMesh");
		//tm.text = "スコア : " + score;
		GameObject turn_text = GameObject.Find("Turn_Text");
		TextMesh tm2 = (TextMesh)turn_text.GetComponent("TextMesh");
		tm2.text = "残りターン数 : " + turn;
		//tm.text = "スコア : " + score;
		//if all the ball stopped, decrement the number of turn and make user touchable
		if (does_all_ball_stopped()) {
			if (turn <= 0 || does_clear()) {
				Application.LoadLevel("result_scene");
			}
			user_touchable = true;
		}
	}
    //ボールが全て止まったかどうかのチェック
    bool does_all_ball_stopped() {
		GameObject my_ball = GameObject.Find("My_Ball");
        if (my_ball.rigidbody.velocity == Vector3.zero) {
            int i;
            for (i=0;i<ball_num;i++) {
                if (balls[i].rigidbody.velocity != Vector3.zero && balls[i].transform.position.y > 0) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

	bool does_clear() {
		int i = 0;
		for (i=0;i<3;i++) {
			if (clear_num[i] > 0) {
				return false;
			}
		}
		Config.clear_flag = true;
		return true;
	}

	void OnGUI () {
		Rect rect_blue = new Rect(700,10, 10, 10);
		style.normal.textColor = Color.white;
		GUI.Label(rect_blue, clear_num[0].ToString (), style);
		Rect rect_yellow = new Rect(830,10, 10, 10);
		GUI.Label(rect_yellow, clear_num[1].ToString(), style);
		Rect rect_red = new Rect(960,10, 10, 10);
		GUI.Label(rect_red, clear_num[2].ToString(), style);
	}
}
