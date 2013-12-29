using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class  Config {
	//stage_number-1 because of array index
	public static int stage_id = 0;
	public static bool clear_flag = false;
	public static int[] ball_num = {2,3};

	//index : stage_id
	public static int[] max_turn = {3,4};
	public static Dictionary<string, int>[] clear_cond = new Dictionary<string, int>[] {
		new Dictionary<string, int>() {{"cyan",0}, {"yellow",0}, {"red",2}},
		new Dictionary<string, int>() {{"cyan",0}, {"yellow",0}, {"red",1}},
	};

	//first_index : stage_id, second_index : ball_index
	public static Dictionary<string, double>[][] ball_data = new Dictionary<string, double>[][]{
		new Dictionary<string, double>[] {
			new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",2.0f}},
			new Dictionary<string, double>() {{"x",3.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
		},
		new Dictionary<string, double>[] {
			new Dictionary<string, double>() {{"x",3.0f},{"y",0.5f},{"z",-2.0f},{"color",1.0f}},
			new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
			//new Dictionary<string, double>() {{"x",1.0f},{"y",0.5f},{"z",0.0f},{"color",1.0f}},
			new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",4.0f},{"color",1.0f}},
		},
	}; 
	//NOTE : DON'T USE THESE FUNCTION
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
