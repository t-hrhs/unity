﻿using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
    private VSlider vslider; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        vslider = GameObject.Find("VSlider").GetComponent<VSlider>(); 
        float height = vslider.vSliderValue;
        Vector3 floorPosition = this.transform.position;
        floorPosition.y = height * 5.0f;
        this.transform.position = floorPosition;
	}
}