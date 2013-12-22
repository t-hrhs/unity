using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
	//public GUIStyleState styleState;
	private GUIStyle style;
	
	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.fontSize = 40;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		//this time only shows clear.
		//TODO : distinct clear and fail
		Rect rect = new Rect(5,100, 400, 200);
		style.normal.textColor = Color.white;
		GUI.Label(rect, " Congratulation!! YOU CLEARED!!", style);

		if(GUI.Button(new Rect(100,250,400,50),"Back to the main !!")) {
			//Go to the 1st STG
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
	}
}
