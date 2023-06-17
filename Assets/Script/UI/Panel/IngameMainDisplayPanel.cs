using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임에서 메인화면에 계속해서 나와있을 UI를 제어
/// </summary>
public class IngameMainDisplayPanel : BaseBehaviour
{
    private const string MENU = "Menu";

    public List<int> lstGetItems {
        get {
            return mItemController.mListItemOrderEx;
        }
    }

    // 아이템 사용중인지.
    public bool isUsingItem {
        get {
            return mItemController.isUsingItem;
        }
    }

    private System.Action mCbShowCombine;
    
    // 우측 아이템 컨트롤러
    private InventoryItemController mItemController;
    private HintPopup mHintPopup;
    protected override void initVariables() {
        base.initVariables();

        mItemController = Utils.getChild<InventoryItemController>(trf, "ItemInventory");
        mItemController.initMemberVariables();

        mHintPopup = Utils.getChild<HintPopup>(trf, "HintPopup");
    }

    public void addItem(ItemDataBundle itemBundle) {
        mItemController.inputItemInfo(itemBundle);
    }

    public void setItem(int idx) {
        mItemController.setItem(idx);
    }

    public void removeItem(int idx) {
        mItemController.removeItem(idx);
    }

    public void setCb(System.Action cb) {
        mCbShowCombine = cb;
    }

    public void offItemSelect()
    {
        mItemController.offItemSelect();
    }

    public void goMenu()
    {
        GameSettingMgr.inst.mPlayState = GamePlayStates.GiveUp;
        SceneController.inst.startSceneLoad(SceneController.MENU);
    }

    // 조합 버튼 보기
    public void onClickShowCombine() {
        if(mCbShowCombine != null) {
            mCbShowCombine();
        }
    }


    public void onClickBtnHintPopup()
    {
        if (!Utils.isActive(mHintPopup)) mHintPopup.show();
        else mHintPopup.hide();
    }

    /// <summary>
    /// 인벤토리 아이템 정보를 가져옴
    /// </summary>
    /// <param name="idx"></param>
    public InventoryItem getInventoryItemInfo(int idx) {
        return mItemController.getInventoryItemInfo(idx);
    }

    public void enableQuickSlot(bool isEnable) {
        if (isEnable) {
            mItemController.show();
        } else {
            mItemController.hide();
        }
    }
}
