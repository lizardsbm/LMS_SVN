using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI의 루트로 전체적인 UI를 제어할 클래스
/// </summary>
public class IngameUIController : BaseBehaviour
{

    public List<int> getCurrentItem {
        get {
            return mItemProducePopup.getCurrentItem;
        }
    }

    // 아이템 사용중인지.
    public bool isUsingItem {
        get {
            return mIngameMainDisplayPanel.isUsingItem;
        }
    }

    //현재 목표 힌트 팝업
    public HintPopup mHintPopup;

    // 아이템 조합 팝업
    public ItemProducePopup mItemProducePopup;

    // 인게임에서 나와있는 패널(조합 버튼, 설정 버튼, 제한시간 같은게 나오지 않을까 싶음)ㄴ
    public IngameMainDisplayPanel mIngameMainDisplayPanel;

    // 인게임 하단 스크립트 나올 패널
    // 2020.05.27 공통 스크립트 패널에서 변경함
    public IngameScriptPanel mIngameScriptPanel;

    // 게임 클리어시에 나올 팝업
    public GameClearPopup mGameClearPopup;

    // 아이템을 얻는경우 나옴
    public GetItemWidget mGetItemWidget;

    private bool isCombineSuccess;

    protected override void initVariables() {
        base.initVariables();

        mItemProducePopup.initMemberVariables();
        mItemProducePopup.hide();

        mIngameMainDisplayPanel.initMemberVariables();
        mIngameMainDisplayPanel.setCb(showCombineItemPopup);

        mIngameScriptPanel.initMemberVariables();
        mIngameScriptPanel.hide();

        mGameClearPopup.initMemberVariables();
        mGameClearPopup.hide();

        mGetItemWidget.initMemberVariables();
        mGetItemWidget.hide();
    }

    /// <summary>
    /// 인게임에서 실행에 필요한 콜백을 연결한다.
    /// </summary>
    /// <param name="cb"></param>
    public void initCallback(System.Action<string, List<string>> cb) {
        mIngameScriptPanel.setCb(cb);
        mItemProducePopup.initCallback(showCombineScriptText, mIngameScriptPanel.setNextIngameScriptData, mIngameMainDisplayPanel.enableQuickSlot);
    }

    /// <summary>
    /// UI 상으로 필요한 정보를 GameManager 로부터 전달받아 가공하여 나타낸다.
    /// </summary>
    /// <param name="_itemBundle"></param>
    /// <param name="_ingameHintBundle"></param>
    public void init(ItemDataBundle _itemBundle, IngameHintBundle _ingameHintBundle) {
        mIngameMainDisplayPanel.addItem(_itemBundle);
        mItemProducePopup.setItemInfo(_itemBundle);

        //2020-10-12 김하아민
        //Awake 타이밍에 ingameHIntData를 받지 못해 강제로 초기화
        mHintPopup.data = _ingameHintBundle.ingameHintData;
        mHintPopup.initMemberVariables(true);
    }

    /// <summary>
    /// 아이템 조합창 열기
    /// </summary>
    private void showCombineItemPopup() {

        if (mItemProducePopup != null) {
            mItemProducePopup.showCombinePopup(mIngameMainDisplayPanel.lstGetItems);
            mIngameMainDisplayPanel.enableQuickSlot(false);
        }
    }

    /// <summary>
    /// 인덱스에 해
    /// </summary>
    /// <param name="idx"></param>
    private void showCombineScriptText(List<CombineItem> items) {

        mItemProducePopup.hide();

        int scriptIdx = IngameDataManager.inst.getScriptNumberIdx(tryCombine(items));

        // 조합 실패의 경우
        if (scriptIdx == -1) {
            scriptIdx = IngameDataManager.inst.getFailCombineItemScript();
        }

        mIngameScriptPanel.setNextIngameScriptData(IngameDataManager.inst.mIngameScriptData[scriptIdx]);
    }

    private int tryCombine(List<CombineItem> items) {

        int resultIdx = -1;

        bool isCombineFail = false;
        bool isCombineSuccess = false;

        // 기준점이 될 For 인덱스
        int standardReIndex = 0;

        // 기준이 되는 선행조건 인덱스
        int standardPreIndex = 0;
        int standardCombineIndex = 0;

        // 비교 대상이 되는 선행조건 인덱스
        int comparePreIndex = 0;
        int compareCombineIndex = 0;

        CombineItem standardItem = items[0];

        CombineItem compareItem = items[1];

        #region 선행 조건 체크

        standardReIndex = standardItem.mPrerequisites.Length - 1;

        // 조합 실패 판정이 나올때까지 되돌림.
        while (!isCombineFail) {

            if (standardReIndex < 0) {
                isCombineFail = true;
                break;
            }

            if (PrerequisitesManager.inst.isSatisfyPre(standardItem.mPrerequisites[standardReIndex])) {
                standardPreIndex = standardReIndex;
            } else {
                standardReIndex--;
                continue;
            }

            for (int i = compareItem.mPrerequisites.Length -1; i >= 0; --i) {
                if (compareItem.mPrerequisites[i] == standardItem.mPrerequisites[standardPreIndex]) {
                    comparePreIndex = i;
                    isCombineSuccess = true;
                    break;
                }

                // 전부 비교했는데 조합이 실패되면
                if (i == 0 && standardReIndex == 0) {
                    isCombineFail = true;
                }
            }

            if (isCombineSuccess) {
                break;
            }

            standardReIndex--;
        }

        if (isCombineFail) {
            Log.e("조합 실패 혹은 선행조건이 올바르게 입력되지 않음");
            isCombineSuccess = false;
            return -1;
        }

        #endregion

        #region 기준이 되는 아이템 기준으로 어떤 아이템을 조합하는지 체크

        for (int i = 0; i < standardItem.mCombineIdx[standardPreIndex].Length; ++i) {
            if (standardItem.mCombineIdx[standardPreIndex][i] == compareItem.itemIdx) {
                standardCombineIndex = i;
                break;
            }

            if (i == standardItem.mCombineIdx[standardPreIndex].Length - 1) {
                Log.e("기준 아이템과 비교 아이템의 조합 및 조합당하는 아이템 인덱스가 맞지않음");
                isCombineSuccess = false;
                return -1;
            }
        }

        for (int i = 0; i < compareItem.mCombineIdx[comparePreIndex].Length; ++i) {
            if (compareItem.mCombineIdx[comparePreIndex][i] == standardItem.itemIdx) {
                compareCombineIndex = i;
                break;
            }

            if (i == standardItem.mCombineIdx[compareCombineIndex].Length - 1) {
                Log.e("비교 아이템과 기준 아이템의 조합 및 조합당하는 아이템 인덱스가 맞지않음");
                isCombineSuccess = false;
                return -1;
            }
        }

        #endregion

        if (standardItem.mCombineScript[standardPreIndex] ==
           compareItem.mCombineScript[comparePreIndex]) {
            isCombineSuccess = true;
            resultIdx = standardItem.mCombineScript[standardPreIndex];
        } else {
            isCombineSuccess = false;
        }

        return resultIdx;
    }

    public void showGetItemWidget(int idx) {
        InventoryItem getItemInfo = mIngameMainDisplayPanel.getInventoryItemInfo(idx);

        mGetItemWidget.showGetItem(getItemInfo);
    }

    public void disapperGetItemWidget() {
        mGetItemWidget.playReverse();
    }

    public void addItem(int idx) {
        // zero index convert
        mIngameMainDisplayPanel.setItem(idx);
    }

    public void removeItem(int idx) {
        // zero index convert
        mIngameMainDisplayPanel.removeItem(idx);
    }


    /// <summary>
    /// 게임이 도중에 튕긴뒤 로드했을떄 튕긴 시점에서의 아이템정보를 넣어준다.
    /// </summary>
    /// <param name="loadInvenItem"></param>
    public void setLoadTokenData(int[] loadInvenItem) {
        for (int i = 0; i < loadInvenItem.Length; ++i) {
            addItem(loadInvenItem[i]);
        }
    }

    public void offItemSelect() {
        mIngameMainDisplayPanel.offItemSelect();
    }

    /// <summary>
    /// 2020.05.27
    /// 스크립트 패널이 공통에서 인게임 전용으로 쓰이는것으로 쓰임
    /// </summary>
    /// <param name="_info"></param>
    public void setNextIngameScriptData(IngameScriptData[] _info) {
        mIngameScriptPanel.setNextIngameScriptData(_info);
    }

    /// <summary>
    /// 게임을 클리어 하는 경우 게임 클리어 관련 팝업을 띄운다.
    /// </summary>
    public void showGameClearPopup(string text) {
        mGameClearPopup.setClearText(text);
        mGameClearPopup.show();
    }

    public void changeScriptPos(float y) {
        mIngameScriptPanel.changeScriptPos(y);
    }

    public void resetScriptPos() {
        mIngameScriptPanel.resetScriptPos();
    }
}
