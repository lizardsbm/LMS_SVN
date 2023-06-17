using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임에서 스크립트 하나하나를 담을 아이템
/// </summary>
public class IngameScriptItem : BaseBehaviour
{
    private const string START_LOOP_ARROW = "ScriptLoopArrow";

    private IngameScriptData currentData {
        get {
            return mIngameScriptData[bundleIndex];
        }
    }

    private IngameScriptData prevData {
        get {
            if(bundleIndex == 0) {
                return null;
            }

            return mIngameScriptData[bundleIndex-1];
        }
    }

    private CustomText mTextScript;

    private Animation mAnim;

    // 화살표 이미지
    private Image mImgArrow;

    // 현재 재생해야 할 스크립트 정보
    private IngameScriptData[] mIngameScriptData;

    private int bundleIndex = 0;

    private System.Action<string> mCbShowSubPopup;

    // 대사가 시작됨과 동시에 스크립트 이벤트를 발생시킴
    private System.Action<string, List<string>> mCbEventScript;

    protected override void initVariables()
    {
        base.initVariables();

        mTextScript = Utils.getChild<CustomText>(trf, "Text");
        mImgArrow = Utils.getChild<Image>(trf, "Arrow");
        mAnim = Utils.getComponent<Animation>(trf);
    }

    public void initCallback(System.Action<string, List<string>> cb, System.Action<string> showSubPopupCb)
    {
        mCbEventScript = cb;
        mCbShowSubPopup = showSubPopupCb;
    }

    public void show(IngameScriptData[] _info)
    {
        Utils.setActive(trf, true);
        mIngameScriptData = _info;

        if (_info.Length == 1)
        {
            IngameDataManager.inst.isFlowMultyScript = false;
            Utils.setActive(mImgArrow, false);
        }
        else
        {
            IngameDataManager.inst.isFlowMultyScript = true;
            Utils.setActive(mImgArrow, true);
            mAnim.Play(START_LOOP_ARROW);
        }

        setScript();
    }

    private void setScript()
    {
        mTextScript.text = currentData.textKr;

        checkShowSubPopup(currentData);

        for (int i = 0; i < currentData.scriptEvent.Count; ++i)
        {
            if (currentData.scriptEvent[i] != null)
            {
                if (GameManager.isBeforeEventType(currentData.scriptEvent[i])) {

                    if (currentData.scriptEvent[i] == GameManager.END_GAME) {
                        currentData.eventIdx[i][0] = currentData.textKr;
                    }
                        mCbEventScript(currentData.scriptEvent[i], currentData.eventIdx[i]);
                }
            }
        }
    }

    /// <summary>
    /// 특정 상황에서 팝업을 켜줘야 하는경우
    /// </summary>
    /// <param name="_info"></param>
    private void checkShowSubPopup(IngameScriptData _info)
    {
        for (int i = 0; i < _info.scriptEvent.Count; ++i)
        {
            // 서브팝업은 이벤트가 1개밖에 없어야 함
            if (_info.scriptEvent[i] == GameManager.SHOW_SUB_POPUP)
            {
                mCbShowSubPopup(_info.eventIdx[i][0]);
                break;
            }
        }
    }

    public void startNextPhase()
    {
        bundleIndex++;

        if (bundleIndex < mIngameScriptData.Length)
        {
            setScript();
        }
        else
        {
            if (prevData != null) {
                for (int i = 0; i < prevData.scriptEvent.Count; ++i) {
                    if (!GameManager.isBeforeEventType(prevData.scriptEvent[i])) {
                        mCbEventScript(prevData.scriptEvent[i], prevData.eventIdx[i]);
                    }
                }
            }

            IngameDataManager.inst.isFlowMultyScript = false;
            startDisappear();
        }
    }


    public void startDisappear()
    {
        bundleIndex = 0;
        Utils.setActive(trf, false);
    }

    public void changeScriptPos(float y) {
        rectTrf.setRectPosY(y);
    }

    public void resetScriptPos() {
        rectTrf.setRectPosY(0);
    }
}
