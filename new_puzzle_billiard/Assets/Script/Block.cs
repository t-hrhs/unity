using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public BlockControl block_control = null;
    //状態
    public enum STEP {
        NONE = -1,
        IDLE = 0,
        FALL,
        VANISH, //消える事が決まっているブロック
        NUM
    };
    public STEP step;
    public STEP next_step = STEP.NONE;
    //ブロックの色
    public enum COLOR_TYPE {
        NONE = -1,
        CYAN = 0,
        YELLOW,
        RED,
        PURPLE,
        GREEN,
        MAGENTA,
        PINK, //通常の意図はここまで
        ORANGE,
        GRAY,
        NUM,
    }
    public static int NORMAL_COLOR_NUM = (int)COLOR_TYPE.GREEN;
    public static COLOR_TYPE NORMAL_COLOR_FIRST = COLOR_TYPE.CYAN;
    public static COLOR_TYPE NORMAL_COLOR_LAST = COLOR_TYPE.PURPLE;
    public COLOR_TYPE color_type = (COLOR_TYPE)0;
    public struct PlaceIndex {
        public int x;
        public int y;
    };
    //blockの配列添字を保持する構造体
    public PlaceIndex place;
    public static Material[] materials;
    public void setColorType(COLOR_TYPE type) {
        this.color_type = type;
        if(0 <= (int)this.color_type && (int)this.color_type < Block.materials.Length) {
            this.renderer.material = Block.materials[(int)this.color_type];
            this.renderer.material.SetFloat("_BlendRate",0.0f);
        }
    }
    public void setVisible(bool is_visible) {
        this.renderer.enabled = is_visible;
    }
    public bool isVisible(){
        return (this.renderer.enabled);
    }
    public static COLOR_TYPE getNextNormalColor(COLOR_TYPE color) {
        int next = (int)color;
        next++;
        if (next > (int)NORMAL_COLOR_LAST) {
            next = (int)NORMAL_COLOR_FIRST;
        }
        return((COLOR_TYPE)next);
    }
    public void setUnused() {
        this.setColorType(Block.COLOR_TYPE.NONE);
        this.setVisible(false);
    }
    //連鎖チャックの対象となるかどうか
    public bool isConnectable() {
        //暫定的に全て"true"で返す
        return true;
    }

    public void beginVanishAction() {
        this.next_step = STEP.VANISH;
    }
	// Use this for initialization
	void Start () {
	    this.setColorType(this.color_type);
	}
	
	// Update is called once per frame
	void Update () {
	    if(this.next_step != STEP.NONE) {
            switch(this.next_step) {
             	case STEP.VANISH:
                    {
                        //Debug.Log("start to banish");
                        //特になにもしない
                    }
				break;
				default: {
					// nothing to do
				}
				break;
            }
            this.step = this.next_step;
            this.next_step = STEP.NONE;
        }
        switch(this.step) {
            case STEP.VANISH:
                {
                    //Debug.Log("banishing");
                    this.transform.position = new Vector3(0,-5,0);
                }
			break;
        }
		if (this.rigidbody.velocity.y > 0) {
			this.rigidbody.velocity = Vector3.zero;
		}
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "MyBall") {
			//Color my_ball_color = collision.gameObject.renderer.material.color;
            /*if (my_ball_color == Color.red) {
                this.setColorType(Block.COLOR_TYPE.RED);
            } else if (my_ball_color == Color.yellow) {
                this.setColorType(Block.COLOR_TYPE.YELLOW);
            } else {
                this.setColorType(Block.COLOR_TYPE.CYAN);
            }*/
            this.setColorType((COLOR_TYPE)My_Ball.current_color_id);
            collision.gameObject.rigidbody.velocity = Vector3.zero;
            collision.gameObject.renderer.enabled = false;
            collision.gameObject.transform.position = new Vector3(-1.1f,0.35f,-12.0f);
		}
	}
}
