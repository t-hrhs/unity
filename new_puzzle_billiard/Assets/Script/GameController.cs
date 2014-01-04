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
    public Material[] block_materials;
    //現在の消す事が可能なブロックの数を保持
    public static int erasable_blocks_num = 0;
    public static int erasable_blocks_num_before = 0;
    // Use this for initialization
    void Start () {
        //パズルを管理するオブジェクトの生成
        Block.materials = this.block_materials;
        this.block_control = new BlockControl();
        this.block_control.BlockPrefab = this.BlockPrefab;
        this.block_control.game_controller = this;
        this.block_control.create();
        erasable_blocks_num = 0;
    }
    
    // Update is called once per frame
    void Update () {
        this.block_control.update();
        //ブロックが既に消し終えている事を確認する
        if (erasable_blocks_num == 0 && !user_touchable && does_ball_stop()) {
            //ブロックを当ててもこれ以上消せる見込みがないかのチェック
            if (does_clear()) {
                Application.LoadLevel("result_scene");
            }
            user_touchable = true;
        }
    }

    bool does_clear() {
        return false;
    }

    bool does_ball_stop() {
        GameObject my_ball = GameObject.Find("My_Ball");
        if (my_ball.rigidbody.velocity.sqrMagnitude <= 0.01f) {
            return true;
        }
        return false;
    }

    void OnGUI () {
        //とりあえず、今はここに何も書かない
    }
}
