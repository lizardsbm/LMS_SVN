using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임에서 발생하는 공용적으로 필요한 이벤트 등을 사용하기 위한 클래스
/// </summary>
public class IngameDataManager : BaseBehaviour
{
    private static IngameDataManager mInst;

    public static IngameDataManager inst {
        get {
            if (mInst == null) {

                GameObject obj = GameObject.Find("IngameDataManager");

                if (obj == null) {
                    obj = new GameObject("IngameDataManager");
                }

                mInst = obj.AddComponent<IngameDataManager>();
            }

            return mInst;
        }
    }

    public void init() {
        obj.name = "IngameDataManager";
    }

    public List<IngameScriptData[]> mIngameScriptData;

    public Dictionary<int, Sprite> mDicItemSprite;
    public Dictionary<int, Sprite> mDicSubPopupSprite;

    // 현재 진행되고 있는 스크립트가
    // 여러줄 나오고 있는것인지.
    public bool isFlowMultyScript;

    public void setIngameScriptData(List<IngameScriptData[]> _data) {
        mIngameScriptData = _data;
    }
    
    public void loadItemSprite(ItemDataList list) {
        mDicItemSprite = new Dictionary<int, Sprite>();

        int itemIdx = 0;
        Sprite itemSpr;

        for (int i = 0; i < list.mLstItemsinfo[GameManager.StageIndex].itemData.Length; ++i)
        {
            itemIdx = list.mLstItemsinfo[GameManager.StageIndex].itemData[i].itemIdx;
            itemSpr = getItemSprite(list.mLstItemsinfo[GameManager.StageIndex].itemData[i].path);

            mDicItemSprite.Add(itemIdx, itemSpr);
        }
    }

    private Sprite getItemSprite(string path) {
        string itemPath = ResPath.COMBINE_ITEM_ICON + string.Format("{0}{1}/", "Stage", GameManager.StageIndex) + path;
        Sprite sprite = Resources.Load<Sprite>(itemPath);

        // 만약 이미지가 없는 경우 기본적으로 나타내줄 이미지가 있으면 좋을듯
        if (sprite == null) {

        }

        return sprite;
    }

    /// <summary>
    /// 서브팝업 나타내는데 필요한 데이터를 로드함
    /// </summary>
    /// <param name="list"></param>
    public void loadSubPopupSprite(IngameSubPopupDataList list)
    {
        mDicSubPopupSprite = new Dictionary<int, Sprite>();

        int subPopupIndex = 0;
        Sprite subPopuSpr;

        for (int i = 0; i < list.mLstSubPopupinfo[GameManager.StageIndex].IngameSubPopupData.Length; ++i)
        {
            subPopupIndex = list.mLstSubPopupinfo[GameManager.StageIndex].IngameSubPopupData[i].index;
            subPopuSpr = getSubPopupSprite(list.mLstSubPopupinfo[GameManager.StageIndex].IngameSubPopupData[i].path);

                if(subPopuSpr == null) {
                    Debug.Log("정상적인 서브 팝업 데이터가 아닙니다.");
                    Debug.Log("서브 팝업 데이터 이름 = " + list.mLstSubPopupinfo[GameManager.StageIndex].IngameSubPopupData[i].path);
                }

            mDicSubPopupSprite.Add(subPopupIndex, subPopuSpr);
        }
    }

    /// <summary>
    /// 서브팝업 이미지를 가져옴
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Sprite getSubPopupSprite(string path)
    {
        string subPopupPath = ResPath.INGAME_SUBPOPUP + string.Format("{0}{1}/", "Stage", GameManager.StageIndex) + path;
        Sprite sprite = Resources.Load<Sprite>(subPopupPath);

        // 만약 이미지가 없는 경우 기본적으로 나타내줄 이미지가 있으면 좋을듯
        if (sprite == null)
        {

        }

        return sprite;
    }


    /// <summary>
    /// 스크립트 넘버에 해당하는 스크립트 인덱스를 반환함
    /// </summary>
    /// <param name="scriptNumber"></param>
    /// <returns></returns>
    public int getScriptNumberIdx(int scriptNumber) {
        for(int i = 0; i < mIngameScriptData.Count; ++i) {
            for(int k = 0; k < mIngameScriptData[i].Length; ++k) {
                if(mIngameScriptData[i][k].scriptIdx == scriptNumber) {
                    return i;
                }
            }
        }

        Log.e("정상적인 스크립트 넘버가 존재하지 않습니다");
        return -1;
    }
    
    /// <summary>
    /// 분해 실패 스크립트 인덱스를 반환
    /// </summary>
    /// <returns></returns>
    public int getFailDevideScript() {
        // 해당 스테이지에 가장 마지막 스크립트 인덱스를 분해 실패 인덱스로 가정하는경우
        return mIngameScriptData.Count - 1;
    }

    /// <summary>
    /// 합성 실패 스크립트 인덱스를 반환
    /// </summary>
    /// <returns></returns>
    public int getFailCombineItemScript() {
        // 해당 스테이지에 가장 마지막 전의 스크립트 인덱스를 분해 실패 인덱스로 가정하는경우
        return mIngameScriptData.Count - 2;
    }

    /// <summary>
    /// 아이템 사용 실패했을때의 스크립트 인덱스를 반환
    /// </summary>
    /// <returns></returns>
    public int getFailUseItemScript() {
        return mIngameScriptData[mIngameScriptData.Count - 3][0].scriptIdx;
    }
}
