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
		if(GUI.Button(new Rect(25,90,100,50),"1st STG")) {
			//Go to the 1st STG
			Application.LoadLevel("MainGame_1");
			Debug.Log("bottun pushed");
		}
	}
}
