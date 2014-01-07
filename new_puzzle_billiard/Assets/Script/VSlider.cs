using UnityEngine;
using System.Collections;

public class VSlider : MonoBehaviour {

    public float vSliderValue = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI() {
        vSliderValue = GUI.VerticalSlider(
            new Rect(25, 25, 200, 100),
            vSliderValue,
            1.0f,
            0.0f
        );
    }
}
