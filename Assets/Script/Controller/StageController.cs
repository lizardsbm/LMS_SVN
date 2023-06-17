using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지별로 기본적으로 공통적으로 들어가야 하는 부분은 여기에 선언하고, 각 개별로 필요한 부분은 상속받아 구현하도록.
/// </summary>
public class StageController :BaseBehaviour
{
    /// <summary>
    /// 스테이지 상에서 활성화 되어있는 아이템을 반환
    /// </summary>
    public List<int> getEnableItems {
        get {
            return mIngameItemController.getEnableItems;
        }
    }

    // 아이템 클릭 이벤트 콜백
    private System.Action<IngameItem> cbClickEvent;

    // 조합창에 들어가는 아이템이 아닌 아이템의 컨트롤
    private IngameItemController mIngameItemController;

    // 스테이지 진행에 대한 정보를 가지고 이씀
    private IngameItemData[] mStageData;

    protected override void initVariables() {
        base.initVariables();

        mIngameItemController = Utils.getChild<IngameItemController>(trf, "IngameItems");
        mIngameItemController.init(onClickItem);
    }

    public void initPrerequisitesData(PrerequisitesDataBundle _bundle)
    {
        PrerequisitesManager.inst.initPrerequisites(_bundle.preIndex,_bundle.strPre);
    }

    /// <summary>
    /// 콜백등록 및 이것저것 초기화가 필요한경우 사용
    /// </summary>
    /// <param name="cb"></param>
    public void init(System.Action<IngameItem> cb) {
        cbClickEvent = cb;
    }

    /// <summary>
    /// 처음 시작했을떄 UIController와 연결을 하고, 필요한 데이터 정보를 받아 넣는다.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="_data"></param>
    public void initStageState(IngameItemData[] _data) {
        mStageData = _data;
        mIngameItemController.initListInfo(_data);
    }

    /// <summary>
    /// 아이템을 클릭한것에 따라서 이벤트를 전달해줌
    /// </summary>
    /// <param name="item"></param>
    private void onClickItem(IngameItem item) {
        if (cbClickEvent != null) {

            // UI 사용중인 상태로 바꾼다.
            cbClickEvent(item);
        }
    }

    /// <summary>
    /// 아이템을 가린다
    /// </summary>
    /// <param name="tempIngameItem"></param>
    public void hideItem(int idx) {
        mIngameItemController.hideItem(idx);
    }

    public void showItem(int idx) {
        mIngameItemController.showItem(idx);
    }
    
    public void changeIngameItemPos(int idx, float posX, float posY) {
        mIngameItemController.changeIngameItemPos(idx, posX, posY);
    }

    /// <summary>
    /// 게임중... 이하생략
    /// </summary>
    /// <param name="ingames"></param>
    public void setLoadTokenData(int[] ingames) {
        for (int i = 0; i < ingames.Length; ++i) {
            for (int k = 0; k < mIngameItemController.mLstItem.Count; ++k) {
                if(mIngameItemController.mLstItem[k].itemIdx == ingames[i]) {
                    showItem(ingames[i]);
                    break;
                }

                if(k == mIngameItemController.mLstItem.Count - 1) {
                    hideItem(mIngameItemController.mLstItem[k].itemIdx);
                }
            }
        }
    }
}

