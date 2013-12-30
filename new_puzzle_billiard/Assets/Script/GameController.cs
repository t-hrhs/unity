using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GUISkin style;
    public GameObject ball_prefab; //basic of ball;
    public GameObject[] balls;
    public static bool user_touchable = true;
    public static bool is_clear = false;
    public Rect position = new Rect (240, 275, 200, 15);
    public static int[] clear_num = {0,0,0};
    //新しいゲームの為の変数
    public BlockControl block_control = null;
    public GameObject BlockPrefab = null;

    // Use this for initialization
    void Start () {
        //パズルを管理するオブジェクトの生成
        this.block_control = new BlockControl();
        this.block_control.BlockPrefab = this.BlockPrefab;
        this.block_control.game_controller = this;
        this.block_control.create();
    }
    
    // Update is called once per frame
    void Update () {
        if (does_all_ball_stopped()) {
            if (does_clear()) {
                Application.LoadLevel("result_scene");
            }
            user_touchable = true;
        }
    }
    //ボールが全て止まったかどうかのチェック
    bool does_all_ball_stopped() {
        GameObject my_ball = GameObject.Find("My_Ball");
        if (my_ball.rigidbody.velocity == Vector3.zero) {
            return true;
        }
        return false;
    }

    bool does_clear() {
        return false;
    }

    void OnGUI () {
        //とりあえず、今はここに何も書かない
    }
}
