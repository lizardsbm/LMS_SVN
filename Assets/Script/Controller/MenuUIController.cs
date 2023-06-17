using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메뉴 화면 제어할 UI 컨트롤러
/// </summary>
public class MenuUIController : BaseUIController
{
    public LeftMenuPanel mLeftPanel;
    public RightMenuPanel mRightPanel;
    public FrontMenuPanel mFrontPanel;
    public CommonMenuPanel mCommonPanel;

    public Button mInteriorButton;

    private Sprite mSprInteriorNormal;
    private Sprite mSprInteriorSelect;

    protected override void initVariables() {
        base.initVariables();
        mLeftPanel.init(onClickEvent);
        mFrontPanel.init(mRightPanel.GetComponent<PagingScrollViewController>());
        loadButtonSprite();
    }

    private void loadButtonSprite() {
        string path;

        path = ResPath.MENU_BUTTON + "Btn_Interier";
        mSprInteriorNormal = Utils.loadRes<Sprite>(path);

        path = ResPath.MENU_BUTTON + "Btn_InterierSelected";
        mSprInteriorSelect = Utils.loadRes<Sprite>(path);
    }

    public void onClickEvent(string clickEvent) {
        switch(clickEvent) {

            case "Achievements":
                mCommonPanel.showAchievementPopup();
                break;

            case "Map":
                showMap();
                break;

            case "Start":
                SceneController.inst.startSceneLoad(SceneController.INGAME);
                //mCommonPanel.showScenarioInfo();
                break;

            case "InteriorManage":
                if (mCommonPanel.mInteriorPanel.isPlayTween) {
                    return;
                }

                if (mCommonPanel.mInteriorPanel.isActive) {
                    hideDecoUI();
                    mInteriorButton.image.sprite = mSprInteriorNormal;
                } else {
                    showDecoUI();
                    mInteriorButton.image.sprite = mSprInteriorSelect;
                }
                break;

            case "showScenario":
                mCommonPanel.showScenarioInfo();
                break;

            case "showSetting":
                CommonUIController.inst.showSettingPopup();
                break;
        }
    }

    private void showMap() {
        mCommonPanel.showMap();
    }

    private void showDecoUI() {
        mCommonPanel.showDecoUI();
    }

    private void hideDecoUI() {
        mCommonPanel.hideDecoUI();
    }
}
