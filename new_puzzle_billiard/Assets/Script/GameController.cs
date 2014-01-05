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
                //Debug.Log("FINISH THIS GAME");
                Application.LoadLevel("result_scene");
            }
            user_touchable = true;
        }
    }

    bool does_clear() {
        for (int i = 0; i < BlockControl.block_num_wide; i++) {
            int count = 0;
            int[] points = new int[BlockControl.block_num_wide];
            if (this.block_control.blocks[i,0]== null) {
                continue;
            }
            for (int j=i-1;j>=0;j--) {
                if(this.block_control.blocks[j,0] != null) {
                    points[count] = j;
                    count++;
                } else {
                    break;
                }
            }
            for (int j=i+1;j<BlockControl.block_num_wide; j++) {
                if (this.block_control.blocks[j,0] != null) {
                    points[count] = j;
                    count++;
                } else {
                    break;
                }
            }
            if (count > 2) {
                return false;
            }
            if (count == 2) {
                if (this.block_control.blocks[i,1]!=null) {
                    return false;
                }
                for (int j = 0; j < count;j++) {
                    if (this.block_control.blocks[points[j],1]!=null) {
                        return false;
                    }
                }
            }
            if (count==1) {
                if (this.block_control.blocks[i,1]==null || this.block_control.blocks[points[0],1]== null) {
                    ;
                }
                else if (this.block_control.blocks[i,1].color_type == this.block_control.blocks[points[0],1].color_type) {
                    return false;
                }
                else if (this.block_control.blocks[i,1].color_type == this.block_control.blocks[i,2].color_type) {
                    return false;
                }
                else if (this.block_control.blocks[points[0],1].color_type == this.block_control.blocks[points[0],2].color_type) {
                    return false;
                }
            } if (count == 0) {
                if (this.block_control.blocks[i,1]==null || this.block_control.blocks[i,2]== null || this.block_control.blocks[i,3]== null) {
                    ;
                }
                else if (this.block_control.blocks[i,1].color_type == this.block_control.blocks[i,2].color_type && this.block_control.blocks[i,2].color_type == this.block_control.blocks[i,3].color_type) {
                    return false;
                }
            }
        }
        return true;
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
