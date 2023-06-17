using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 게임 데이터를 관리하는 클래스
/// </summary>
public partial class DataManager : BaseSingleton<DataManager>
{
    // 소리 설정 
    public SoundData mSoundData = new SoundData();
    public GamePlayData mGPData = new GamePlayData();

    public override void init() {
        base.init();
        obj.name = "DataManager";

        mMyRoomDataManager = new MyRoomDataManager();
        mAchievementsDataManager = new AchievementsDataManager();
        mCharacterDataManager = new CharacterDataManager();

        initGPData();
    }

    /// <summary>
    /// 모든 데이터 일괄 저장할떄 사용
    /// </summary>
    public void saveAllData() {

        string charData;
        string roomData;
        string AchieveData;

        roomData = mMyRoomDataManager.getJsonData();
        charData = mCharacterDataManager.getJsonData();
        AchieveData = mAchievementsDataManager.getJsonData();

        string allData = string.Format("{{\"Achievements\":{0},\"Character\":{1},\"MyRoom\":{2}}}", AchieveData, charData, roomData);

        FirebaseManager.inst.saveAllData(allData);

        //mFirebaseDatabaseManager.saveCharacterData(charData);
        //mFirebaseDatabaseManager.saveAchievementsData(AchieveData);
        //mFirebaseDatabaseManager.saveMyRoomData(roomData);
    }

    public void save() {

    }

    public void LoadData() {
        FirebaseManager.inst.loadAllData(this);
    }

    // 데이터 뭐뭐가 필요할까..
    // 현재 클리어 된 스테이지
    // 현재 진행중인 스테이지
    // 현재 쥐가 무엇인지
    // 가게도
    // 가구 상태
    // 얻은 가구
    // 옵션
    // 업적
    // 
}
