using UnityEngine;
using System.Collections;

public class My_Ball : MonoBehaviour {
	private Vector3 screenPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//decide the first velocity
        /*
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
		Vector3 first_velocity =  - 5 *(currentPosition -  transform.position);
		first_velocity.y = 0.0f;
		if (Input.GetMouseButtonUp(0)) {
			this.rigidbody.velocity = first_velocity;
			GameController.user_touchable = false;
			GameController.turn--;
		}
        */
	}
   
    public void Shot(float shotVelocity, float shotAngle) {
        //float shotAngleRadian = shotAngle * Mathf.Deg2Rad;
		Vector3 firstVelocityUnit = ShotButton.lineDirection.normalized;
        //Vector3 firstVelocityUnit = new Vector3(Mathf.Cos(shotAngleRadian), 0.0f, Mathf.Sin(shotAngleRadian));
        this.rigidbody.velocity = shotVelocity * firstVelocityUnit;
        GameController.user_touchable = false;
        GameController.turn--;
    }
}
