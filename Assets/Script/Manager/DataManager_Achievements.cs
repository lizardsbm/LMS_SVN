using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 게임 데이터를 관리하는 클래스 (업적)
/// </summary>
public partial class DataManager : BaseSingleton<DataManager>
{
    // 업적 데이터
    public AchievementsDataManager mAchievementsDataManager;

    public void setAchievementsData() {

    }

    // 각각 개별 데이터를 세팅하는 경우

    public void setPatternData(Achieve_Mouse_Pattern _pattern, bool isOpen) {
        mAchievementsDataManager.mLstPattern[(int)_pattern] = isOpen;
    }

    public void setIllustData(Achieve_Illust _illust, bool isOpen) {
        mAchievementsDataManager.mLstIllust[(int)_illust] = isOpen;
    }

    public void setFurniture(Achieve_Furniture _furniture, bool isOpen) {
        mAchievementsDataManager.mLstFurniture[(int)_furniture] = isOpen;
    }

    /// <summary>
    /// 캐릭터 데이터 로드 시도
    /// </summary>
    public void loadAchievementsData() {
        FirebaseManager.inst.loadAchieveData(mAchievementsDataManager);
    }

    /// <summary>
    /// 캐릭터 데이터 세이브 시도
    /// </summary>
    public void saveAchievementsData() {
        FirebaseManager.inst.saveChildData(mAchievementsDataManager.getJsonData(), SaveType.Achieve);
    }
}
