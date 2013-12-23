using UnityEngine;
using System.Collections;

public class ExplainController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		if(GUI.Button(new Rect(100,250,200,50),"Back to the main !!")) {
			//Go to the Main STG
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(350,250,200,50), "Go to the stage !!")) {
			//Go to the Nth STG
			string basic_scene_addr = "main_game_scene_";
			basic_scene_addr += (Config.stage_id + 1).ToString();
			Application.LoadLevel(basic_scene_addr);
			Debug.Log("bottun pushed");
		}
	}
}
