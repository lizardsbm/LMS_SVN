using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uTools;

public class TweenCombiner : BaseBehaviour
{
    private Tweener[] allTween;

    protected override void initVariables() {
        base.initVariables();

        allTween = trf.GetComponentsInChildren<Tweener>(true);
    }

#if UNITY_EDITOR

    private void Update() {

        if (Input.GetKeyDown(KeyCode.A)) {
            play();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            playReverse();
        }
    }

#endif

    public void addMainTweenEvent(int idx,UnityEvent cb) {
        allTween[idx].SetOnFinished(cb);
    }

    public void removeEvent() {
        for (int i = 0; i < allTween.Length; ++i) {
            allTween[i].removeAllFinished();
        }
    }

    public void play() {
        for(int i = 0; i < allTween.Length; ++i) {
            allTween[i].Toggle();
            allTween[i].ResetToBeginning();
            allTween[i].PlayForward();
        }
    }

    public void playReverse() {
        for (int i = 0; i < allTween.Length; ++i) {
            allTween[i].Toggle();
            allTween[i].PlayReverse();
        }
    }
}
