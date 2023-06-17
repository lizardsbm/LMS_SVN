using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPanel : AchievementPanel
{
    private PanelPopup popup;
    protected override void initVariables()
    {
        base.initVariables();
        DataManager.inst.mAchievementsDataManager.mLstPattern[0] = true;
        panelData = DataManager.inst.mAchievementsDataManager.mLstPattern;
        panelName = Achieve_Panel.Pattern;
        spritePath = ResPath.ACHIEVEMENT + panelName;

        setAchievementItem();
    }

    //transform.parent 초기화 이후 팝업 생성;
    protected override void Start()
    {
        popup = Utils.createObject(ResPath.POPUP + "PatternPopup", Utils.findInParents<AchievementPopup>(rectTrf).rectTrf).GetComponent<PanelPopup>();
        popup.hide();
    }

    protected override void setButtonEvent(AchievementItem item)
    {
        item.button.onClick.AddListener(() => { popup.show(); });


    }
}
