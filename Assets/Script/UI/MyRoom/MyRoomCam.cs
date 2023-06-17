using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomCam : BaseBehaviour
{
    private const float ANIM_TIME = 0.8f;

    private const float EXPEND_X = 0;
    private const float EXPEND_W = 1;
    private const float EXPEND_Y = 0;
    private const float EXPEND_H = 1;

    private const float REDUCE_X = 0.1f;
    private const float REDUCE_W = 0.52f;
    private const float REDUCE_Y = 0.18f;
    private const float REDUCE_H = 0.7f ;

    public AnimationCurve mCurve;

    private float flowTime;

    private Camera mMyRoomCam;

    protected override void initVariables() {
        base.initVariables();

        mMyRoomCam = Utils.getComponent<Camera>(trf);
    }

    public void extrudeCamView(bool isExpend) {
        if (isExpend) {
            StartCoroutine(coExpendView());
        } else {
            StartCoroutine(coReduceView());
        }
    }

    /// <summary>
    /// 카메라 확장
    /// </summary>
    /// <returns></returns>
    private IEnumerator coExpendView() {

        float x = mMyRoomCam.rect.x;
        float w = mMyRoomCam.rect.width;
        float y = mMyRoomCam.rect.y;
        float h = mMyRoomCam.rect.height;

        Rect changeValue = new Rect();

        flowTime = 0;

        while (flowTime < ANIM_TIME) {

            changeValue.x = Mathf.Lerp(x, EXPEND_X, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.width = Mathf.Lerp(w, EXPEND_W, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.y = Mathf.Lerp(y, EXPEND_Y, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.height = Mathf.Lerp(h, EXPEND_H, mCurve.Evaluate(flowTime / ANIM_TIME));

            mMyRoomCam.rect = changeValue;

            flowTime += Time.deltaTime;

            yield return null;
        }

        mMyRoomCam.rect = new Rect(EXPEND_X, EXPEND_Y, EXPEND_W, EXPEND_H);
    }

    /// <summary>
    /// 카메라 축소
    /// </summary>
    /// <returns></returns>
    private IEnumerator coReduceView() {

        float x = mMyRoomCam.rect.x;
        float w = mMyRoomCam.rect.width;
        float y = mMyRoomCam.rect.y;
        float h = mMyRoomCam.rect.height;

        Rect changeValue = new Rect();

        flowTime = 0;

        while (flowTime < ANIM_TIME) {

            changeValue.x = Mathf.Lerp(x, REDUCE_X, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.width = Mathf.Lerp(w, REDUCE_W, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.y = Mathf.Lerp(y, REDUCE_Y, mCurve.Evaluate(flowTime / ANIM_TIME));
            changeValue.height = Mathf.Lerp(h, REDUCE_H, mCurve.Evaluate(flowTime / ANIM_TIME));

            mMyRoomCam.rect = changeValue;

            flowTime += Time.deltaTime;

            yield return null;
        }

        mMyRoomCam.rect = new Rect(REDUCE_X, REDUCE_Y, REDUCE_W, REDUCE_H);
    }
}
