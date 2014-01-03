using UnityEngine;
using System.Collections;

public class ConnectChecker {
	public BlockControl block_control = null;
	public Block[,] blocks;
    public enum CONNECT_STATUS {
        NONE = -1,
        UNCHECKED = 0,
        CONNECTED,
        NUM,
    };
    public CONNECT_STATUS[,] connect_status = null;
    public Block.PlaceIndex[] connect_block;

    public void create() {
        this.connect_status = new CONNECT_STATUS[BlockControl.block_num_wide,BlockControl.block_num_height];
        this.connect_block = new Block.PlaceIndex[BlockControl.block_num_wide * BlockControl.block_num_height];
    }

    public void clearAll() {
        for (int y = 0;y < BlockControl.block_num_height;y++) {
            for (int x = 0;x < BlockControl.block_num_wide; x++) {
                this.connect_status[x,y] = CONNECT_STATUS.UNCHECKED;
            }
        }
    }
    //(x,y)の位置からつながっているブロックをチェックする。
    public int checkConnect(int x,int y) {
        int connect_num = this.check_connect_recurse(x,y, Block.COLOR_TYPE.NONE,0);
        for (int i = 0;i < connect_num; i++) {
            Block.PlaceIndex index = this.connect_block[i];
            this.connect_status[index.x, index.y] = CONNECT_STATUS.CONNECTED;
        }
        return (connect_num);
    }
    private bool is_error_printed = false;

    private int check_connect_recurse(int x, int y, Block.COLOR_TYPE previous_color, int connect_count) {
        Block.PlaceIndex block_index;
        do {
            if (connect_count >= BlockControl.block_num_wide * BlockControl.block_num_height) {
                if (!this.is_error_printed) {
                    Debug.LogError("Suspicious recursive call");
                    this.is_error_printed = true;
                }
                break;
            }
			if (this.blocks[x,y] == null) {
				break;
			}
            //連結対象じゃない(空中にいるとか、非表示中とか)
            //これは今回は使わないはず
            if (!this.blocks[x,y].isConnectable()) {
                break;
            }
            //既に他のブロックと連結していたらスキップ
            if (this.connect_status[x,y] == CONNECT_STATUS.CONNECTED) {
                break;
            }
            block_index.x = x;
            block_index.y = y;
            //今回既にチェック済ならスキップ
            if(this.is_checked(block_index, connect_count)) {
                break;
            }
            if (previous_color == Block.COLOR_TYPE.NONE) {
                //最初の1個目
                this.connect_block[0] = block_index;
                connect_count = 1;
            } else {
                //2個目以降は、前のブロックと同じ色かチェックする
                if (this.blocks[x,y].color_type == previous_color) {
                    this.connect_block[connect_count] = block_index;
                    connect_count++;
                }
            }
            //同じ色が続いていたら、さらに隣もチェックする
            //ここの端の判定は要改善
            if (previous_color == Block.COLOR_TYPE.NONE || this.blocks[x,y].color_type == previous_color) {
                //左
                if (x > 0) {
                    connect_count = this.check_connect_recurse(x-1, y,this.blocks[x,y].color_type, connect_count);
                }
                //右
                if (x < BlockControl.block_num_wide -1 ) {
                    connect_count = this.check_connect_recurse(x+1,y,this.blocks[x,y].color_type, connect_count);
                }
                //上
                if (y > 0) {
                    connect_count = this.check_connect_recurse(x, y-1,this.blocks[x,y].color_type,connect_count);
                }
                //下
                if (y < BlockControl.block_num_height -1 ) {
                    connect_count = this.check_connect_recurse(x,y+1,this.blocks[x,y].color_type, connect_count);
                }
            }
        } while (false);
        return (connect_count);
    }

    //既にチェック済かどうか確認する
    private bool is_checked(Block.PlaceIndex place, int connect_count){
        bool is_checked = false;
        for (int i = 0;i<connect_count;i++) {
            if (this.connect_block[i].Equals(place)) {
                is_checked = true;
                break;
            }
        }
        return (is_checked);
    }
}
