using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메뉴에서 전방 프레임에서 쓰이는 UI 에 대한 제어
/// </summary>
public class FrontMenuPanel : BaseBehaviour {

    private const float ALPHA_TIME = 0.2f;

    // 인디케이터 초기위치
    private float INDICATOR_INIT_POS = 445;

    // 인디케이터 간격
    private float INDICATOR_INTERVAL_POS = 45;

    private string[] mArrayTitle = new string[3];

    // 위젯 타이틀 이미지
    private Text mTextWidgetTitle;

    // 스와이프 자동으로 촤락 해주는애
    private PagingScrollViewController mPagingScrollViewController;

    // 인디케이터
    private Image mImgIndicator;

    private float flowTime = 0;

    private float targetIndiCatorPos;

    private Indicator_State mIndicator_State;


    protected override void initVariables() {
        base.initVariables();


        mArrayTitle[0] = "PROFILE";
        mArrayTitle[1] = "ROMANCE";
        mArrayTitle[2] = "FAMILY TREE";

        mTextWidgetTitle = Utils.getChild<Text>(trf, "MainFrame/WidgetTitle");

        mImgIndicator = Utils.getChild<Image>(trf, "MainFrame/Indicator");

        mIndicator_State = Indicator_State.NONE;
    }
    
    public void init(PagingScrollViewController pagingScroll) {
        mPagingScrollViewController = pagingScroll;
        mPagingScrollViewController.mCbEndPaging = setPageInfo;
    }

    private void Update() {

        switch (mIndicator_State) {
            case Indicator_State.FADE_IN_ALPHA:
                flowTime += Time.deltaTime;

                mImgIndicator.setAlpha(Mathf.Lerp(1, 0, flowTime / ALPHA_TIME));

                if (flowTime > ALPHA_TIME) {

                    flowTime = 0;

                    mImgIndicator.setAlpha(0);
                    mImgIndicator.rectTransform.setRectTrfX(targetIndiCatorPos);
                    mIndicator_State = Indicator_State.FADE_OUT_ALPHA;
                }

                break;

            case Indicator_State.FADE_OUT_ALPHA:
                flowTime += Time.deltaTime;

                mImgIndicator.setAlpha(Mathf.Lerp(0, 1, flowTime / ALPHA_TIME));

                if (flowTime > ALPHA_TIME) {
                    mImgIndicator.setAlpha(1);
                    mIndicator_State = Indicator_State.NONE;
                }
                break;

        }
    }

    /// <summary>
    /// 스와이프 끝났을떄 제목을 바꿈
    /// </summary>
    /// <param name="idx"></param>
    private void setPageInfo(int idx) {
        mTextWidgetTitle.text = mArrayTitle[idx];

        targetIndiCatorPos = INDICATOR_INIT_POS + idx * INDICATOR_INTERVAL_POS;

        if (mImgIndicator.rectTransform.anchoredPosition.x != targetIndiCatorPos) {
            mIndicator_State = Indicator_State.FADE_IN_ALPHA;
        }
    }

    private enum Indicator_State {
        FADE_IN_ALPHA,
        FADE_OUT_ALPHA,
        NONE
    }
}
