﻿using UnityEngine;
using System.Collections;

public class ShotButton : MonoBehaviour {

    public float shotAngle = 0;
    public float shotVelocity = 0;
    public GameObject myBall;
    public My_Ball myBallScript;
    public LineRenderer lineRenderer;
    public const float LINE_LENGTH = 10.0f;

	// Use this for initialization
	void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);

        myBall = GameObject.Find("My_Ball");
        myBallScript = myBall.GetComponent<My_Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        // get Value of slider
        if (GameController.user_touchable == true) {
            float shotAngleRadian = shotAngle * Mathf.Deg2Rad;
            Vector3 lineDirection = new Vector3(Mathf.Cos(shotAngleRadian), myBall.transform.position.y, Mathf.Sin(shotAngleRadian));

            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, myBall.transform.position);
            lineRenderer.SetPosition(1, myBall.transform.position + LINE_LENGTH * lineDirection);
        } else {
            lineRenderer.SetVertexCount(0);
        }
	}
    
    void OnGUI () {
        //slider
        //shotAngle = GUI.HorizontalSlider(new Rect(50, 350, 360, 20), shotAngle, 0, 360);
		shotAngle = GUI.HorizontalSlider(new Rect(90, 280, 360, 20), shotAngle, 0, 360);
        shotVelocity = GUI.HorizontalSlider(new Rect(90, 300, 360, 20), shotVelocity, 0, 100); 
        // display
        GUI.Label(new Rect(460, 280, 100, 20), ((int)shotAngle).ToString());
        GUI.Label(new Rect(460, 300, 100, 20), ((int)shotVelocity).ToString());

        if (GUI.Button(new Rect(500, 280, 100, 30), "shot")) {
            myBallScript.Shot(shotVelocity, shotAngle);
        }
    }
}