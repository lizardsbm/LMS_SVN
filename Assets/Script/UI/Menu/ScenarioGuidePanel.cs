using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시작 버튼 눌렀을때 에피소드 가이드 버튼
/// </summary>
public class ScenarioGuidePanel : BaseStackablePanel<UIKey>
{
    private const string SPR_ALL_CLEAR = "Img_AllClear";
    private const string SPR_BEFORE_ENTER = "Img_BeforeEnter";
    private const string SPR_RETURN_MENU = "Img_ReturnMenu";

    // 시나리오 시작 전 타이틀 제목 서브,메인

    
    // 배경 이미지
    private Image mImgBackGround;

    // 에피소드 이름
    private Text mTxtScenarioName;

    private Image mImgScenarioBackground;

    #region 상태별 텍스트 정보

    private ScenarioStartIngameWidget mScenarioStartIngameWidget;
    private ScenarioEndStoryWidget mScenarioEndStroyWidget;

    #endregion


    protected override void initVariables()
    {
        base.initVariables();

        mImgBackGround = Utils.getChild<Image>(trf, "ImgBg");
        mTxtScenarioName = Utils.getChild<Text>(trf, "Title");

        mScenarioStartIngameWidget = Utils.getChild<ScenarioStartIngameWidget>(trf, "StartIngame");
        mScenarioStartIngameWidget.initMemberVariables();

        mScenarioEndStroyWidget = Utils.getChild<ScenarioEndStoryWidget>(trf, "StartEndStory");
        mScenarioEndStroyWidget.initMemberVariables();
    }

    /// <summary>
    /// 에피소드 데이터 세팅
    /// </summary>
    public void setScenarioInfo()
    {
        setScenarioText();
        setScenarioBackground();
    }

    private void setScenarioText() {

        mScenarioStartIngameWidget.setState(GameSettingMgr.inst.mPlayState);
        mScenarioEndStroyWidget.setState(GameSettingMgr.inst.mPlayState);
    }

    private void setScenarioBackground() {

        Sprite imgScenarioBg = null;

        string fileName = null;

        switch (GameSettingMgr.inst.mPlayState) {
            case GamePlayStates.Attenstion:
                fileName = SPR_BEFORE_ENTER;
                break;

            case GamePlayStates.GiveUp:
                fileName = SPR_RETURN_MENU;
                break;

            case GamePlayStates.Clear:
                fileName = SPR_ALL_CLEAR;
                break;
        }

        string path = string.Format(ResPath.SCENA_GUIDE, GameSettingMgr.inst.currentSettingScenario, fileName);
        imgScenarioBg = Utils.loadRes<Sprite>(path);
    }

    /// <summary>
    /// 시나리오 확인 완료 후 스토리로 진입한다.
    /// </summary>
    private void startStory()
    {
        SceneController.inst.startSceneLoad(SceneController.STORY);
    }


    /// <summary>
    /// 메인배경 뒤에 들어갈 스프라이트 할당
    /// </summary>
    /// <param name="path"></param> 
    /// <returns></returns>
    private Sprite getSprite(string path)
    {
        Sprite spr;

        spr = Resources.Load<Sprite>(ResPath.SCENA_GUIDE + path);

        return spr;
    }

    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    /// <param name="btn"></param>
    public void onClickButton(GameObject btn)
    {
        switch (btn.name)
        {
            case "BtnGame":
                clickGameArea();
                break;

            case "BtnStory":
                clickStoryArea();
                break;

            case "BtnStart":
                SceneController.inst.startSceneLoad(SceneController.STORY);
                SoundManager.inst.fadeOutBGM();
                break;
        }
    }


    /// <summary>
    /// 게임쪽 부분 클릭했을떄
    /// </summary>
    private void clickGameArea() {

    }

    private void clickStoryArea() {

    }
}
