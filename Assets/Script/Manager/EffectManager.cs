using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseBehaviour
{
    private TouchEffect[] effects;

    private Camera mCam;

    public Vector3 mPos;

    protected override void initVariables() {
        base.initVariables();
        DontDestroyOnLoad(obj);
        effects = trf.GetComponentsInChildren<TouchEffect>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            touch();
        }

        mPos = Input.mousePosition;
    }

    private void touch() {
        Vector3 pos = Input.mousePosition;
        pos.z = 0;
        getLiveEffect().show(pos);
    }

    private TouchEffect getLiveEffect() {
        for(int i=0;i< effects.Length; ++i) {
            if (!effects[i].isPlay) {
                return effects[i];
            }
        }

        return null;
    }

}
