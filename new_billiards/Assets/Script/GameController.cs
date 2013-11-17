using UnityEngine;
using System.Collections;

public class  GameController : MonoBehaviour {
	//Ball Instance Array
	public GameObject ball_prefab;
	public GUIText hp_text;
	//team_A's Ball
	public GameObject[] team_A = new GameObject[4];
	public GUIText[] team_A_hps = new GUIText[4];
	//team_B's Ball
	public GameObject[] team_B = new GameObject[4];
	//my original method
	GameObject make_ball(int x, int z) {
		return Instantiate(ball_prefab, new Vector3(x,0.5f,z),Quaternion.identity) as GameObject;
	}
	//GUIText make_hp(double x, double y) {
	//	return Instantiate(hp_text, new Vector3(x,y,0.0f),Quaternion.identity) as GUIText;
	//}
	// Use this for initialization
	void Start () {
		//team_A's instantiation
		for (int i=0;i<4;i++){
			team_A[i] = this.make_ball(5,i);
			//team_A_hps[i] = make_hp(0.15f+0.05f*i,0.15);
			team_A_hps[i] = Instantiate(hp_text, new Vector3(0.15f+0.07f*i,0.15f,0),Quaternion.identity) as GUIText;

			team_A_hps[i].text = (100 * (i+1)).ToString();
			team_A[i].renderer.material.color = Color.red;
			Ball ballscript = team_A[i].GetComponent<Ball>();
			ballscript.hp = 100 * i;
		}
		//team_B's instantiation
		for (int i=0;i<4;i++){
			team_B[i] = this.make_ball(-5,i);
			team_B[i].renderer.material.color = Color.blue;
			Ball ballscript = team_A[i].GetComponent<Ball>();
			ballscript.hp = 100 * i;
		}
		//show hp below
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
