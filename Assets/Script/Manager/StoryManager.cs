using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스토리 씬에 들어왔을때 세팅할 녀석
/// </summary>
public class StoryManager : BaseBehaviour
{
    // 스토리 대사들이 들어있음.
    public StoryScriptDataList mStoryScriptDataList;

    private StoryUIController mStoryUIController;

    protected override void initVariables()
    {
        base.initVariables();
        mStoryUIController = GameObject.Find("StoryCanvas").GetComponent<StoryUIController>();
        mStoryUIController.initMemberVariables();
    }

    protected override void Start() {
        base.Start();

        StartCoroutine(coIntroStory());
    }

    private IEnumerator coIntroStory() {
        yield return StartCoroutine(mStoryUIController.playIntro());

        loadScript();
    }

    private void loadScript()
    {
        TextAsset tAsset = Utils.loadRes<TextAsset>(ResPath.BASE_CSV + Const.CSV_STORY);

        mStoryScriptDataList = new StoryScriptDataList();
        mStoryScriptDataList.parse(tAsset);

        mStoryUIController.setStoryScriptData(mStoryScriptDataList.mLstScript[GameSettingMgr.inst.currentSettingScenario]);
    }
}
