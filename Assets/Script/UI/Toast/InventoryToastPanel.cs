using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 토스트 패널
/// </summary>
public class InventoryToastPanel : BaseBehaviour
{
    private float textAlpha = 0;
    private float bgAlpha = 0;

    // 배경 이미지
    private Image mImageBg;

    // 글씨
    private Text mText;

    // 3초동안 보여주고 사라지게함
    private float toastingTime = 3.0f;

    // 0.5초동안 나타나고 사라지게 함
    private float fadeTime = 0.5f;

    private IEnumerator mCoToast;

    // 애니메이션 커브
    public AnimationCurve mCurve;

    // 색 변경을 위한 임시 컬러변수
    private Color tempColor;

    protected override void initVariables() {
        base.initVariables();

        mImageBg = Utils.getComponent<Image>(trf);
        mText = Utils.getChild<Text>(trf, "lbText");

        bgAlpha = mImageBg.color.a;
        textAlpha = mText.color.a;
    }

    public void setText(string toastText) {
        mText.text = toastText;
        Utils.setActive(trf, true);
        startToast();
    }

    /// <summary>
    /// 토스트 시작
    /// </summary>
    private void startToast() {
        stopToast();
        mCoToast = coToast();
        StartCoroutine(mCoToast);
    }

    /// <summary>
    /// 토스트 멈춤
    /// </summary>
    private void stopToast() {
        if(mCoToast != null) {
            StopCoroutine(mCoToast);
        }
    }

    private IEnumerator coToast() {

        float time = 0;
        float alpha = 0;

        // FadeIn
        while(time < fadeTime) {

            time += Time.deltaTime;


            ///////////////////////배경 알파/////////////////////////

            alpha = mCurve.Evaluate(time / fadeTime) * bgAlpha;

            tempColor = mImageBg.color;
            tempColor.a = alpha;

            mImageBg.color = tempColor;

            ///////////////////////글씨 알파/////////////////////////

            alpha = mCurve.Evaluate(time / fadeTime) * textAlpha;

            tempColor = mText.color;
            tempColor.a = alpha;

            mText.color = tempColor;

            yield return null;
        }

        tempColor = mImageBg.color;
        tempColor.a = bgAlpha;

        mImageBg.color = tempColor;

        tempColor = mText.color;
        tempColor.a = textAlpha;

        mText.color = tempColor;

        time = 0;

        while(time < toastingTime) {
            time += Time.deltaTime;

            yield return null;
        }

        time = 0;

        // FadeOut
        while(time < fadeTime) {

            time += Time.deltaTime;

            ///////////////////////배경 알파/////////////////////////

            alpha = bgAlpha - mCurve.Evaluate(time / fadeTime) * bgAlpha;

            tempColor = mImageBg.color;
            tempColor.a = alpha;

            mImageBg.color = tempColor;

            ///////////////////////글씨 알파/////////////////////////

            alpha = textAlpha - mCurve.Evaluate(time / fadeTime) * textAlpha;

            tempColor = mText.color;
            tempColor.a = alpha;

            mText.color = tempColor;

            yield return null;
        }

        tempColor = mImageBg.color;
        tempColor.a = 0;

        mImageBg.color = tempColor;

        tempColor = mText.color;
        tempColor.a = 0;

        mText.color = tempColor;

        hide();
    }
}
