using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Config : MonoBehaviour {
	/*
	 *  config variables
	 *  ball_data : (x,y,z,size,hp,attack,skill)
	 *  hole_data : (x,y,z,size)
	 *  skill_data : not written yet
	 */
	static public Dictionary<string, double>[] ball_data = new Dictionary<string, double>[]{
		new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",0.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.14f},{"hp_y",0.12f},{"gauge_x",14.9f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",1.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.23f},{"hp_y",0.12f},{"gauge_x",10.9f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",2.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.32f},{"hp_y",0.12f},{"gauge_x",6.9f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",5.0f},{"y",0.5f},{"z",3.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.41f},{"hp_y",0.12f},{"gauge_x",2.9f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",-5.0f},{"y",0.5f},{"z",0.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.58f},{"hp_y",0.12f},{"gauge_x",-4.8f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",-5.0f},{"y",0.5f},{"z",1.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.67f},{"hp_y",0.12f},{"gauge_x",-8.8f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",-5.0f},{"y",0.5f},{"z",2.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.76f},{"hp_y",0.12f},{"gauge_x",-12.8f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
		new Dictionary<string, double>() {{"x",-5.0f},{"y",0.5f},{"z",3.0f},{"size",1.0f},{"hp",20},{"attack",10},{"hp_x",0.85f},{"hp_y",0.12f},{"gauge_x",-16.8f},{"gauge_y",0.5f},{"gauge_z",12.5f}},
	};
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
