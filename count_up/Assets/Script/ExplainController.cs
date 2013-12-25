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
		if(GUI.Button(new Rect(100,450,400,150),"Back to the main !!")) {
			//Go to the Main STG
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(650,450,400,150), "Go to the stage !!")) {
			//Go to the Nth STG
			string basic_scene_addr = "main_game_scene_";
			basic_scene_addr += (Config.stage_id + 1).ToString();
			Application.LoadLevel(basic_scene_addr);
			Debug.Log("bottun pushed");
		}
	}
}
