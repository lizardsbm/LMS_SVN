using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이것저것 게임 세팅할 녀석
/// </summary>
public class GameSettingMgr :BaseSingleton<GameSettingMgr> {

    // 게임 진행 상태
    public GamePlayStates mPlayState;

    public int currentSettingScenario = 1;

    // 씬을 이동한 뒤에 나올 함수 타입
    public FunKind mFunKind;

    public override void init() {
        base.init();
        obj.name = "GameSettingMgr";

        currentSettingScenario = 1;
    }

    public void clearStage() {
        currentSettingScenario++;
    }
}
