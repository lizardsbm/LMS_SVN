using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파이어베이스 기본인데 데이터베이스 매니저쪽 연결함
/// </summary>
public partial class FirebaseManager : BaseSingleton<FirebaseManager> {

    private const string CHILD_CHARACTER = "Character";
    private const string CHILD_MYROOM = "MyRoom";
    private const string CHILD_ACHIEVE = "Achievements";


    private FirebaseDatabaseManager mFirebaseDatabaseManager;

    private void initDatabase() {
        mFirebaseDatabaseManager = new FirebaseDatabaseManager();
        mFirebaseDatabaseManager.init();
    }

    /// <summary>
    /// 필요한 데이터를 모두 저장한다.
    /// </summary>
    /// <param name="datas"></param>
    public void saveAllData(string datas) {
        mFirebaseDatabaseManager.SaveData(datas);
    }

    /// <summary>
    /// 전체 데이터가 아니라 개별적으로 저장을 시도하는 경우
    /// </summary>
    /// <param name="data"></param>
    /// <param name="_type"></param>
    public void saveChildData(string data, SaveType _type) {

        string childKey = null;

        switch (_type) {
            case SaveType.Achieve:
                childKey = CHILD_ACHIEVE;
                break;

            case SaveType.MyRoom:
                childKey = CHILD_MYROOM;
                break;

            case SaveType.Character:
                childKey = CHILD_CHARACTER;
                break;
        }

        mFirebaseDatabaseManager.saveChildData(data, childKey);
    }

    /// <summary>
    /// 전체 데이터를 로드 한다.
    /// </summary>
    /// <param name="dManager"></param>
    public void loadAllData(DataManager dManager) {
        mFirebaseDatabaseManager.LoadData(dManager);
    }

    public void loadCharacterData(CharacterDataManager charData) {
        mFirebaseDatabaseManager.LoadChildData(charData,CHILD_CHARACTER);
    }

    public void loadMyRoomData(MyRoomDataManager roomData) {
        mFirebaseDatabaseManager.LoadChildData(roomData, CHILD_MYROOM);
    }

    public void loadAchieveData(AchievementsDataManager aData) {
        mFirebaseDatabaseManager.LoadChildData(aData, CHILD_ACHIEVE);
    }
}
