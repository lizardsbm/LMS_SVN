using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rect Transform 전용 트윈 포지션
/// </summary>
public class RectTweenPosition : BaseTween
{
    [HideInInspector]
    public Vector2 from;

    [HideInInspector]
    public Vector2 to;

    private Vector2 value;

    public override void play()
    {
        base.play();

        if(delay > 0)
        {
            isDelay = true;
        }
    }

    public void setFrom()
    {
        rectTrf.setPos(from);
    }

    public void setTo()
    {
        rectTrf.setPos(to);
    }

    private void Update()
    {
        if(isDelay)
        {
            flowTime += Time.deltaTime;
            if(flowTime > delay)
            {
                isDelay = false;
                flowTime = 0;
            }
            return;
        }

        if (isReverse) {
            value = Vector2.Lerp(to, from, curve.Evaluate(flowTime / duration));
        } else {
            value = Vector2.Lerp(from, to, curve.Evaluate(flowTime / duration));
        }
        rectTrf.anchoredPosition = value;

        flowTime += Time.deltaTime * valueCorrection;

        if(flowTime > duration)
        {
            if (isReverse) {
                rectTrf.anchoredPosition = from;
            } else {
                rectTrf.anchoredPosition = to;
            }

            switch (style)
            {

                case Style.Loop:
                    flowTime = 0;
                    break;

                case Style.Once:
                    enabled = false;
                    excuteCallback();
                    break;

                case Style.PingPong:
                    valueCorrection *= -1;
                    flowTime = duration;
                    break;
            }
        }

        if(flowTime < 0)
        {
            valueCorrection *= -1;
        }
    }
}