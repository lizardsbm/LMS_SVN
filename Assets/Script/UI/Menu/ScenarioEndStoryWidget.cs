using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndStoryWidget : BaseBehaviour
{
    private GameObject mObjClear;
    private GameObject mObjLock;


    // 인게임 클리어 후 타이틀 제목 서브,메인

    private const string LOCAL_SCENA_AFTER_SUB = "tx_clear_sub_title_stage_{0}";
    private const string LOCAL_SCENA_AFTER_MAIN = "tx_clear_main_title_stage_{0}";
    // 이거 추후에 로컬라이징 해야함
    private const string LOCAL_SCENARIO = "EPISODE{0}";

    #region 게임 클리어 후
    // 에피소드 분류
    private Text mTextClEpisode;

    // 소제목 부제
    private Text mTextClSubTitle;

    // 클리어 상태
    private Text mTextClMainTitle;

    #endregion

    private Text mTextLoEpisode;

    // 소제목 부제
    private Text mTextLoTitle;

    // 클리어 상태
    private Text mTextLoState;

    private Text mTextLockEpisode;

    protected override void initVariables() {
        base.initVariables();

        mObjClear = Utils.getChild(trf, "ObjClear").gameObject;

        mTextClMainTitle = Utils.getChild<Text>(mObjClear, "Clear/ImgBg/TextMainTitle");
        mTextClEpisode = Utils.getChild<Text>(mObjClear, "Clear/ImgBg/TextEpisode");
        mTextClSubTitle = Utils.getChild<Text>(mObjClear, "Clear/ImgBg/TextSubTitle");


        mObjLock = Utils.getChild(trf, "ObjLockStory").gameObject;
        mTextLockEpisode = Utils.getChild<Text>(mObjLock, "ImgLock/TextEpisode");
    }

    public void setState(GamePlayStates _state) {

        int stageIndex = GameSettingMgr.inst.currentSettingScenario;

        switch (_state) {
            // 시작전과 중도포기는 상태가 같음
            case GamePlayStates.Attenstion:
            case GamePlayStates.GiveUp:
                setActiveIngameState(true);
                mTextLockEpisode.text = string.Format(LOCAL_SCENARIO, stageIndex);
                break;

            case GamePlayStates.Clear:
                setActiveIngameState(false);

                mTextClEpisode.text = string.Format(LOCAL_SCENARIO, stageIndex);
                mTextClSubTitle.text = Localize.Format(LOCAL_SCENA_AFTER_SUB, stageIndex);
                mTextClMainTitle.text = Localize.Format(LOCAL_SCENA_AFTER_MAIN, stageIndex);
                break;

            default:
                // 그 외의 상태로 들어오면 error
                break;
        }
    }

    private void setActiveIngameState(bool active) {
        Utils.setActive(mObjLock, active);
        Utils.setActive(mObjClear, !active);
    }
}
