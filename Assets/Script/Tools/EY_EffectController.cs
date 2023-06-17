using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EY_EffectController : BaseBehaviour
{
    public ParticleSystem[] particleSystems;
    public TweenCombiner[] tweenCombiners;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
            for (int i = 0; i < tweenCombiners.Length; i++)
            {
                tweenCombiners[i].play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            for (int i = 0; i < tweenCombiners.Length; i++)
            {
                tweenCombiners[i].playReverse();
            }
        }
    }
}