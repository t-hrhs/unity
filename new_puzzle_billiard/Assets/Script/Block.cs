using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public BlockControl block_control = null;
    //状態
    public enum STEP {
        NONE = -1,
        IDLE = 0,
        FALL, //落下中

        NUM
    };
    public STEP step;
    public STEP next_step = STEP.NONE;
    //ブロックの色
    public enum COLOR_TYPE {
        NONE = -1,
        CYAN = 0,
        YELLOW,
        ORANGE,
        MAGENTA,
        GREEN,
        PINK, //通常の意図はここまで
        RED,
        GRAY,
        NUM,
    }
    public static int NORMAL_COLOR_NUM = (int)COLOR_TYPE.RED;
    public struct PlaceIndex {
        public int x;
        public int y;
    };
    //blockの配列添字を保持する構造体
    public PlaceIndex place;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
