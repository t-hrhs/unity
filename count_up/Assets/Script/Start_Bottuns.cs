using UnityEngine;
using System.Collections;

public class Start_Bottuns : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		//TODO : move config.cs
		if(GUI.Button(new Rect(25,90,100,50),"1st STG")) {
			//Go to the 1st STG
			Config.stage_id = 0;
			Application.LoadLevel("explain_stage_1");
			//Debug.Log("bottun pushed");
		}
		if(GUI.Button(new Rect(130,90,100,50),"2nd STG")) {
			//Go to the 1st STG
			Config.stage_id = 1;
			Application.LoadLevel("explain_stage_2");
			//Debug.Log("bottun pushed");
		}
	}
}
