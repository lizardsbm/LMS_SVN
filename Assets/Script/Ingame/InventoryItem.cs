using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임에서 조합 창 들어가기 전 우측에 위치할 리스트에 포함된 아이템의 정보를 가지고 있음
/// </summary>
public class InventoryItem : BaseBehaviour
{
    public int itemIdx; // 아이템 인덱스
    public int[] mPrerequisites; // 선행조건
    public int[][] mCombineIdx; // 조합 가능한 인덱스
    public int[][] mAddCondition; // 아이템 사용시 충족시켜줄 선행 조건
    public int[] mCombineScript; // 조합 했을때 나올 스크립트
    public int[] mReturnScript; // 아이템 설명 스크립트
    public int[] mDevideScript; // 분해 했을떄 나올 스크립ㅌ르
    public string path;

    /// <summary>
    /// 이 아이템을 얻었는지.
    /// </summary>
    public bool isGet;

    /// <summary>
    /// 아이템 이미지
    /// </summary>
    private Image mItemImage;

    public Sprite pSprItem {
        get {
            return mItemImage.sprite;
        }
    }

    private Image mImgSelect;

    private GameObject mObjX;
     
    // 선택 되어있는지
    public bool isSelected;

    // 현재 충족중인 선행조건 인덱스
    public int curMeetPrereIdx;

    private System.Action<InventoryItem,bool> mCbItem;

    private Sprite mSprNotSelect;

    private Sprite mSprSelect;


    protected override void initVariables() {
        base.initVariables();

        mImgSelect = Utils.getComponent<Image>(trf);
        mItemImage = Utils.getChild<Image>(trf, "InventoryItem");
        mObjX = Utils.getChild(trf, "BtnX").gameObject;

        mSprNotSelect = Utils.loadRes<Sprite>(string.Format("{0}{1}", ResPath.INGAME_SPRITE, "Img_Item_Bg"));
        mSprSelect = Utils.loadRes<Sprite>(string.Format("{0}{1}", ResPath.INGAME_SPRITE, "Img_Item_Active_Bg"));
    }

    public void initState() {

        if (isSelected) {
            isSelected = false;
            for (int i = 0; i < mAddCondition[curMeetPrereIdx].Length; ++i) {
                PrerequisitesManager.inst.enablePrerequisites(mAddCondition[curMeetPrereIdx][i], false);
            }
        }

        curMeetPrereIdx = 0;

        isGet = false;
        initItemInfo();
        setEmpty();
    }

    /// <summary>
    /// 아이템을 선택하는 경우 호출
    /// </summary>
    public void onClickItem() {
        
        if (!isGet) {
            return;
        }

        isSelected = !isSelected;

        mImgSelect.sprite = isSelected ? mSprSelect : mSprNotSelect;

        Utils.setActive(mObjX, isSelected);

        if (mCbItem != null) {
            mCbItem(this, isSelected);
        }
    }

    /// <summary>
    /// 외부로부터 아이템 선택된것을 취소시킬때 사용
    /// </summary>
    public void offItemSelect() {

        if (isSelected)
        {
            isSelected = false;
            for(int i=0;i< mAddCondition[curMeetPrereIdx].Length; ++i) {
                PrerequisitesManager.inst.enablePrerequisites(mAddCondition[curMeetPrereIdx][i], false);
            }
        }
        curMeetPrereIdx = 0;
        mImgSelect.sprite = mSprNotSelect;
        Utils.setActive(mObjX, false);
    }

    /// <summary>
    /// 아이템 정보 세팅
    /// 이 아이템이 무슨 아이템이고, 이미지 패스는 무엇이고, 조합 가능하다면 어떤 아이템과 조합이 가능하며, 결과로써의 문구는 뭐가 나올지
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="returnIdx"></param>
    /// <param name="itemPath"></param>
    /// <returns></returns>
    public void setItemInfo(ItemData _data, System.Action<InventoryItem,bool> cb) {

        itemIdx = _data.itemIdx;
        mPrerequisites = (int[])_data.prerequisites.Clone();
        mCombineIdx = _data.combineIdx.ToArray();
        mAddCondition = _data.addCondition.ToArray();
        mReturnScript = _data.returnScript.ToArray();
        mCombineScript = _data.combineScript.ToArray();
        mDevideScript = _data.devideScript.ToArray();
        path = _data.path;
        mCbItem = cb;

        isGet = true;
        mItemImage.sprite = getItemSprite(_data.path);
        mItemImage.enabled = true;
    }

    private void initItemInfo() {
        itemIdx = -1;
        mPrerequisites.ArrayInitialize();
        mCombineIdx.ArrayInitialize();
        mAddCondition.ArrayInitialize();
        mReturnScript.ArrayInitialize();
        mCombineScript.ArrayInitialize();
        mDevideScript.ArrayInitialize();
        path = null;
        mItemImage.enabled = false;
    }

    private Sprite getItemSprite(string path) {
        string itemPath = ResPath.COMBINE_ITEM_ICON + string.Format("{0}{1}/", "Stage", GameManager.StageIndex) + path;
        Sprite sprite = Resources.Load<Sprite>(itemPath);

        // 만약 이미지가 없는 경우 기본적으로 나타내줄 이미지가 있으면 좋을듯
        if(sprite == null) {

        }

        return sprite;
    }

    public void setEmpty() {
        mImgSelect.sprite = mSprNotSelect;
        Utils.setActive(mObjX, false);
    }
}
