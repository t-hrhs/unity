using UnityEngine;
using System.Collections;

public class ShotButton : MonoBehaviour {
	public GUISkin style;
    public float shotAngle = 0;
    public float shotVelocity = 0;
    public GameObject myBall;
    public My_Ball myBallScript;
    public LineRenderer lineRenderer;
    public const float LINE_LENGTH = 10.0f;
	public static Vector3 lineDirection;

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
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
		//lineDirection = new Vector3(currentPosition.x - myBall.transform.position.x,0, currentPosition.z - myBall.transform.position.z);
		if (GameController.user_touchable == true && currentPosition.z > -3) {
            //float shotAngleRadian = shotAngle * Mathf.Deg2Rad;
            //Vector3 lineDirection = new Vector3(Mathf.Cos(shotAngleRadian), myBall.transform.position.y, Mathf.Sin(shotAngleRadian));
			lineRenderer.SetColors(Color.yellow, Color.yellow);
			lineDirection = new Vector3(currentPosition.x - myBall.transform.position.x,0, currentPosition.z - myBall.transform.position.z);
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, myBall.transform.position);
            lineRenderer.SetPosition(1, myBall.transform.position + LINE_LENGTH * lineDirection);
		} else if (GameController.user_touchable == true) {
			//lineRenderer.SetVertexCount(2);
			lineRenderer.SetVertexCount(2);
			lineRenderer.SetPosition(0, myBall.transform.position);
			lineRenderer.SetPosition(1, myBall.transform.position + LINE_LENGTH * lineDirection);
		} else {
            lineRenderer.SetVertexCount(0);
        }
		//Debug.Log(lineDirection.x);
		//Debug.Log(lineDirection.y);
		//Debug.Log(lineDirection.z);
	}
    
    void OnGUI () {
        //slider
        //shotAngle = GUI.HorizontalSlider(new Rect(50, 350, 360, 20), shotAngle, 0, 360);
		GUI.skin = style;
		//shotAngle = GUI.HorizontalSlider(new Rect(150, 505, 720, 40), shotAngle, 0, 360);
        shotVelocity = GUI.HorizontalSlider(new Rect(150, 505, 720, 60), shotVelocity, 0, 80); 
        // display
        //GUI.Label(new Rect(880, 500, 90, 40), ((int)shotAngle).ToString());
        GUI.Label(new Rect(880, 505, 90, 40), ((int)shotVelocity).ToString());
		GUI.backgroundColor = Color.yellow;
        if (GUI.Button(new Rect(920, 500, 200, 100), "shot") && GameController.user_touchable) {
			GameController.user_touchable = false;
            myBallScript.Shot(shotVelocity, shotAngle);
        }
    }
}
