using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분기점 인덱스를 가지고 있을 녀석
/// </summary>
public class BifurcationItem : BaseBehaviour
{
    public int stageIdx;

    private System.Action<int> cbSelectStage;

    protected override void initVariables() {
        base.initVariables();

    }

    public void onClickStage(int idx) {
        if(cbSelectStage != null) {
            cbSelectStage(idx);
        }
    }

}
