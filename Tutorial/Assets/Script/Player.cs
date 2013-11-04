using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool is_landing = false;
	// Use this for initialization
	void Start () {
		this.is_landing = false;
	}
	protected float jump_speed = 5.0f;
	// Update is called once per frame
	void Update () {
		if (this.is_landing) {
			if (Input.GetMouseButtonDown(0))  {
				this.is_landing = false;
				this.rigidbody.velocity = Vector3.up * this.jump_speed;	
			}
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		this.is_landing = true;	
	}
}
