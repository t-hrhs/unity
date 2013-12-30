using UnityEngine;
using System.Collections;

public class BlockFeeder {
    public BlockControl control = null;
    private StaticArray<int> connect_num = null;
    //出現する色の候補
    private StaticArray<Block.COLOR_TYPE> candidates = null;
    
    public void create() {
        this.connect_num = new StaticArray<int>(Block.NORMAL_COLOR_NUM);
        this.connect_num.resize(this.connect_num.capacity());
        this.candidates = new StaticArray<Block.COLOR_TYPE>(Block.NORMAL_COLOR_NUM);
    }
}
