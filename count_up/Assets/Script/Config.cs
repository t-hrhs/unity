using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class  Config {
	//stage_number-1 because of array index
	public static int stage_id = 0;
	public static int[] ball_num = {3};

	//index : stage_id
	public static int[] max_turn = {3,};

	//TODO : too_complicated
	/*public static Dictionary<int, Dictionary<string, double>>[] all_info = new Dictionary<int, Dictionary<string, double>>[]{
		new Dictionary<int, Dictionary<string, double>> () {
			{1, new Dictionary<string, double>() {{"x", 5.0f}}},
		},
	};*/

	//first_index : stage_id, second_index : ball_index
	public static Dictionary<string, double>[][] ball_data = new Dictionary<string, double>[][]{
		new Dictionary<string, double>[] {
			new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
			new Dictionary<string, double>() {{"x",3.0f},{"y",0.5f},{"z",0.0f},{"color",2.0f}},
			new Dictionary<string, double>() {{"x",1.0f},{"y",0.5f},{"z",0.0f},{"color",3.0f}},
			new Dictionary<string, double>() {{"x",-1.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
		},
		//{
			//new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
			//new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
			//new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
		//},
	}; 
	//NOTE : DON'T USE THIS FUNCTION
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
