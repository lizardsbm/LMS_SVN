using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineItem : BaseBehaviour
{
    public Sprite curItemImg {
        get {
            return mItemImage.sprite;
        }
    }

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

    private Image mSprBg;
    
    // 선택 되어있는지
    public bool isSelected;

    private System.Action<CombineItem> mCbItem;

    private Sprite mSprNotSelect;

    private Sprite mSprSelect;

    private GameObject mObjX;

    protected override void initVariables() {
        base.initVariables();

        mSprBg = Utils.getChild<Image>(trf, "sprBg");
        mItemImage = Utils.getChild<Image>(mSprBg, "sprItemImage");

        mObjX = Utils.getChild(trf, "BtnX").gameObject;

        mSprNotSelect = Utils.loadRes<Sprite>(string.Format("{0}{1}", ResPath.INGAME_SPRITE, "Img_Item_Bg"));
        mSprSelect = Utils.loadRes<Sprite>(string.Format("{0}{1}", ResPath.INGAME_SPRITE, "Img_Item_Active_Bg"));
    }

    /// <summary>
    /// 아이템을 선택하는 경우 호출
    /// </summary>
    public void onClickItem() {

        if (!isGet)
            return;

        isSelected = !isSelected;

        mSprBg.sprite = isSelected ? mSprSelect : mSprNotSelect;
        
        mObjX.SetActive(isSelected);

        if (mCbItem != null) {
            mCbItem(this);
        }
    }

    public void offSelect() {
        isSelected = false;
        mSprBg.sprite = isSelected ? mSprSelect : mSprNotSelect;
        mObjX.SetActive(isSelected);
    }

    public void initItemState() {
        isSelected = false;
        mSprBg.sprite = isSelected ? mSprSelect : mSprNotSelect;
        mItemImage.enabled = false;
        mObjX.SetActive(isSelected);
        initItemInfo();
    }

    public void setCallback(System.Action<CombineItem> cb) {
        mCbItem = cb;
    }

    /// <summary>
    /// 아이템 정보 세팅
    /// 이 아이템이 무슨 아이템이고, 이미지 패스는 무엇이고, 조합 가능하다면 어떤 아이템과 조합이 가능하며, 결과로써의 문구는 뭐가 나올지
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="returnIdx"></param>
    /// <param name="itemPath"></param>
    /// <returns></returns>
    public void setItemInfo(ItemData _data) {

        itemIdx = _data.itemIdx;
        mPrerequisites = (int[])_data.prerequisites.Clone();
        mCombineIdx = _data.combineIdx.ToArray();
        mAddCondition = _data.addCondition.ToArray();
        mReturnScript = _data.returnScript.ToArray();
        mCombineScript = _data.combineScript.ToArray();
        mDevideScript = _data.devideScript.ToArray();
        path = _data.path;

        mItemImage.sprite = IngameDataManager.inst.mDicItemSprite[itemIdx];
        mItemImage.enabled = true;
        isGet = true;
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
        isGet = false;
    }
}
