﻿using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
	//public GUIStyleState styleState;
	private GUIStyle style;
	public GUISkin test;
	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.fontSize = 64;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		//this time only shows clear.
		//TODO : distinct clear and fail
		GUI.skin = test;
		GUI.backgroundColor = Color.yellow;
		Rect rect = new Rect(10,230, 400, 200);
		style.normal.textColor = Color.white;
		if (Config.clear_flag) {
			GUI.Label(rect, " Congratulation!! YOU CLEARED!!", style);
		} else {
			GUI.Label(rect, " Try Again!! YOU FAILED!!", style);
		}
		if(GUI.Button(new Rect(700,400,400,150),"Back to the main !!")) {
			//Go to the 1st STG
			//Config.clear_flag = false;
			Application.LoadLevel("start_scene");
			Debug.Log("bottun pushed");
		}
	}
}