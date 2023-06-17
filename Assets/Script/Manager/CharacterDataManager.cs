using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 데이터 매니지먼트
/// </summary>
public class CharacterDataManager : BaseJsonConvert {

    // 캐릭터 종류
    public CharKind charKind;

    public CharacterDataManager() {
        
    }

    public void testDataInput() {
        charKind = (CharKind)Random.Range(1, 5);
    }

    /// <summary>
    /// 외부로부터 데이터를 로드하면 
    /// </summary>
    /// <param name="data"></param>
    public CharacterDataManager(CharacterDataManager data) {
        charKind = data.charKind;
    }
}
