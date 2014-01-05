using UnityEngine;
using System.Collections;

public class ExplainController : MonoBehaviour {
	public GUISkin style;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		GUI.skin = style;
		GUI.backgroundColor = Color.yellow;
		if(GUI.Button(new Rect(15,900,300,150),"Stage一覧へ")) {
			//Go to the Main STG
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(330,900,300,150), "崩す")) {
			//Go to the Nth STG
			string basic_scene_addr = "main_game_scene_";
			basic_scene_addr += (Config.stage_id + 1).ToString();
			Application.LoadLevel(basic_scene_addr);
			Debug.Log("bottun pushed");
		}
	}
}
