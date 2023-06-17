using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임에서 사용될 패널
/// </summary>
public class IngameScriptPanel : BaseBehaviour
{
    private const string SCRIPT_APPEAR = "Appear_IngameScriptPanel";

    // 실제 아이템, 스크립트 대사를 넣어줄것임
    private IngameScriptItem mIngameScriptItem;

    private IngameSubPopup mIngameSubPopup;

    private Animation mAnim;

    protected override void initVariables()
    {
        base.initVariables();

        mAnim = Utils.getComponent<Animation>(trf);

        mIngameScriptItem = Utils.getChild<IngameScriptItem>(trf, "IngameScriptItem");

        mIngameSubPopup = Utils.getChild<IngameSubPopup>(trf, "IngameSubPopup");
        mIngameSubPopup.hide();
    }

    public void onTouchPanel()
    {
        if (mIngameSubPopup.isActive) {
            mIngameSubPopup.hide();
        }

        mIngameScriptItem.startNextPhase();
    }

    /// <summary>
    /// 대사를 마친뒤 실행할 이벤트 스크립트가 있는지. 콜백 등록
    /// </summary>
    /// <param name="cb"></param>
    public void setCb(System.Action<string, List<string>> cb)
    {
        mIngameScriptItem.initCallback(cb,showSubPopup);
    }

    /// <summary>
    /// 다음 대사를 표기하기 위해 세팅해줌
    /// </summary>
    /// <param name="_info"></param>
    public void setNextIngameScriptData(IngameScriptData[] _info)
    {
        if (mIngameSubPopup.isActive) {
            mIngameSubPopup.hide();
        }

        show();
        mIngameScriptItem.show(_info);
        mAnim.Play(SCRIPT_APPEAR);
    }

    public void showSubPopup(string subPopupIndex) {

        int iPopupIndex = int.Parse(subPopupIndex);
        mIngameSubPopup.showSubPopup(iPopupIndex);
    }

    public void changeScriptPos(float y) {
        mIngameScriptItem.changeScriptPos(y);
    }

    public void resetScriptPos() {
        mIngameScriptItem.resetScriptPos();
    }
}
