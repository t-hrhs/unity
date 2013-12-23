using UnityEngine;
using System.Collections;

public class Start_Bottuns : MonoBehaviour {
	//public GUIStyle style;
	public GUISkin style;
	// Use this for initialization
	void Start () {
		//style = new GUIStyle();
		//style.fontSize = 45;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		//style.normal.textColor = Color.white;
		//TODO : move config.cs
		GUI.skin = style;
		if(GUI.Button(new Rect(25,200,200,50),"1st STG")) {
			//Go to the 1st STG
			Config.stage_id = 0;
			Application.LoadLevel("explain_stage_1");
			//Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(230,200,200,50),"2nd STG")) {
			//Go to the 1st STG
			Config.stage_id = 1;
			Application.LoadLevel("explain_stage_2");
			//Debug.Log("bottun pushed");
		}
	}
}
