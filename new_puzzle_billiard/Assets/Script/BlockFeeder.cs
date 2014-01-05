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
    //3つのブロックを作ってよい数
    public int connect_combo_num = 0;

    //次のブロックの色を取得する
    public Block.COLOR_TYPE getNextColorStart(int x,int y) {
#if false
        Block.COLOR_TYPE color_type;
        color_type = (Block.COLOR_TYPE)Random.Range((int)Block.NORMAL_COLOR_FIRST,(int)Block.NORMAL_COLOR_LAST + 1);
        return(color_type);
#else
        Block[,] blocks = this.control.blocks;
        ConnectChecker connect_checker = this.control.connect_checker;
        Block.COLOR_TYPE org_color;
        int sel;
        org_color = blocks[x,y].color_type;
        //[出現する色の候補のリスト]を初期化する
        this.init_candidates();
        //Debug.Log("x,y");
        //Debug.Log(x);
        //Debug.Log(y);
        //各色を置いた時、同じ色が何個並ぶかを調べておく
        for(int i = 0;i<(int)Block.NORMAL_COLOR_NUM;i++) {
            blocks[x,y].setColorType((Block.COLOR_TYPE)i);
            connect_checker.clearAll();
            this.connect_num[i] = connect_checker.checkConnect(x,y);
            //Debug.Log("color_id:connect_num");
            //Debug.Log(i);
            //Debug.Log(connect_num[i]);
        }
        if (this.connect_combo_num > 0) {
            for (int i = candidates.size() -1; i>=0;i--) {
                if (this.connect_num[(int)candidates[i]] >=4){
                    candidates.erase_by_index(i);
                }
            }
            if (candidates.size() == 0) {
                this.init_candidates();
                Debug.Log("give_up!!");
            }
            int max_num = this.get_max_connect_num();
            this.erase_candidate_if_not(max_num);
            sel = Random.Range(0, candidates.size());
            if (this.connect_num[(int)candidates[sel]] >= 3) {
                this.connect_combo_num--;
            }
        } else {
            for (int i = candidates.size() -1; i>=0;i--){
                if (this.connect_num[(int)candidates[i]] > 2){
                    candidates.erase_by_index(i);
                }
            }
            if (candidates.size() == 0) {
                this.init_candidates();
                Debug.Log("give_up");
            }
            int max_num = this.get_max_connect_num();
            this.erase_candidate_if_not(max_num);
            sel = Random.Range(0,candidates.size());
        }
        blocks[x,y].setColorType(org_color);
        return((Block.COLOR_TYPE)candidates[sel]);
#endif
    }
    private void init_candidates() {
        this.candidates.resize(0);
        for (int i = 0;i<this.candidates.capacity();i++){
            /*if (!this.control.is_color_enable[i]) {
                continue;
            }*/
            this.candidates.push_back((Block.COLOR_TYPE)i);
        }
    }
    //limitの中で最大数をとれるように変更した(4未満に制限)
    private int get_max_connect_num() {
        int sel = 0;
        bool is_selected = false;
        for (int i = 0;i<candidates.size();i++) {
            if (this.connect_num[(int)this.candidates[i]] > 3) {
                continue;
            }
            if (!is_selected && this.connect_num[(int)this.candidates[i]] < 4) {
                is_selected = true;
                sel = i;
            }
            else if (this.connect_num[(int)this.candidates[i]] > this.connect_num[(int)this.candidates[sel]] && this.connect_num[(int)this.candidates[i]] < 4) {
                sel = i;
            }
        }
        return(this.connect_num[(int)this.candidates[sel]]);
    }
    private void erase_candidate_if_not(int connect_num) {
        for (int i = candidates.size() -1; i >= 0; i--) {
            if (this.connect_num[(int)candidates[i]] != connect_num) {
                candidates.erase_by_index(i);
            }
        }
    }
    private void erase_candidate_if_upper(int connect_num) {
        for (int i = candidates.size() -1; i >= 0; i--) {
            if (this.connect_num[(int)candidates[i]] >= connect_num) {
                candidates.erase_by_index(i);
            }
        }
    }
    private void erase_color_from_candidates(Block.COLOR_TYPE color) {
        for (int i = candidates.size() -1;i >=0;i--) {
            if (candidates[i] == color) {
                candidates.erase_by_index(i);
            }
        }
    }
}
