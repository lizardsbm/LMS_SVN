using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 인게임에서 배경을 클릭 했을때 이벤트 진행 및 아이템에 대한 인덱스를 가지고 있을 컴포넌트
/// </summary>
public class IngameItem : BaseBehaviour
{
    private const float FADE_TIME = 0.3f;

    private float time = 0;

    private float startAlpha = 0;
    private float endAlpha = 0;

    private Color mColorItem;

    private float alpha = 0;

    // 아이템 인덱스는 하이어라키에서 세팅하고, 세팅된 아이템 인덱스를 기준으로 필요한 정보를 넣어준다

    // 아이템 인덱스
    public int itemIdx;

    public bool isEnable;

    // 선행 조건
    public List<int[]> mPrerequisites = new List<int[]>();

    // 리턴 스크립트
    public List<int> mReturnScript = new List<int>();

    private System.Action<IngameItem> mCbClick;

    // 활성 혹은 비활성화 시작하는지
    private bool aniStart;

    private SpriteRenderer mSprItem;

    protected override void initVariables() {
        base.initVariables();
        mSprItem = Utils.getComponent<SpriteRenderer>(trf);
    }

    private void Update() {
        if (aniStart) {

            time += Time.deltaTime;
            if(time < FADE_TIME) {
                mColorItem.a = Mathf.Lerp(startAlpha, endAlpha, time / FADE_TIME);
                mSprItem.color = mColorItem;
            }

            if(time > FADE_TIME) {
                aniStart = false;
                mColorItem.a = endAlpha;
                mSprItem.color = mColorItem;
                time = 0;

                if (endAlpha == 0) {
                    hide();
                }
            }
        }
    }

    /// <summary>                                                     
    /// 필요한 정보를 세팅한다.
    /// </summary>
    /// <param name="_data"></param>
    public void setInfo(IngameItemData _data) {
        itemIdx = _data.inagmeItemIdx;
        mPrerequisites = _data.prerequisites;
        mReturnScript = _data.returnScript;
    }

    public void init(System.Action<IngameItem> cb) {
        mCbClick = cb;
    }


    public override void show()
    {
        isEnable = true;
        base.show();
    }

    public void enableItem() {

        show();

        aniStart = true;

        mColorItem = mSprItem.color;
        startAlpha = 0;
        endAlpha = 1;
    }

    public override void hide() {
        isEnable = false;
        base.hide();
    }

    public void disableItem() {
        mColorItem = mSprItem.color;
        aniStart = true;
        startAlpha = 1;
        endAlpha = 0;
    }

    private void OnMouseUp() {

        Debug.Log(trf.name + " 터치");

#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject() || IngameDataManager.inst.isFlowMultyScript) {
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId) || IngameDataManager.inst.isFlowMultyScript) {
#endif
            // UI 터치가 아닐떄만 작동
            // 현재 나오고있는 스크립트 여러줄이 나오는게 아닐때만.(2020.05.29 추가)
            return;
        } else {
            mCbClick(this);
        }

    }
}
