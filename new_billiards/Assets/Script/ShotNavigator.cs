using UnityEngine;
using System.Collections;

public class ShotNavigator : MonoBehaviour {

	// Use this for initialization
    public LineRenderer lineRenderer;
    public Ray ray;
    private GameObject hitObject;
    private RaycastHit hit;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void SetRayFromSelectedBall (Ball ball, Vector3 inputVector) {
        ray = new Ray(ball.transform.position, inputVector);
        if (Physics.Raycast(ray, out hit, 10)) {
            this.hitObject = hit.transform.gameObject;        
            Debug.Log("===== Ray part =====");
            Debug.Log(hitObject.transform.position);
        }
    }
    
    void RenderNavigationLine() {
    }
}
