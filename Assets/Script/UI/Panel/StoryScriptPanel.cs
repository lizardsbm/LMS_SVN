using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uTools;

public partial class StoryScriptPanel : BaseUI {

    // 스토리씬에서 애니메이션재생이 필요한경우
    // 1. 인트로, 2. 컷씬
    private Animation anim;

    // 화면 하단의 대화창에서 나올 텍스트 창
    private Text mMainText;

    // 화면에 표현 될 캐릭터 이미지 디폴트로는 2개를 가지고 있고, 만약 추가로 필요한 경우 더 만든다.
    private List<Image> mLstCharImage = new List<Image>();

    // 0.05초마다 다음 문구로 이동
    public float scriptingTime = 0.05f;

    // 스크립트 대사 인덱스
    private int currentScriptText = 0;

    // 스크립트 제어 시간
    private float controlScriptTime = 0;

    // 현재 대사를 담아둠
    private string currentScript;

    private int bundleIndex = 0;

    // 대사가 종료되면 호출 할 스크립트
    private System.Action<string,List<int>> mCbEndScript;

    private ScriptPanelMode mMode;

    public GameObject btnSkip;

    protected override void initVariables()
    {
        base.initVariables();

        mMainText = Utils.getChild<Text>(trf, "TextArea/ScriptText");

        anim = Utils.getComponent<Animation>(trf);

        Transform charTrf;

        charTrf = Utils.getChild(trf, "CharacterPanel");

        for (int i = 0; i < charTrf.childCount; ++i)
        {
            Image image = Utils.getChild<Image>(trf, string.Format("Char_{0}", i + 1));
            mLstCharImage.Add(image);
        }

        initState();
        initStory();
    }


    private void initState() {
        isFlowStory = false;
        mMode = ScriptPanelMode.NONE;
    }

    private void Update() {


        switch(mMode) {

            case ScriptPanelMode.NONE:
                // 아무것도 하지않음
                break;

            case ScriptPanelMode.STORY:
                UpdateStoryScript();
                break;

        }
    }

    private void setScriptMode(ScriptPanelMode _mode) {

        if(mMode != _mode) {
            mMode = _mode;
        }
    }

    /// <summary>
    /// 텍스트 패널을 터치했을떄 작업
    /// </summary>
    public void touchTextPanel() {

        // 페이드인, 아웃중일떄는 터치 안댄다
        if (SceneController.inst.isFading)
        {
            return;
        }

        switch(mMode) {

            case ScriptPanelMode.NONE:
                // 아무것도 하지않음
                break;

            case ScriptPanelMode.STORY:
                touchStoryPanel();
                break;
        }
    }


    private void initCharacterState() {
        for(int i = 0; i < mLstCharImage.Count; ++i) {
            Utils.setActive(mLstCharImage[i], false);
        }
    }


    public override void show()
    {
        mMainText.text = null;
        base.show();
    }
}

public enum ScriptPanelMode {
    NONE,
    STORY,
}