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
        MAGENTA,
        GREEN,
        PINK, //通常の意図はここまで
        ORANGE,
        GRAY,
        NUM,
    }
    public static int NORMAL_COLOR_NUM = (int)COLOR_TYPE.MAGENTA;
    public static COLOR_TYPE NORMAL_COLOR_FIRST = COLOR_TYPE.CYAN;
    public static COLOR_TYPE NORMAL_COLOR_LAST = COLOR_TYPE.RED;
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
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "MyBall") {
			//collision.gameObject.renderer.material.color = this.renderer.material.color;
            this.setColorType(Block.COLOR_TYPE.RED);
			//this.increment_and_draw_ball();
		}
	}
}
