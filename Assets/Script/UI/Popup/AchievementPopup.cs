using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : BaseBehaviour
{
    private PanelPopup PatternPopup, IllustPopup;
    private AchievementPanel[] mPanels;
    private ScrollRect scrollRect;
    private Text[] mBtnTexts;
    protected override void initVariables()
    {
        base.initVariables();

        mBtnTexts = Utils.getChild(this, "Buttons").GetComponentsInChildren<Text>();
        mPanels = Resources.LoadAll<AchievementPanel>(ResPath.PANEL + "Achievement");
        scrollRect = GetComponentInChildren<ScrollRect>();

        //패널 생성 후 위치잡기
        for (int i = 0; i < mPanels.Length; i++)
        {

            Utils.createObject(mPanels[i].gameObject, scrollRect.transform.GetChild(0));
            mPanels[i] = Utils.getChild<AchievementPanel>(scrollRect.transform.GetChild(0), i);
        }
        this.hide();
    }
    public void OnEnable()
    {
        setScrollViewContent(Achieve_Panel.Pattern);
    }

    //선택한 패널 제외하고 끄기
    private void setScrollViewContent(Achieve_Panel panelName)
    {
        foreach (AchievementPanel panels in mPanels)
        {
            //패널과 같은 순서의 버튼 색 변경
            int index = Array.IndexOf(mPanels, panels);
            if (panelName == panels.panelName)
            {
                panels.show();
                scrollRect.content = panels.rectTrf;
                mBtnTexts[index].color = Color.black;
                continue;
            }
            mBtnTexts[index].color = Color.white;
            panels.hide();
        }
    }
    #region 클릭 이벤트
    public void onClickPatternBtn() => setScrollViewContent(Achieve_Panel.Pattern);

    public void onClickIllustBtn() => setScrollViewContent(Achieve_Panel.Illust);

    public void onClickFurnitureBtn() => setScrollViewContent(Achieve_Panel.Furniture);

    #endregion
    public override void show() => base.show();

}
