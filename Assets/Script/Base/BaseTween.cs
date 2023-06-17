using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 트윈 스케일, 알파, 포지션 등 공통적으로 사용할 베이스
/// </summary>
public class BaseTween : BaseBehaviour
{
    // 트윈 커브
    [HideInInspector]
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    protected List<System.Action> mLstCb = new List<System.Action>();

    protected float flowTime = 0;

    protected bool isDelay;

    // 증감값에 대한 보정값을 함
    protected int valueCorrection = 1;

    protected bool isReverse;

    [HideInInspector]
    // 트윈을 하는데 걸리는 시간을 표기
    public float duration;

    [HideInInspector]
    // 시작 딜레이 초
    public float delay;

    [HideInInspector]
    public Style style;

    public enum Style
    {
        Once,
        Loop,
        PingPong,
    }

    /// <summary>
    /// 플레이 시작
    /// </summary>
    public virtual void play()
    {
        isReverse = false;
        enabled = true;
        flowTime = 0;
    }

    public virtual void reverse() {
        play();
        isReverse = true;
    }

    public void pause()
    {
        enabled = false;
    }

    public void resume()
    {
        enabled = true;
    }

    public void replay()
    {
        flowTime = 0;
        isDelay = true;
    }

    private void OnDisable()
    {
        valueCorrection = 1;
    }

    public void addEvent(System.Action cb)
    {
        mLstCb.Add(cb);
    }

    public void removeEvent(System.Action cb)
    {
        mLstCb.Remove(cb);
    }

    public void clearEvent()
    {
        mLstCb.Clear();
    }

    /// <summary>
    /// 트윈이 끝난뒤의 콜백을 실행시킨다.
    /// </summary>
    protected void excuteCallback()
    {
        for(int i = 0; i < mLstCb.Count; ++i)
        {
            if(mLstCb[i] != null)
            {
                mLstCb[i]();
            }
        }
    }
}
