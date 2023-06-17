using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메뉴에서 중앙에서 분할 없이 띄워지는 녀석
/// </summary>
public class CommonMenuPanel : BaseBehaviour
{
    // 맵선택 팝업
    public MapSelectPopup mMapPopup;

    // 인테리어 패널로 변경
    public InteriorPanel mInteriorPanel;

    // 시나리오 가이드 패널(게임 시작 누르면 간단하게 나오는 에피소드 설명)
    public ScenarioGuidePanel mScenarioGuidePanel;

    // 업적 팝업창
    public AchievementPopup mAchievementPopup;


    protected override void initVariables()
    {
        base.initVariables();
        //mMapPopup.initMemberVariables();
        mScenarioGuidePanel.initMemberVariables();
        mInteriorPanel.initMemberVariables();
        mAchievementPopup.initMemberVariables(true);
    }

    public void showMap()
    {
        mMapPopup.show();
    }

    public void showDecoUI()
    {
        mInteriorPanel.show();
    }

    public void hideDecoUI()
    {
        mInteriorPanel.close();
    }

    public void showScenarioInfo()
    {
        mScenarioGuidePanel.setScenarioInfo();
        mScenarioGuidePanel.show();
    }

    public void showAchievementPopup()
    {
        mAchievementPopup.show();
    }

    public void hideachievementPopup()
    {
        mAchievementPopup.hide();
    }
}
