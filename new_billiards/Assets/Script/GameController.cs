using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	//Ball Instance Array
	public GameObject ball_prefab;
	public GUIText hp_text;
	//team_A's Ball
	public GameObject[] a = new GameObject[4];
	public int[] a_hp = {100,200,300,400};
	public GUIText[] a_hp_text = new GUIText[4];
	//team_B's Information
	public GameObject[] b = new GameObject[4];
	public int[] b_hp = {100,200,300,400};
	public GUIText[] b_hp_text = new GUIText[4];
	//hole's information
	public Vector3[] hall_points = {
		new Vector3(-11.5f,0.5f,5.3f),
		new Vector3(-11.5f,0.5f,-5.3f),
		new Vector3(0.0f,0.5f,5.3f),
		new Vector3(0.0f,0.5f,-5.3f),
		new Vector3(11.5f,0.5f,5.3f),
		new Vector3(11.5f,0.5f,-5.3f),
	};
	
	//my original method
	GameObject make_ball(int x, int z) {
		return Instantiate(ball_prefab, new Vector3(x,0.5f,z),Quaternion.identity) as GameObject;
	}
	
	// Use this for initialization
	void Start () {
		//team_A's instantiation
		for (int i=0;i<4;i++){
			a[i] = this.make_ball(5,i);
			a_hp_text[i] = Instantiate(hp_text, new Vector3(0.20f+0.07f*i,0.15f,0),Quaternion.identity) as GUIText;
			a_hp_text[i].text = a_hp[i].ToString();
			a[i].renderer.material.color = Color.red;
			Ball ballscript = a[i].GetComponent<Ball>();
			ballscript.hp = a_hp[i];
		}
		//team_B's instantiation
		for (int i=0;i<4;i++){
			b[i] = this.make_ball(-5,i);
			b_hp_text[i] = Instantiate(hp_text, new Vector3(0.55f+0.07f*i,0.15f,0),Quaternion.identity) as GUIText;
			b_hp_text[i].text = b_hp[i].ToString();
			b[i].renderer.material.color = Color.blue;
			Ball ballscript = b[i].GetComponent<Ball>();
			ballscript.hp = b_hp[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0;i<4;i++){
			if(is_in_a_hole(a[i])) {
				a[i].transform.position = new Vector3(0,-5,0);
			}
			if(is_in_a_hole(b[i])) {
				b[i].transform.position = new Vector3(0,-5,0);
			}
		}
	}
	bool is_in_a_hole (GameObject ball) {
		for (int i=0;i<6;i++){
			if (Vector3.Distance(ball.transform.position,hall_points[i]) < 1) {
				return true;	
			}
		}
		return false;
	}
}
