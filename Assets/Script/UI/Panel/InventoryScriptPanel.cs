using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScriptPanel : BaseBehaviour
{
    // 현재 재생해야 할 스크립트 정보
    private IngameScriptData[] mIngameScriptData;

    // 화면 하단의 대화창에서 나올 텍스트 창
    private Text mMainText;

    // 스크립트 제어 시간
    private float controlScriptTime = 0;

    // 0.05초마다 다음 문구로 이동
    public float scriptingTime = 0.05f;

    // 스크립팅 시작하는지
    private bool isFlowGuideItem = false;

    // 스크립트 대사 인덱스
    private int currentScriptText = 0;

    // 현재 대사를 담아둠
    private string currentScript;

    // 번들 인덱스
    private int bundleIndex = 0;

    // 대사가 종료되면 호출 할 스크립트
    private System.Action<string, List<string>> mCbEndScript;

    protected override void initVariables() {
        base.initVariables();

        mMainText = Utils.getChild<Text>(trf, "lbText");
    }

    private void Update() {
        if(isFlowGuideItem) {
            controlScriptTime += Time.deltaTime;
            if(controlScriptTime > scriptingTime) {
                currentScriptText++;
                controlScriptTime = 0;
                addItemScript();
                mMainText.text = currentScript;
            }
        }
    }

    public void setItemCb(System.Action<string, List<string>> cb) {
        mCbEndScript = cb;
    }

    private void initState() {
        mMainText.text = null;
        currentScript = null;
        currentScriptText = 0;
        bundleIndex = 0;
    }

    public void setItemText(IngameScriptData[] _info) {

        initState();

        bundleIndex = 0;

        mIngameScriptData = _info;
        addItemScript();
        isFlowGuideItem = true;
    }

    private void addItemScript() {

        if(currentScriptText == mIngameScriptData[bundleIndex].textKr.Length) {
            EndScript();
            return;
        }

        currentScript += mIngameScriptData[bundleIndex].textKr[currentScriptText];
    }

    private void EndScript() {
        isFlowGuideItem = false;
        mMainText.text = mIngameScriptData[bundleIndex].textKr;
    }

    /// <summary>
    /// 터치 이벤트
    /// </summary>
    public void touchIngamePanel() {

        if(bundleIndex == mIngameScriptData.Length - 1) {
            EndScript();
            return;
        }

        if(isFlowGuideItem) {
            EndScript();
        } else {
            bundleIndex++;
            startIngameScript();
        }
    }

    /// <summary>
    /// 스크립트를 시작할때 호출
    /// </summary>
    private void startIngameScript() {
        currentScript = null;
        currentScriptText = 0;
        isFlowGuideItem = true;

        //showCharacter(mIngameScriptData[bundleIndex].displayType - 1);
        addItemScript();
    }

    public void endIngameScript() {

        if(mCbEndScript != null) {
            for(int i = 0; i < mIngameScriptData[bundleIndex].scriptEvent.Count; ++i) {
                mCbEndScript(mIngameScriptData[bundleIndex].scriptEvent[i], mIngameScriptData[bundleIndex].eventIdx[i]);
            }
        }

        texthide();
    }

    public void texthide() {
        isFlowGuideItem = false;
        mMainText.text = null;
    }
}
