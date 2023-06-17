using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 게임 데이터를 관리하는 클래스(마이룸)
/// </summary>
public partial class DataManager : BaseSingleton<DataManager>
{
    // 마이룸 데이터
    public MyRoomDataManager mMyRoomDataManager;

    public void setMyRoomData() {

    }


    // 각각 개별 데이터를 저장하는 경우
    public void setMyRoomData(FurnitureInfo _data) {
        mMyRoomDataManager.mLstFurnitureInfo[(int)_data.furniture] = _data;
    }

    /// <summary>
    /// 캐릭터 데이터 로드 시도
    /// </summary>
    public void loadMyRoomData() {
        FirebaseManager.inst.loadMyRoomData(mMyRoomDataManager);
    }

    /// <summary>
    /// 캐릭터 데이터 세이브 시도
    /// </summary>
    public void saveMyRoomData() {
        FirebaseManager.inst.saveChildData(mMyRoomDataManager.getJsonData(), SaveType.MyRoom);
    }
}
