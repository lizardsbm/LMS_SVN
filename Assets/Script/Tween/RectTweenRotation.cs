using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rect Transform 전용 트윈 로테이션
/// </summary>
public class RectTweenRotation : BaseTween
{
    [HideInInspector]
    public Vector3 from;

    [HideInInspector]
    public Vector3 to;

    private Vector3 value;

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
        rectTrf.setRotation(from);
    }

    public void setTo()
    {
        rectTrf.setRotation(to);
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

        value = Vector3.Lerp(from, to, curve.Evaluate(flowTime / duration));
        rectTrf.localEulerAngles = value;

        flowTime += Time.deltaTime * valueCorrection;

        if(flowTime > duration)
        {
            rectTrf.localEulerAngles = to;

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