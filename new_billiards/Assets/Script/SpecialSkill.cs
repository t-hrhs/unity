using UnityEngine;
using System.Collections;

public class SpecialSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void BringOut(Ball ball) {
        Debug.Log("hogehoge");
        float currentMass = ball.rigidbody.mass;
        ball.rigidbody.mass = 100.0f;

        float zVelocity = Random.value * 10;
        Vector3 specialEffectVelocity = new Vector3(30.0f, 1.0f, zVelocity);
        ball.rigidbody.velocity = specialEffectVelocity;
        ball.canUseSpecialSkill = false;
    }
}
