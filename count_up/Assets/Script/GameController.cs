using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private const int ball_num = 4; //the number of default ball is FOUR
	public GameObject ball_prefab; //basic of ball;
	public GameObject[] balls = new GameObject[ball_num];
	public static int turn = 15;
	public static int score = 0;
	public Rect position = new Rect (240, 275, 200, 15);

	//make a ball
	void make_a_ball(int index) {
		balls[index] = Instantiate (
			ball_prefab,
			new Vector3((float)1.0f,(float)0.5f,(float)(5.0f-2.0f*index)),
			Quaternion.identity
		) as GameObject;
		Ball ballscript = balls[index].GetComponent<Ball>();
		ballscript.color_id = index;
		ballscript.draw_ball(index);
	}

	// Use this for initialization
	void Start () {
		int i = 0;
		for (i=0;i<ball_num;i++) {
			make_a_ball(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject score_text = GameObject.Find("Score_Text");
		TextMesh tm = (TextMesh)score_text.GetComponent("TextMesh");
		tm.text = "score : " + score;
	}

	void OnGUI () {
		DrawQuad(position, Color.red);
		//DrawRectangle (position,  Color.red);
		//GUI.Label (new Rect (200, 25, 100, 30),"test string");
	}
	void DrawQuad(Rect position, Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}
}
