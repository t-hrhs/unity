using UnityEngine;
using System.Collections;

public class My_Ball : MonoBehaviour {
	private Vector3 screenPoint;
    public const int NEXT_BALL_NUM = 3;
    //次の3つの色を保持しておく
    public static int[] next_color_ids = new int[NEXT_BALL_NUM];
	// Use this for initialization
	void Start () {
        //ランダムに選んで欲しい場合
        color_choice(-1);
        for (int i = 0; i < NEXT_BALL_NUM;i++) {
            next_color_ids[i] = Random.Range(0,NEXT_BALL_NUM);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.user_touchable) {
            this.renderer.enabled = true;
        }
        Vector3 first_velocity = 20 * LineRenderObj.lineDirection.normalized;
		if (Input.GetMouseButtonUp(0)) {
			this.rigidbody.velocity = first_velocity;
			GameController.user_touchable = false;
			//GameController.turn--;
		}

	}

    private void color_choice(int color_id) {
        if (color_id == -1) {
            color_id = Random.Range(0,NEXT_BALL_NUM);
        }
        if (color_id == 0) {
            this.renderer.material.color = Color.cyan;
        } else if (color_id == 1) {
            this.renderer.material.color = Color.yellow;
        } else {
            this.renderer.material.color = Color.red;
        }
    }
}
