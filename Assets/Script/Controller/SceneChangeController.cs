using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 씬 전환 효과를 제어함
/// </summary>
public class SceneChangeController : BaseBehaviour
{
    private const string FADE_OUT_LOAD = "SceneChangeFadeOut";
    private const string FADE_IN_LOAD = "SceneChangeFadeIn";

    private Animation mAnim;

    // 페이드 아웃이 끝난뒤에 실행 할 함수가 있는지?
    private System.Action mCbFadeEndCallback;

    public bool isFading {
        get {
            return mAnim.isPlaying;
        }
    }

    protected override void initVariables()
    {
        base.initVariables();

        mAnim = Utils.getComponent<Animation>(trf);
    }

    public void SCENE_FADE_OUT(System.Action cb = null)
    {
        mAnim.Play(FADE_OUT_LOAD);
        SoundManager.inst.playUISound(UISound.SceneChangeEffect.ToString());
    }

    public void loadSceneFade()
    {
        mAnim.Play(FADE_IN_LOAD);
    }

    /// <summary>
    /// FadeIn -> 까맣게 변한뒤에 씬을 로드함
    /// </summary>
    public void endCbAnimation()
    {
        SceneController.inst.loadScene();
    }

    /// <summary>
    /// FadeOut -> 까맣에서 하얗게 되면 끝난뒤에 함수체크를 해서 실행시킴
    /// </summary>
    public void endCbFadeOut()
    {
        if(mCbFadeEndCallback != null)
        {
            mCbFadeEndCallback();
            mCbFadeEndCallback = null;
        }
    }
}
