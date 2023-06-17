using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스토리 진입시 전체 UI 컨트롤
/// </summary>
public class StoryUIController : BaseBehaviour
{
    private Image mImgStoryBg;

    private StoryCharacterController mStoryCharacterController;

    private StoryScriptPanel mStoryScriptPanel;

    private Button mBtnTextTouch;

    protected override void initVariables() {
        base.initVariables();

        mImgStoryBg = Utils.getChild<Image>(trf, "ImgStoryBg");
        mBtnTextTouch = Utils.getChild<Button>(trf, "textTouch");

        mStoryCharacterController = Utils.getChild<StoryCharacterController>(trf, "Character");

        createStoryUI();
    }

    private void createStoryUI() {
        GameObject go;
        string path;

        path = ResPath.PANEL + "StoryScriptPanel";
        go = Utils.createObject(path, trf, "StoryScriptPanel");

        mStoryScriptPanel = Utils.getComponent<StoryScriptPanel>(go);
        mStoryScriptPanel.initMemberVariables();
        mStoryScriptPanel.setFullSize();

        mStoryScriptPanel.setStoryCb(cbEndScript);

        mBtnTextTouch.onClick.AddListener(mStoryScriptPanel.touchTextPanel);
    }

    public void onClickEvent(GameObject obj) {

        switch (obj.name) {

        }
    }

    public IEnumerator playIntro() {
        yield return StartCoroutine(mStoryScriptPanel.playIntro());
    }

    /// <summary>
    /// 스토리 대사집 넣는다.
    /// </summary>
    /// <param name="_bundle"></param>
    public void setStoryScriptData(StoryScriptBundle _bundle) {
        mStoryScriptPanel.setStoryScriptData(_bundle);
    }

    /// <summary>
    /// 메뉴화면으로 돌아올떄 호출하여
    /// 혹시라도 스크립트 나오던 도중에 씬 이동을 하는경우 가리게 하자ㅏㅏㅏ
    /// </summary>
    public void hideScriptPanel() {
        mStoryScriptPanel.hide();
    }

    /// <summary>
    /// 대사를 끝냈을때 실행해야 할 이벤트에 따라 실행함
    /// 스토리쪽에도 필요할것 같다.
    /// </summary>
    /// <param name="scriptEvent"></param>
    /// <param name="idx"></param>
    private void cbEndScript(string scriptEvent, List<int> idx) {
        if (GameSettingMgr.inst.mPlayState == GamePlayStates.Clear) {
            SceneController.inst.startSceneLoad(SceneController.MENU);
            GameSettingMgr.inst.mPlayState = GamePlayStates.Attenstion;
        } else {
            SceneController.inst.startSceneLoad(SceneController.INGAME);
        }
    }

}
