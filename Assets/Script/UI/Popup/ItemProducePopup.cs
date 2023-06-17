using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 조합 팝업
/// </summary>
public class ItemProducePopup : BaseBehaviour
{
    private Color devideColor = new Color(45.0f / 255.0f, 102.0f / 255.0f, 113.0f / 255.0f);
    private Color combineColor = new Color(103.0f / 255.0f, 32.0f / 255.0f, 40.0f / 255.0f);
    private Color activeColor = new Color(232.0f / 255.0f, 1, 124.0f / 255.0f);

    /// <summary>
    /// 현재 얻은 아이템들을 반환한다.
    /// </summary>
    public List<int> getCurrentItem {
        get {
            List<int> tempGetItem = new List<int>();

            for(int i=0;i< mLstItem.Length; ++i) {
                if (mLstItem[i].isGet) {
                    tempGetItem.Add(mLstItem[i].itemIdx);
                }
            }

            return tempGetItem;
        }
    }

    // 조합창 상태
    public enum PopupState {
        GUIDE,
        COMBINE,
        DEVIDE
    }

    // 팝업 상태(조합이냐 설명보는거냐, 분해하는거냐)
    private PopupState mState;

    private CombineItem[] mLstItem;

    // 현재 조합하려는 아이템
    private List<CombineItem> currentCombineItem = new List<CombineItem>();

    // 아이템 조합 결과에 따른 대사 재생을 나타냄
    private System.Action<List<CombineItem>> mCbItemCombineResult;

    // 아이템 분해에 따른 대사 재생을 나타냄
    private System.Action<IngameScriptData[]> mCbSetNextScript;

    private System.Action<bool> mCbEnableQuickSlot;

    private List<ItemData> mListItemData = new List<ItemData>();

    // 인벤토리 스크립트 패널
    private InventoryScriptPanel mInventoryScriptPanel;

    // 인벤토리 내에서 사용할 토스트
    private InventoryToastPanel mInventoryToastPanel;

    private GameObject objDivideActive;
    private Text mTextDivide;

    private GameObject objCombineActive;
    private Text mTextCombine;

    private GameObject objThumbnail;

    private Image mImgThumbnail;
    private Text mTextThumbnail;

    // 현재 선택중인 아이템
    private CombineItem _curSelectItem;

    protected override void initVariables() {
        base.initVariables();

        mInventoryScriptPanel = Utils.getChild<InventoryScriptPanel>(trf, "sprBg/InventoryScriptPanel");
        mInventoryScriptPanel.initMemberVariables();
        mInventoryScriptPanel.texthide();

        mInventoryToastPanel = Utils.getChild<InventoryToastPanel>(trf, "sprBg/InventoryToastPanel");
        mInventoryToastPanel.initMemberVariables();
        mInventoryToastPanel.hide();

        mLstItem = Utils.getChild(trf, "sprBg/ItemParent").GetComponentsInChildren<CombineItem>();

        for(int i=0;i< mLstItem.Length; ++i) {
            mLstItem[i].initMemberVariables();
            mLstItem[i].setCallback(clickItem);
        }

        objDivideActive = Utils.getChild(trf, "sprBg/btnDivide/ActivateDevide").gameObject;
        mTextDivide = Utils.getChild<Text>(trf, "sprBg/btnDivide/Text");

        objCombineActive = Utils.getChild(trf, "sprBg/btnCombine/ActivateCombine").gameObject;
        mTextCombine = Utils.getChild<Text>(trf, "sprBg/btnCombine/Text");

        objThumbnail = Utils.getChild(trf, "sprBg/BigThumnail").gameObject;
        mImgThumbnail = Utils.getChild<Image>(objThumbnail, "ImgThumnail");
        mTextThumbnail = Utils.getChild<Text>(objThumbnail, "TextItemName");
    }

    /// <summary>
    /// 콜백등록
    /// </summary>
    /// <param name="cb"></param>
    public void initCallback(System.Action<List<CombineItem>> cb, System.Action<IngameScriptData[]> showScriptCb, System.Action<bool> cbQuick) {
        mCbItemCombineResult = cb;
        mCbSetNextScript = showScriptCb;
        mCbEnableQuickSlot = cbQuick;
    }
    

    private void clickItem(CombineItem item) {

        switch(mState) {
            case PopupState.DEVIDE:
                tryDevide(item);
                break;

            case PopupState.COMBINE:

                if(currentCombineItem.Count == 0) {
                    currentCombineItem.Add(item);
                } else {

                    // 같은 아이템을 선택했기 때문에 아무 작동을 하지 않음.
                    if(currentCombineItem[0] == item) {
                        currentCombineItem.Clear();
                        return;
                    }

                    currentCombineItem.Add(item);

                    tryCombineItem();
                }

                break;

            case PopupState.GUIDE:
                setOffItems(item);
                currentCombineItem.Clear();

                if(_curSelectItem == item) {
                    objThumbnail.SetActive(false);
                    mInventoryScriptPanel.texthide();
                    _curSelectItem = null;
                } else {
                    objThumbnail.SetActive(true);
                    setThumbnail(item);
                    showItemScript(item);
                    _curSelectItem = item;
                }

                break;
        }
    }

    private void setThumbnail(CombineItem _item) {
        mImgThumbnail.sprite = _item.curItemImg;
        mTextThumbnail.text = _item.path;
    }

    /// <summary>
    /// 다른 아이템들의 선택 상태를 해제함
    /// </summary>
    private void setOffItems(CombineItem item) {
        for(int i = 0; i < mLstItem.Length; ++i) {
            if(item != mLstItem[i]) {
                mLstItem[i].offSelect();
            }
        }
    }

    /// <summary>
    /// 인벤토리 내에서 스크립트를 보여줄때 사용한다
    /// </summary>
    /// <param name="item"></param>
    private void showItemScript(CombineItem item) {

        int preIdx = 0;

        for(int i = item.mPrerequisites.Length-1; i >0 ; --i) {
            if(PrerequisitesManager.inst.isSatisfyPre(item.mPrerequisites[i])){
                preIdx = i;
                break;
            }
        }

        int scriptIdx = IngameDataManager.inst.getScriptNumberIdx(item.mReturnScript[preIdx]);

        mInventoryScriptPanel.setItemText(IngameDataManager.inst.mIngameScriptData[scriptIdx]);
        mInventoryScriptPanel.show();
    }

    /// <summary>
    /// 분해를 시도한다.
    /// </summary>
    /// <param name="item"></param>
    private void tryDevide(CombineItem item) {

        hide();

        int preIdx = 0;

        for(int i = item.mPrerequisites.Length-1; i > 0; --i) {
            if(PrerequisitesManager.inst.isSatisfyPre(item.mPrerequisites[i])) {
                preIdx = i;
            }
        }

        int scriptIdx = 0;

        int devideIdx = getDivideScript(item, preIdx);

        // 정상적인 분해 인덱스가 없는경우(분해 실패 혹은 에러)
        if(devideIdx == -1) {
            scriptIdx = IngameDataManager.inst.getFailDevideScript();

        } else {
            scriptIdx = IngameDataManager.inst.getScriptNumberIdx(devideIdx);

            if(scriptIdx == -1) {
                scriptIdx = IngameDataManager.inst.getFailDevideScript();
            }
        }

        if(mCbSetNextScript != null)
        {
            mCbSetNextScript(IngameDataManager.inst.mIngameScriptData[scriptIdx]);
        }
    }

    /// <summary>
    /// 선행조건 만족을 통한 아이템 분해에 맞는 스크립트를 반환한다.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="preIdx"></param>
    /// <returns></returns>
    private int getDivideScript(CombineItem item, int preIdx) {

        int devideIdx = -1;

        if (preIdx < 0 || preIdx >= item.mDevideScript.Length) {
            devideIdx = -1;
        } else {
            devideIdx = item.mDevideScript[preIdx];
        }

        return devideIdx;
    }

    /// <summary>
    /// 아이템 조합을 시도한다.
    /// </summary>
    public void tryCombineItem() {
        mCbItemCombineResult(currentCombineItem);
    }

    /// <summary>
    /// 미리 아이템을 만들어 둔다.
    /// </summary>
    /// <param name="itemBundle"></param>
    public void setItemInfo(ItemDataBundle itemBundle) {

        for (int i = 0; i < itemBundle.itemData.Length; ++i) {
            mListItemData.Add(itemBundle.itemData[i]);
        }
    }

    /// <summary>
    /// 팝업 정보 세팅
    /// </summary>
    /// <param name="stateIdx"></param>
    public void setPopupState(int stateIdx) {

        _curSelectItem = null;

        for (int i = 0; i < mLstItem.Length; ++i) {
            mLstItem[i].offSelect();
        }

        mInventoryScriptPanel.texthide();
        objThumbnail.SetActive(false);

        if (mState == (PopupState)stateIdx) {
            mState = PopupState.GUIDE;
        } else {
            mState = (PopupState)stateIdx;
        }

        switch(mState) {
            case PopupState.COMBINE:
                // 조합상태일떄 팝업 상태 변경
                mTextDivide.color = devideColor;
                mTextCombine.color = activeColor;
                break;

            case PopupState.DEVIDE:
                mTextDivide.color = activeColor;
                mTextCombine.color = combineColor;
                // 분해상태일떄 팝업 상태 변경
                break;
            case PopupState.GUIDE:
                mTextDivide.color = devideColor;
                mTextCombine.color = combineColor;
                // 설명 상태일떄 팝업 상태 변경
                break;
        }

        objDivideActive.SetActive(mState == PopupState.DEVIDE);
        objCombineActive.SetActive(mState == PopupState.COMBINE);
    }

    /// <summary>
    /// 현재 켜져있는 아이템 개수를 반환함
    /// </summary>
    /// <returns></returns>
    private int currentShowItem() {
        int showItem = 0;

        for(int i = 0; i < mLstItem.Length; ++i) {
            if(mLstItem[i].isGet) {
                showItem++;
            }
        }

        return showItem;
    }

    private void initPopupState() {
        mInventoryScriptPanel.texthide();
        mInventoryToastPanel.hide();
        currentCombineItem.Clear();

        for(int i = 0; i < mLstItem.Length ; ++i) {
            mLstItem[i].initItemState();
        }

        mState = PopupState.GUIDE;
    }

    public void showCombinePopup(List<int> lstItem) {
        initPopupState();
        setPopupState(0);
        for(int i = 0; i < lstItem.Count; ++i) {
            mLstItem[i].setItemInfo(getItemDatabyIndex(lstItem[i]));
        }
        objThumbnail.SetActive(false);
        show();
    }

    private ItemData getItemDatabyIndex(int idx) {
        for(int i =0;i< mListItemData.Count; ++i) {
            if(mListItemData[i].itemIdx == idx) {
                return mListItemData[i];
            } 
        }

        return null;
    }

    /// <summary>
    /// 얘가 꺼지면 무조건 퀵슬롯이 켜지도록 해야함
    /// </summary>
    public override void hide() {
        if(mCbEnableQuickSlot != null) {
            mCbEnableQuickSlot(true);
        }
        base.hide();
    }
}
