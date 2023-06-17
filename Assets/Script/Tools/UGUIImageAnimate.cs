using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUIImageAnimate : BaseBehaviour
{
    public float Anim_Frame = 0;

    public Sprite[] mSprAnimImage;

    private Image mImgMainImage;

    // 주기`
    private float duration;

    private int ImgFrame = 0;

    protected override void initVariables() {
        base.initVariables();

        mImgMainImage = Utils.getComponent<Image>(trf);
        mImgMainImage.sprite = mSprAnimImage[ImgFrame];
        duration = 0;
        ImgFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;

        if(duration > Anim_Frame) {
            ImgFrame++;

            if (ImgFrame >= mSprAnimImage.Length) {
                ImgFrame = 0;
            }
            duration = 0;

            mImgMainImage.sprite = mSprAnimImage[ImgFrame];
        }
    }
}
