using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : BaseUI
{
    private Vector2 EffectRight = new Vector2(12, -34);
    private Vector2 EffectLeft = new Vector2(-42, -34);

    private Vector2 BGMRight = new Vector2(269, -34);
    private Vector2 BGMLeft = new Vector2(326, -34);

    private Sprite mSprBgmToggleOn;
    private Sprite mSprBgmToggleOff;

    private Animation mEffectToggle;

    private GameObject mObjCredit;

    private Button mBtnEffectSound;
    private Button mBtnBGM;

    private Image mBgmBg;
    private Image mEffectBg;

    private RectTweenPosition mTweenEffectSound;
    private RectTweenPosition mTweenBGM;
    private void OnEnable()
    {

        base.initVariables();

        mBgmBg = Utils.getChild<Image>(trf, "BgmBg");
        mEffectBg = Utils.getChild<Image>(trf, "EffectBg");

        mObjCredit = Utils.getChild(trf, "AntCreditPanel").gameObject;
        loadButtonSprite();

        mBtnEffectSound = Utils.getChild<Button>(trf, "BtnEffect");
        mBtnBGM = Utils.getChild<Button>(trf, "BtnBGM");

        mTweenEffectSound = Utils.getComponent<RectTweenPosition>(mBtnEffectSound);
        mTweenBGM = Utils.getComponent<RectTweenPosition>(mBtnBGM);

        mTweenEffectSound.addEvent(setEffectSoundToggle);
        mTweenBGM.addEvent(setBGMToggle);

    }

    public override void show()
    {
        base.show();

        setImgEffect();
        setImgBGM();
    }

    private void loadButtonSprite()
    {
        string path;

        path = ResPath.COMMON_SPRITE + "Btn_On_Bg";
        mSprBgmToggleOn = Utils.loadRes<Sprite>(path);

        path = ResPath.COMMON_SPRITE + "Btn_Off_Bg";
        mSprBgmToggleOff = Utils.loadRes<Sprite>(path);
    }

    public void onClickEvent(GameObject obj)
    {

        SoundManager.inst.playUISound(UISound.UI_Click_Button.ToString());

        switch (obj.name)
        {
            case "BtnEffect":
                mBtnEffectSound.interactable = false;
                mTweenEffectSound.play();
                DataManager.inst.mSoundData.isEffectMute = !DataManager.inst.mSoundData.isEffectMute;
                SoundManager.inst.setUIMute(DataManager.inst.mSoundData.isEffectMute);
                break;

            case "BtnBGM":
                mTweenBGM.play();
                mBtnBGM.interactable = false;
                DataManager.inst.mSoundData.isBGMMute = !DataManager.inst.mSoundData.isBGMMute;
                SoundManager.inst.setBGMMute(DataManager.inst.mSoundData.isBGMMute);
                break;
        }
    }


    public void showCredit()
    {
        Utils.setActive(mObjCredit, true);
    }

    public void hideCredit()
    {
        Utils.setActive(mObjCredit, false);
    }

    private void setImgEffect()
    {
        if (DataManager.inst.mSoundData.isEffectMute)
        {
            mBtnEffectSound.image.rectTransform.setPos(EffectLeft);
            mEffectBg.sprite = mSprBgmToggleOff;

            mTweenEffectSound.from = EffectLeft;
            mTweenEffectSound.to = EffectRight;

        }
        else
        {
            mBtnEffectSound.image.rectTransform.setPos(EffectRight);
            mEffectBg.sprite = mSprBgmToggleOn;

            mTweenEffectSound.from = EffectRight;
            mTweenEffectSound.to = EffectLeft;
        }
    }

    private void setImgBGM()
    {
        if (DataManager.inst.mSoundData.isBGMMute)
        {
            mBtnBGM.image.rectTransform.setPos(BGMRight);
            mBgmBg.sprite = mSprBgmToggleOff;

            mTweenBGM.from = BGMRight;
            mTweenBGM.to = BGMLeft;

        }
        else
        {

            mBtnBGM.image.rectTransform.setPos(BGMLeft);
            mBgmBg.sprite = mSprBgmToggleOn;

            mTweenBGM.from = BGMLeft;
            mTweenBGM.to = BGMRight;
        }
    }

    /// <summary>
    /// 정보에 따른 이펙트 사운드의 정보를 바꾼다.
    /// </summary>
    private void setEffectSoundToggle()
    {

        mBtnEffectSound.interactable = true;

        if (DataManager.inst.mSoundData.isEffectMute)
        {
            mEffectBg.sprite = mSprBgmToggleOff;

            mTweenEffectSound.from = EffectLeft;
            mTweenEffectSound.to = EffectRight;

        }
        else
        {

            mEffectBg.sprite = mSprBgmToggleOn;

            mTweenEffectSound.from = EffectRight;
            mTweenEffectSound.to = EffectLeft;
        }
    }

    /// <summary>
    /// 정보에 따른 배경 사운드의 정보를 바꾼다.
    /// </summary>
    private void setBGMToggle()
    {

        mBtnBGM.interactable = true;

        if (DataManager.inst.mSoundData.isBGMMute)
        {
            mBgmBg.sprite = mSprBgmToggleOff;

            mTweenBGM.from = BGMRight;
            mTweenBGM.to = BGMLeft;

        }
        else
        {
            mBgmBg.sprite = mSprBgmToggleOn;

            mTweenBGM.from = BGMLeft;
            mTweenBGM.to = BGMRight;
        }
    }

}
