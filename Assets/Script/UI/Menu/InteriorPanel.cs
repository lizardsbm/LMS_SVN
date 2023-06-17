using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MyRoom 꾸미기 가능한 인테리어 패널
/// </summary>
public class InteriorPanel : BaseBehaviour
{
    private RectTweenPosition mAppearTween;

    public bool isPlayTween {
        get {
            return mAppearTween.enabled;
        }
    }

    protected override void initVariables() {
        base.initVariables();

        mAppearTween = Utils.getComponent<RectTweenPosition>(trf);
    }

    public override void show() {

        if (mAppearTween.enabled) {
            return;
        }

        base.show();
        mAppearTween.play();
    }

    public override void hide() {
        base.hide();
        mAppearTween.removeEvent(endPanel);
    }

    public void close() {
        if (mAppearTween.enabled) {
            return;
        }

        mAppearTween.reverse();
        mAppearTween.addEvent(endPanel);
    }

    private void endPanel() {
        hide();
    }
}
