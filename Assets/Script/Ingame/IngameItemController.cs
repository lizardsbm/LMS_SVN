using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임에서 조합창을 제외한 추리 화면에서 쓰이는 아이템들의 제어 역할을 함
/// </summary>
public class IngameItemController : BaseBehaviour
{
    /// <summary>
    /// 현재 활성화 되어있는 아이템의 인덱스를 반환해줌
    /// </summary>
    public List<int> getEnableItems {
        get {
            List<int> enableItems = new List<int>();
            for(int i = 0; i < mLstItem.Count; ++i) {
                if (mLstItem[i].isEnable) {
                    enableItems.Add(mLstItem[i].itemIdx);
                }
            }

            return enableItems;
        }
    }

    public List<IngameItem> mLstItem = new List<IngameItem>();

    private readonly System.Action<IngameItem> mCbClickItem;

    protected override void initVariables() {
        base.initVariables();

        int ingameIndex = 0;

        while (true) {
            IngameItem item = Utils.getChild<IngameItem>(trf, string.Format("Item{0}", ingameIndex + 1));
            
            if (item == null)
                break;

            mLstItem.Add(item);

            if (item.isActive) {
                item.isEnable = true;
            }

            ingameIndex++;
        }
    }

    public void init(System.Action<IngameItem> cb) {

        if (!isInitialized) {
            initVariables();
        }
        
        for (int i = 0; i < mLstItem.Count; ++i) {
            mLstItem[i].init(cb);
        }
    }

    /// <summary>
    /// 외부에서 들어온 데이터를 기준으로 할당해둔 인덱스에 아이템들에 필요한 정보를 세팅해준다.
    /// </summary>
    /// <param name="_data"></param>
    public void initListInfo(IngameItemData[] _data) {
        for(int i = 0; i < mLstItem.Count; ++i) {
            for(int k = 0; k < _data.Length; ++k) {
                if(mLstItem[i].itemIdx == _data[k].inagmeItemIdx) {
                    mLstItem[i].setInfo(_data[k]);
                }
            }
        }
    }

    /// <summary>
    /// 아이템을 눌렀을때 필요한 정보를 넘겨줌
    /// </summary>
    /// <param name="item"></param>
    public void onClickEvent(IngameItem item) {
        mCbClickItem?.Invoke(item);
    }

    /// <summary>
    /// 아이템을 가린다.
    /// </summary>
    /// <param name="idx"></param>
    public void hideItem(int idx) {
        for(int i = 0; i < mLstItem.Count; ++i) {
            if(mLstItem[i].itemIdx == idx) {
                mLstItem[i].disableItem();
            }
        }
    }

    /// <summary>
    /// 아이템을 보이게한다
    /// </summary>
    /// <param name="idx"></param>
    public void showItem(int idx) {
        for(int i = 0; i < mLstItem.Count; ++i) {
            if(mLstItem[i].itemIdx == idx) {
                mLstItem[i].enableItem();
            }
        }
    }

    /// <summary>
    /// 인게임 아이템의 위치를 변경시킨다.
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    public void changeIngameItemPos(int idx, float posX, float posY) {
        for(int i = 0; i < mLstItem.Count; ++i) {
            if(mLstItem[i].itemIdx == idx) {
                mLstItem[i].trf.setLocalX(posX);
                mLstItem[i].trf.setLocalY(posY);
                break;
            }
        }
    }
}
