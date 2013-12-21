using UnityEngine;
using System.Collections;

public class ExplainController_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		if(GUI.Button(new Rect(100,250,200,50),"Back to the main !!")) {
			//Go to the 1st STG
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(350,250,200,50), "Go to the stage !!")) {
			//Go to the 1st STG
			Application.LoadLevel("main_game_scene_1");
			Debug.Log("bottun pushed");
		}
	}
}
