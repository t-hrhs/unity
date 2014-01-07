using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public float vSliderValue = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 floorPosition = this.transform.position;
        floorPosition.y += 0.2f * Time.deltaTime;
        this.transform.position = floorPosition;
	}
    void OnGUI() {
        vSliderValue = GUI.VerticalSlider(
            new Rect(25, 25, 100, 30),
            vSliderValue,
            10.0f,
            0.0f
        );
    }
}
