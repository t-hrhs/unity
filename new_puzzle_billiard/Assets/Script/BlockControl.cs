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
    public static int block_num_height = 6;
    //具体的にblockインスタンスを保持する配列
    public Block[,] blocks;
    //ブロックに関する情報
    public static float interval = 0.6f;
    public static float x_offset = -4.2f;
    public static float y_offset = 0;
    public static float z_offset = -2.5f;
    public ConnectChecker connect_checker = null; //連鎖チェッカー
    public BlockFeeder block_feeder = null; //次に出てくるブロックの色
    //コンストラクタ
    public void create() {
        this.blocks = new Block[block_num_wide, block_num_height];
        for (int y = 0; y < block_num_height; y++) {
            for (int x = 0; x < block_num_wide; x++) {
                //1. blocksにBlockを格納する
                GameObject game_object = 
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
                block.block_control = this;
            }
        }
        this.connect_checker = new ConnectChecker();
        this.connect_checker.block_control = this;
        this.connect_checker.blocks = this.blocks;
        //this.connect_checker.create();

        this.block_feeder = new BlockFeeder();
        //this.block_feeder.control = this;

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
    }
}
