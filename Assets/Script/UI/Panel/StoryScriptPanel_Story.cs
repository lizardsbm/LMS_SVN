using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class StoryScriptPanel : BaseUI
{
    private const string PLAY_UI_SOUND = "PLAY_UI_SOUND";
    private const string PLAY_BGM_SOUND = "PLAY_BGM_SOUND";
    private const string PLAY_EAX_SOUND = "PLAY_EAX_SOUND";

    private const string STOP_BGM_SOUND = "STOP_BGM_SOUND";
    private const string STOP_EAX_SOUND = "STOP_EAX_SOUND";

    private const string PLAY_CUT_SCENE = "PLAY_CUT_SCENE";

    private const string CLOSE_CUT_SCENE = "CLOSE_CUT_SCENE";

    private const string STORY_COMPLETE = "STORY_COMPLETE";

    private StoryScript[] currentSettingScript;

    private StoryScript currentStoryScript {

        get {
            return currentSettingScript[bundleIndex];
        }
    }


    // 스크립팅 시작하는지
    private bool isFlowStory;

    private GameObject mObjCutScene;

    private GameObject mTextBg;
    private Text mTextCharName;
    private Image mImgCutScene;

    private void initStory() {
        mTextBg = Utils.getChild(trf, "TextArea/Name").gameObject;

        mTextCharName = Utils.getChild<Text>(mTextBg, "NameText");
        mImgCutScene = Utils.getChild<Image>(trf, "CutScene/ImgScene");
        mObjCutScene = Utils.getChild(trf, "CutScene").gameObject;
        Utils.setActive(mTextBg, false);
    }

    public IEnumerator playIntro() {
        anim.Play("StoryPanel_In");

        while (anim.isPlaying) {
            yield return null;
        }
    }

    public void setStoryScriptData(StoryScriptBundle _info) {

        Utils.setActive(btnSkip, true);

        show();

        setScriptMode(ScriptPanelMode.STORY);

        if (GameSettingMgr.inst.mPlayState != GamePlayStates.Clear) {
            currentSettingScript = _info.prevScripts;
        } else {
            currentSettingScript = _info.afterScripts;
        }

        bundleIndex = 0;
        initCharacterState();
        startStory();
    }

    private void UpdateStoryScript() {
        if(isFlowStory) {
            controlScriptTime += Time.deltaTime;
            if(controlScriptTime > scriptingTime) {
                currentScriptText++;
                controlScriptTime = 0;
                addScript();
                mMainText.text = currentScript;
            }
        }
    }

    private void touchStoryPanel() {
        if(bundleIndex == currentSettingScript.Length - 1 && mMainText.text == currentStoryScript.textKr) {
            completeScript();
            return;
        }

        if(bundleIndex == currentSettingScript.Length - 1) {
            EndScript();
            return;
        }

        if(isFlowStory) {
            EndScript();
        } else {
            bundleIndex++;
            startStory();
        }
    }

    public void skip()
    {
        if(GameSettingMgr.inst.mPlayState == GamePlayStates.Clear) {
            GameSettingMgr.inst.clearStage();
        }
        completeScript();
    }

    private void showCharacter(int idx, float pos) {

        Sprite spr = getCharacterImage();

        if(spr != null) {
            mLstCharImage[idx].sprite = getCharacterImage();
            mLstCharImage[idx].SetNativeSize();
            Utils.setActive(mLstCharImage[idx], true);
            mLstCharImage[idx].rectTransform.setRectTrfX(pos);
        }
    }

    private void addScript() {

        if(currentScriptText == currentStoryScript.textKr.Length) {
            EndScript();
            return;
        }

        currentScript += currentStoryScript.textKr[currentScriptText];
    }

    /// <summary>
    /// 해당 스크립트 한줄 대사가 끝날때
    /// </summary>
    private void EndScript() {
        isFlowStory = false;
        mMainText.text = currentStoryScript.textKr;
    }

    // 스토리 스크립트 대사가 완전히 종료되었을때.
    public void completeScript() {
        List<int> tempLst = new List<int>();
        hideName();
        hide();

        if (mCbEndScript != null)
        {
            mCbEndScript("Temp",tempLst);
        }
    }

    public void setStoryCb(System.Action<string, List<int>> cb)
    {
        mCbEndScript = cb;
    }

    private void hideName() {
        Utils.setActive(mTextBg, false);
    }

    private void executeEvent(List<string> eventName, List<string> eventContent) {

        for(int i = 0; i < eventName.Count; ++i) {
            switch(eventName[i]) {
                case PLAY_UI_SOUND:
                    SoundManager.inst.playUISound(eventContent[0]);
                    break;

                case PLAY_BGM_SOUND:
                    SoundManager.inst.playBGM(eventContent[0]);
                    break;

                case PLAY_EAX_SOUND:
                    SoundManager.inst.playEAXSound(eventContent[0]);
                    break;

                case STOP_BGM_SOUND:
                    SoundManager.inst.fadeOutBGM();
                    break;

                case STOP_EAX_SOUND:
                    SoundManager.inst.fadeOutEAX();
                    break;

                case PLAY_CUT_SCENE:
                    playCutScene(eventContent[0]);
                    break;

                case CLOSE_CUT_SCENE:
                    closeCutScene();
                    break;

                case STORY_COMPLETE:
                    GameSettingMgr.inst.clearStage();
                    break;
            }
        }
    }

    private void closeCutScene() {
        mObjCutScene.SetActive(false);
    }

    private void playCutScene(string imgName) {

        string path = string.Format("{0}{1}", ResPath.STORY_CUT_SCENE, imgName);

        mImgCutScene.sprite = Utils.loadRes<Sprite>(path);
        anim.Play("StoryPanel_CutScene");
    }

    /// <summary>
    /// 스크립트를 시작할때 호출
    /// </summary>
    private void startStory()
    {
        currentScript = null;
        currentScriptText = 0;
        isFlowStory = true;

        showCharacter(currentStoryScript.displayType - 1, currentStoryScript.charPos);

        if(currentStoryScript.lstEvent[0] != "-") {
            executeEvent(currentStoryScript.lstEvent, currentStoryScript.lstEventContent);
        }

        if (currentStoryScript.name != "플레이어")
        {
            Utils.setActive(mTextBg, true);
            mTextCharName.text = currentStoryScript.name;

        }
        else
        {
            hideName();
        }



        addScript();
    }

    private Sprite getCharacterImage() {

        string path = string.Format("{0}{1}/{2}", ResPath.CHARACTER, currentStoryScript.characterName, currentStoryScript.emotion);

        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }


}  
