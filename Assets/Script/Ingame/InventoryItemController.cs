using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임에서 우측에 위치할 리스트 아이템들의 제어를 맡음
/// </summary>
public class InventoryItemController : BaseBehaviour
{

    // 현재 아이템 사용중인지 체크
    public bool isUsingItem {
        get {
            for(int i = 0; i < mLstItem.Length; ++i) {

                // 아이템을 얻은 상태가 아니면 돌아갓
                if (!mLstItem[i].isGet) {
                    continue;
                }

                if (mLstItem[i].isSelected) {
                    return true;
                }
            }
            return false;
        }
    }

    public List<int> mListItemOrderEx {
        get {
            return mListItemOrderIdx;
        }
    }

    private int currentInventoryIndex;

    // 아이템 정보 세팅
    private InventoryItem[] mLstItem;

    // 아이템이 들어온 순서대세팅 될 예정
    private List<int> mListItemOrderIdx = new List<int>();

    private List<ItemData> mListItemData = new List<ItemData>();

    // 얘가 아래로
    private Button mBtnNextInventory;

    private Image mImgNextArrow;

    // 얘가 위로
    private Button mBtnPrevInventory;

    private Image mImgPrevArrow;

    // 현재 몇 번쨰 인지
    private Text mTextState;

    protected override void initVariables() {
        base.initVariables();

        currentInventoryIndex = 0;
        mLstItem = Utils.getChild(trf, "ItemList").GetComponentsInChildren<InventoryItem>();

        mBtnPrevInventory = Utils.getChild<Button>(trf, "BtnItemListUp");
        mImgPrevArrow = Utils.getChild<Image>(mBtnPrevInventory, "ImgArrow");

        mBtnNextInventory = Utils.getChild<Button>(trf, "BtnItemListDown");
        mImgNextArrow = Utils.getChild<Image>(mBtnNextInventory, "ImgArrow");

        mTextState = Utils.getChild<Text>(trf, "ImgInventoryState/TextState");

        for (int i=0;i< mLstItem.Length; ++i) {
            mLstItem[i].initMemberVariables();
            mLstItem[i].initState();
        }
    }

    /// <summary>
    /// 개수만큼 아이템을 만듬
    /// </summary>
    /// <param name="count"></param>
    public void inputItemInfo(ItemDataBundle itemBundle) {

        for(int i = 0; i < itemBundle.itemData.Length; ++i) {
            mListItemData.Add(itemBundle.itemData[i]);
        }

        shiftButtonUpdate();
    }

    public void setItem(int idx) {

        for (int i = 0; i < mListItemOrderIdx.Count; ++i) {
            if(mListItemOrderIdx[i] == idx) {
                Log.error(string.Format("{0}, Item = {1}", "해당 아이템은 추가되어 있습니다.", idx , idx));
                return;
            }
        }

        ItemData _Data = null;

        for(int i = 0; i < mListItemData.Count; ++i) {
            if(mListItemData[i].itemIdx == idx) {
                _Data = mListItemData[i];
                break;
            }
        }

        if(_Data == null) {
            Log.error(string.Format("정상적인 아이템 데이터가 아닙니다."));
            return;
        }

        mListItemOrderIdx.Add(idx);

        sortItem();
        shiftButtonUpdate();
    }

    public void removeItem(int idx) {

        mListItemOrderIdx.Remove(idx);

        for(int i = 0; i < mLstItem.Length; ++i) {
            if (!mLstItem[i].isGet) {
                continue;
            }

            if(mLstItem[i].itemIdx == idx) {
                mLstItem[i].initState();
                mLstItem[i].isGet = false;
                break;
            }
        }

        if(mListItemOrderIdx.Count / mLstItem.Length < currentInventoryIndex) {
            currentInventoryIndex--;
            sortItem();
        }
    }

    private ItemData getItemDataByIndex(int idx) {
        for(int i = 0; i < mListItemData.Count; ++i) {
            if(mListItemData[i].itemIdx == idx) {
                return mListItemData[i];
            }
        }

        return null;
    }

    private void sortItem() {

        for(int i = 0; i < mLstItem.Length; ++i) {
            mLstItem[i].initState();
        }

        int inventoryCount = currentInventoryIndex * mLstItem.Length;

        // 시작 인덱스는 현재 인벤토리 인덱스 * 개수 (0 혹은 4 혹은 8..)
        int startIndex = (inventoryCount);

        // 총 개수에서 인덱스 * 개수 가 4보다 작으면 작은 그 값 그대로 사용, 높으면 아이템 개수대로 고정
        int setItemCount = mListItemOrderIdx.Count - inventoryCount < 4 ?
                            mListItemOrderIdx.Count - inventoryCount :
                            mLstItem.Length;

        for(int i = 0; i < setItemCount; ++i) {
            mLstItem[i].setItemInfo(getItemDataByIndex(mListItemOrderIdx[i + inventoryCount]),clickItem);
        }
    }

    private void enableShiftButton(Button btn,Image arrow, bool isActive) {
        
        btn.interactable = isActive;
        
        if (isActive) {
            btn.image.color = Color.white;
            arrow.color = Color.white;
        } else {
            btn.image.color = Color.gray;
            arrow.color = Color.gray;
        }
    }

    private void shiftButtonUpdate() {

        // 위로 올라가는 버튼 비활성화
        if (currentInventoryIndex == 0) {
            enableShiftButton(mBtnPrevInventory, mImgPrevArrow, false);
        }
        // 그 외엔 돌아갈수 있어야함
        else{
            enableShiftButton(mBtnPrevInventory, mImgPrevArrow, true);
        }

        // 현재 인벤토리 인덱스가 최대로 차있거나, 현재 얻은 아이템의 개수가 4개 이하면
        // 아래로 올라가는 버튼 비활성화
        if (currentInventoryIndex == mListItemOrderIdx.Count / mLstItem.Length
            || mListItemOrderIdx.Count <= mLstItem.Length) {
            enableShiftButton(mBtnNextInventory, mImgNextArrow, false);
        // 그 외에는 모두 활성화
        } else { 
            enableShiftButton(mBtnNextInventory, mImgNextArrow, true);
        }

        mTextState.text = string.Format("{0}/{1}", currentInventoryIndex + 1, mLstItem.Length);
    }

    public void shiftItem(bool isNext) {

        if (isNext) {
            // 현재 인덱스가 나타내야 할 아이템 개수보다 적다면
            if (mListItemOrderIdx.Count / mLstItem.Length > currentInventoryIndex) {
                currentInventoryIndex++;
            }
        } else {

            if(currentInventoryIndex > 0) {
                currentInventoryIndex--;
            }
        }

        shiftButtonUpdate();
        sortItem();
    }

    /// <summary>
    /// 아이템을 모두 가림
    /// </summary>
    public void hideItem() {
        for(int i = 0; i < mLstItem.Length; ++i) {
            mLstItem[i].hide();
        }
    }

    /// <summary>
    /// 아이템 해제
    /// </summary>
    public void offItemSelect()
    {
        for (int i = 0; i < mLstItem.Length; ++i)
        {
            mLstItem[i].offItemSelect();
        }
    }

    /// <summary>
    /// 아이템 클릭시 이벤트 처리
    /// </summary>
    /// <param name="item"></param>
    private void clickItem(InventoryItem item,bool isSelect) {

        for(int i = 0; i < mLstItem.Length; ++i) {
            if(mLstItem[i] != item) {
                mLstItem[i].offItemSelect();
            }
        }

        // 선행조건 인덱스
        int preIdx = 0;

        for(int i = item.mPrerequisites.Length -1; i > 0; --i) {
            if(PrerequisitesManager.inst.isSatisfyPre(item.mPrerequisites[i])) {
                preIdx = i;
                break;
            }
        }

        item.curMeetPrereIdx = preIdx;
        for(int i = 0; i < item.mAddCondition[preIdx].Length; ++i) {
            PrerequisitesManager.inst.enablePrerequisites(item.mAddCondition[preIdx][i], isSelect);
        }
    }

    /// <summary>
    /// 아이템 번호로 인벤토리 아이템의 정보를 가져온다
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public InventoryItem getInventoryItemInfo(int idx) {

        for(int i=0;i< mLstItem.Length; ++i) {
            if(mLstItem[i].itemIdx == idx) {
                return mLstItem[i];
            }
        }

        return null;
    }
}
