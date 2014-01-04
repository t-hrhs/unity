using UnityEngine;
using System.Collections;

public class LineRenderObj : MonoBehaviour {
	public GUISkin style;
    public float shotAngle = 0;
    public float shotVelocity = 0;
    public GameObject myBall;
    public My_Ball myBallScript;
    public LineRenderer lineRenderer;
    public const float LINE_LENGTH = 10.0f;
	public static Vector3 lineDirection;
    public float z_distance = 0.1f;
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
        Vector3 screenPos = Camera.main.WorldToScreenPoint(myBall.transform.position);
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
		//lineDirection = new Vector3(currentPosition.x - myBall.transform.position.x,0, currentPosition.z - myBall.transform.position.z);
		//if (GameController.user_touchable == true && currentPosition.z > -3) {
		if (GameController.user_touchable == true) {
            //float shotAngleRadian = shotAngle * Mathf.Deg2Rad;
            //Vector3 lineDirection = new Vector3(Mathf.Cos(shotAngleRadian), myBall.transform.position.y, Mathf.Sin(shotAngleRadian));
			lineRenderer.SetColors(Color.yellow, Color.yellow);
			lineDirection = new Vector3(-currentPosition.x + myBall.transform.position.x,0, -currentPosition.z + myBall.transform.position.z);
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, myBall.transform.position);
            lineRenderer.SetPosition(1, myBall.transform.position + LINE_LENGTH * lineDirection.normalized);
		} else {
            lineRenderer.SetVertexCount(0);
        }
	}
}
