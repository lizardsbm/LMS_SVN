using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로컬라이즈를 익스펙터에 붙여서 사용할 애
/// </summary>
public class LocalizeInvalidator : BaseBehaviour
{
    // LocalizeKey값
    public string key;

    private CustomText mText;

    protected override void initVariables() {
        base.initVariables();

        if (mText == null) {
            mText = Utils.getComponent<CustomText>(trf);
        }

        mText.text = Localize.Get(key);
    }

    public void OnEnable() {

        if (isInitialized) {
            return;
        }

        isInitialized = true;

        if (mText == null) {
            mText = Utils.getComponent<CustomText>(trf);
        }

        mText.text = Localize.Get(key);
    }

    
}
