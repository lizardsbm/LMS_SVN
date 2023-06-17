using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : BaseBehaviour
{
    ParticleSystem[] _particle;

    private float mDuration;

    protected override void initVariables() {
        base.initVariables();
        _particle = trf.GetComponentsInChildren<ParticleSystem>();
    }

    public bool isPlay;

    private void Update() {
        if (isPlay) {
            mDuration += Time.deltaTime;
            if(mDuration > _particle[0].main.duration) {
                hide();
                isPlay = false;
            }
        }    
    }

    public void show(Vector3 pos) {
        isPlay = true;
        mDuration = 0;

        trf.position = pos;

        for(int i=0;i< _particle.Length; ++i) {
            _particle[i].Play();
        }

        base.show();
    }

    public override void hide() {
        isPlay = false;
        base.hide();
    }
}
