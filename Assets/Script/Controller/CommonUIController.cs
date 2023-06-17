using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI의 루트로 전체적인 UI를 제어할 클래스
/// </summary>
public class CommonUIController : BaseBehaviour
{
    private static CommonUIController mInst;

    public static CommonUIController inst {
        get {
            if(mInst == null) {
                
                GameObject obj = GameObject.Find("CommonUI");

                if(obj == null) {
                    obj = Utils.createObject(ResPath.COMMON_UI+ "CommonUI", null, "CommonUI");
                }

                mInst = obj.GetComponent<CommonUIController>();
            }

            return mInst;
        }
    }

    // 환경설정 팝업
    private SettingPopup mSettingPopup;

    public void init() {
        obj.name = "CommonUIController";
        createCommonUI();
    }

    private void createCommonUI() {
        GameObject go;
        string path;

        path = ResPath.COMMON_UI + "SettingPopup";
        go = Utils.createObject(path, trf, "SettingPopup");

        mSettingPopup = Utils.getComponent<SettingPopup>(go);
        mSettingPopup.initMemberVariables();
        mSettingPopup.setFullSize();
        mSettingPopup.hide();
    }

    /// <summary>
    /// 환경설정 팝업을 띄움
    /// </summary>
    public void showSettingPopup()
    {
        mSettingPopup.show();
    }
}
