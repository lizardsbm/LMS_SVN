using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시나리오 가이드에서 상태에 따른 소분류 텍스트 인풋 관리 담당
/// </summary>
public class ScenarioStartIngameWidget : BaseBehaviour
{
    private GameObject mObjAttentsion;
    private GameObject mObjClear;


    // 시나리오 시작 전 타이틀 제목 서브,메인
    private const string LOCAL_SCENA_BEFORE_SUB = "tx_before_enter_sub_title_stage_{0}";
    private const string LOCAL_SCENA_BEFORE_MAIN = "tx_before_enter_main_title_stage_{0}";

    // 이거 추후에 로컬라이징 해야함
    private const string LOCAL_SCENARIO = "EPISODE{0}";
    private const string LOCAL_STATE = "진행완료";

    #region 게임 시작전
    // 에피소드 분류
    private Text mTextAtEpisode;

    // 소제목 부제
    private Text mTextAtSubTitle;
    
    // 메인제목
    private Text mTextAtMainTitle;

    #endregion

    #region 게임 클리어 후
    // 에피소드 분류
    private Text mTextClEpisode;

    // 소제목 부제
    private Text mTextClTitle;

    // 클리어 상태
    private Text mTextClState;

    #endregion

    protected override void initVariables() {
        base.initVariables();

        mObjAttentsion = Utils.getChild(trf, "ObjAttension").gameObject;
        mTextAtEpisode = Utils.getChild<Text>(mObjAttentsion, "Clear/ImgBg/TextEpisode");
        mTextAtMainTitle = Utils.getChild<Text>(mObjAttentsion, "Clear/ImgBg/TextMainTitle");
        mTextAtSubTitle = Utils.getChild<Text>(mObjAttentsion, "Clear/ImgBg/TextSubTitle");

        mObjClear = Utils.getChild(trf, "ObjClear").gameObject;

        mTextClTitle = Utils.getChild<Text>(mObjClear, "ImgClearBg/ImgImfactBox/TextEpisodeTitle");
        mTextClEpisode = Utils.getChild<Text>(mObjClear, "ImgClearBg/TextEpisode");
        mTextClState = Utils.getChild<Text>(mObjClear, "ImgClearBg/TextEpisodeState");
    }

    public void setState(GamePlayStates _state) {

        int stageIndex = GameSettingMgr.inst.currentSettingScenario;

        switch (_state) {
            // 시작전과 중도포기는 상태가 같음
            case GamePlayStates.Attenstion:
            case GamePlayStates.GiveUp:
                setActiveIngameState(true);

                mTextAtEpisode.text = string.Format(LOCAL_SCENARIO, stageIndex);
                mTextAtMainTitle.text = Localize.Format(LOCAL_SCENA_BEFORE_MAIN, stageIndex);
                mTextAtSubTitle.text = Localize.Format(LOCAL_SCENA_BEFORE_SUB, stageIndex);
                break;

            case GamePlayStates.Clear:
                setActiveIngameState(false);

                mTextClEpisode.text = string.Format(LOCAL_SCENARIO, stageIndex);
                mTextClState.text = LOCAL_STATE;
                mTextClTitle.text = Localize.Format(LOCAL_SCENA_BEFORE_MAIN, stageIndex);
                break;

            default:
                // 그 외의 상태로 들어오면 error
                break;
        }
    }

    private void setActiveIngameState(bool active) {
        Utils.setActive(mObjAttentsion, active);
        Utils.setActive(mObjClear, !active);
    }
}
