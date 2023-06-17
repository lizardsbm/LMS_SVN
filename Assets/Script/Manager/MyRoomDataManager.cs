using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 마이룸 데이터 정보
/// 실제로 내 방이 어떻게 적용되어있나 를 참고 할 녀석
/// 이녀석은 나중에 필히 손봐야 함 
/// TODOTODO
/// </summary>
public class MyRoomDataManager : BaseJsonConvert {

    public List<FurnitureInfo> mLstFurnitureInfo = new List<FurnitureInfo>();

    public MyRoomDataManager() {
        initData();
    }

    /// <summary>
    /// 외부로부터 데이터를 로드하면 
    /// </summary>
    /// <param name="data"></param>
    public MyRoomDataManager(MyRoomDataManager data) {

        mLstFurnitureInfo = data.mLstFurnitureInfo;
    }

    private void initData(){
        int enumCount;

        FurnitureInfo furnitureElements;
        enumCount = Enum.GetNames(typeof(Achieve_Furniture)).Length;

        mLstFurnitureInfo.Clear();

        for (int i = 0; i < enumCount - 1; ++i) {

            furnitureElements = new FurnitureInfo((Achieve_Furniture)i, 0);
            mLstFurnitureInfo.Add(furnitureElements);
        }
    }
}

[System.Serializable]
public struct FurnitureInfo {
    public Achieve_Furniture furniture;
    public int posIdx;

    public FurnitureInfo(Achieve_Furniture furniture, int posIdx) {
        this.furniture = furniture;
        this.posIdx = posIdx;
    }
}