using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

    public int skillType;
    public GameObject floor;
    private int skillValidTurn = 4;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void BringOut(Ball ball, Vector3 firstVelocity) {
        if (this.skillType == 1) {
            Debug.Log("skill 1");
            float currentMass = ball.rigidbody.mass;
            ball.rigidbody.mass = 1.0f;
            Vector3 specialEffectVelocity = new Vector3(0.1f*firstVelocity.x, 9.0f, 0.1f*firstVelocity.z);
            Debug.Log(specialEffectVelocity);
            ball.rigidbody.velocity = specialEffectVelocity;
            ball.canUseSkill = false;
            ball.rigidbody.mass = currentMass;
        }
        else if (this.skillType == 2) {
            Debug.Log("skill 2");
            ball.collider.material.dynamicFriction = 0.10f;
            ball.collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
            ball.rigidbody.velocity = firstVelocity;
        } 
        else if (this.skillType == 3) {
            Debug.Log("skill 3");
            ball.collider.material.dynamicFriction = 0.80f;
            ball.collider.material.staticFriction  = 0.90f;
            ball.collider.material.bounciness      = 0.95f;
            ball.collider.material.bounceCombine   = PhysicMaterialCombine.Maximum;
            ball.rigidbody.velocity = firstVelocity;
            ball.rigidbody.collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
        }
        else {
            Debug.Log("skill 4");
            float scale_x = ball.transform.localScale.x;
            float scale_y = ball.transform.localScale.y;
            float scale_z = ball.transform.localScale.z;
            Debug.Log(scale_x);
            ball.transform.localScale = new Vector3(scale_x*2, scale_y, scale_z*2);
            Debug.Log(this.floor);
            // this.floor.collider.material.dynamicFriction = 0.80f;
            // this.floor.collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
            ball.rigidbody.velocity = firstVelocity;
        }
    }
}
