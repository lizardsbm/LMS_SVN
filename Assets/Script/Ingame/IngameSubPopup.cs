using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameSubPopup : BaseBehaviour
{
    // 이 아래부터 다른 스크립트로 옮겨야 함

    private Animation mAnim;

    // 아이템을 얻는경우 사용하는 팝업
    private GameObject mObjItemPopup;
    // 간격 120;
    private const float INTERVAL_ITEM_X = 120;

    private const string ANIM_APPEAR_POPUP = "Appear_ScriptSubPopup";
    private const string ANIM_DISAPPEAR_POPUP = "Disappear_ScriptSubPopup";

    // 상황이 바뀌어야 할떄 쓰이는 팝업
    private GameObject mObjChangeState;

    // 서브팝업을 직접 나타낼 오브젝트
    private Image mImgSubPopup;

    protected override void initVariables() {
        base.initVariables();

        mAnim = Utils.getComponent<Animation>(trf);

        mObjItemPopup = Utils.getChild(trf, "GetItem").gameObject;

        mObjChangeState = Utils.getChild(trf, "ChangeState").gameObject;

        mImgSubPopup = mObjChangeState.GetComponent<Image>();

        hide();
    }

    /// <summary>
    /// 서브팝업을 보여줘야 할때 
    /// </summary>
    public void showSubPopup(int subPopupIndex)
    {
        show();

        mImgSubPopup.sprite = IngameDataManager.inst.mDicSubPopupSprite[subPopupIndex];
        mAnim.Play(ANIM_APPEAR_POPUP);

        Utils.setActive(mObjItemPopup, false);
        Utils.setActive(mObjChangeState, true);
    }
}
