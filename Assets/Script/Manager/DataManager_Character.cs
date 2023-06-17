using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 게임 데이터를 관리하는 클래스(캐릭터)
/// </summary>
public partial class DataManager : BaseSingleton<DataManager>
{
    // 캐릭터 데이터 매니저
    public CharacterDataManager mCharacterDataManager;

    public void setCharacterType(CharKind kind) {
        mCharacterDataManager.charKind = kind;
    }

    /// <summary>
    /// 캐릭터 데이터 로드 시도
    /// </summary>
    public void loadCharacterData() {
        FirebaseManager.inst.loadCharacterData(mCharacterDataManager);
    }

    /// <summary>
    /// 캐릭터 데이터 세이브 시도
    /// </summary>
    public void saveCharacterData() {
        FirebaseManager.inst.saveChildData(mCharacterDataManager.getJsonData(), SaveType.Character);
    }
}
