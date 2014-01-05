using UnityEngine;
using System.Collections;

/*-----------------------------
BlockControl.cs
詰まれているブロック全体を制御するクラス
------------------------------*/
public class BlockControl {
    //GameController
    public GameController game_controller = null;
    //Block
    public GameObject BlockPrefab = null;
    //NOTE 暫定的にブロックの個数は固定
    public static int block_num_wide = 6;
    public static int block_num_height = 20;
    //具体的にblockインスタンスを保持する配列
    public Block[,] blocks;
    //ブロックに関する情報
    public static float interval = 0.6f;
    public static float x_offset = -4.2f;
    public static float y_offset = 0;
    public static float z_offset = -2.5f;
    public ConnectChecker connect_checker = null; //連鎖チェッカー
    public BlockFeeder block_feeder = null; //次に出てくるブロックの色
    public bool[] is_color_enable = null;
    //コンストラクタ
    public void create() {
        this.blocks = new Block[block_num_wide, block_num_height];
        for (int y = 0; y < block_num_height; y++) {
            for (int x = 0; x < block_num_wide; x++) {
                //1. blocksにBlockを格納する
                GameObject game_object = 
                    //origではこの時点で座標を指定していない気がする
                    GameObject.Instantiate(this.BlockPrefab, 
                        new Vector3(
                            x_offset + interval + x,
                            y_offset + interval + y,
                            z_offset - interval
                        ),
                        Quaternion.identity
                    ) as GameObject;
                Block block = game_object.GetComponent<Block>();
                block.place.x = x;
                block.place.y = y;
                this.blocks[x,y] = block;
                block.setUnused();
                block.block_control = this;
            }
        }
        this.is_color_enable = new bool[Block.NORMAL_COLOR_NUM];
        for (int i = 0; i< this.is_color_enable.Length;i++) {
            this.is_color_enable[i] = true;
        }
        //pink封印コードはなし
        this.connect_checker = new ConnectChecker();
        this.connect_checker.block_control = this;
        this.connect_checker.blocks = this.blocks;
        this.connect_checker.create();

        this.block_feeder = new BlockFeeder();
        this.block_feeder.control = this;
        this.block_feeder.create();

        this.setColorToAllBlock();
    }
    //全てのブロックの色を選ぶ
    public void setColorToAllBlock() {
        var places = new StaticArray<Block.PlaceIndex>(block_num_wide*block_num_height);
        for (int y = 0; y < block_num_height; y++) {
            for (int x = 0; x < block_num_wide; x++) {
                Block.PlaceIndex place;
                place.x = x;
                place.y = y;
                places.push_back(place);
            }
        }
        //順番シャッフルコード
        for (int i = 0; i < places.size() -1; i++) {
            int j = Random.Range(i+1, places.size());
            places.swap(i,j);
        }
        this.block_feeder.connect_combo_num = 20;
        foreach(Block.PlaceIndex place in places) {
            Block block = this.blocks[place.x,place.y];
            block.setColorType(this.block_feeder.getNextColorStart(place.x, place.y));
            block.setVisible(true);
        }
    }

    public void update() {
        //現在のブロックの状況で連鎖しているかどうかのチェック
        //但し、blockの速度が全て0の場合に限る
        if (blocks_stop()) {
            this.CheckConnect();
        }
    }

    public bool blocks_stop() {
        for (int x = 0;x < BlockControl.block_num_wide; x++) {
            for (int y = 0;y < BlockControl.block_num_height;y++) {
                if (blocks[x,y] != null) {
                    if(blocks[x,y].transform.position.y > 0 && blocks[x,y].rigidbody.velocity.y < -0.1f) {
                        //Debug.Log("block is moving");
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public bool CheckConnect() {
        bool ret = true;
        //if (this.is_connect_check_enable) {
            ret = this.check_connect_sub();
        //}
        return(ret);
    }
    private bool check_connect_sub() {
        int connect_num = 0;
        int[] delete_num = new int[BlockControl.block_num_wide];
		for (int i = 0; i<BlockControl.block_num_wide; i++) {
			delete_num[i] = 0;
		}
        int[,] delete_position = new int[BlockControl.block_num_wide,BlockControl.block_num_height];
        this.connect_checker.clearAll();
        for (int y=0;y<BlockControl.block_num_height;y++) {
            for (int x = 0;x<BlockControl.block_num_wide;x++) {
                //Debug.Log(blocks[x,y]);
                if (this.blocks[x,y]==null || !this.blocks[x,y].isConnectable()) {
                    continue;
                }
                //同じ色が並んでいるブロックの数をチェックする
                int connect_block_num = this.connect_checker.checkConnect(x,y);
                //同じ色の並びが4つの場合は消さない
                if (connect_block_num < 4) {
                    continue;
                }
                connect_num++;
                //つながっているブロックを消す
                for (int i = 0;i<connect_block_num;i++) {
                    Block.PlaceIndex index = this.connect_checker.connect_block[i];
                    this.blocks[index.x,index.y].beginVanishAction();
					for (int j = 0; j<delete_num[index.x]; j++) {
						if (delete_position[index.x, j] < index.y) {
							index.y--;
						}
					}
                    delete_position[index.x,delete_num[index.x]] = index.y;
                    delete_num[index.x]++;
                }
            }
        }
        //Block配列のほうも削除されたものを反映する
        for (int x = 0;x<BlockControl.block_num_wide;x++) {
            for (int num = 0; num < delete_num[x];num++) {
                for (int i = delete_position[x,num]+1;i<BlockControl.block_num_height;i++){
                    if (i ==  BlockControl.block_num_height-1) {
                        blocks[x,i-1] = blocks[x,i];
                        if (blocks[x,i-1]!=null) {
                            blocks[x,i-1].place.y = i-1;
                        }
                        blocks[x,i] = null;
                    } else {
                        blocks[x,i-1] = blocks[x,i];
                        if (blocks[x,i-1]!=null) {
                            blocks[x,i-1].place.y = i-1;
                        }
                    }
                }
            }
            GameController.erasable_blocks_num = connect_num;
        }
		return true;
    }
}
