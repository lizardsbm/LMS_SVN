using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustPanel : AchievementPanel
{
    private PanelPopup popup;
    protected override void initVariables()
    {
        base.initVariables();
        DataManager.inst.mAchievementsDataManager.mLstIllust[0] = true;
        panelData = DataManager.inst.mAchievementsDataManager.mLstIllust;
        panelName = Achieve_Panel.Illust;
        spritePath = ResPath.ACHIEVEMENT + panelName;
        setAchievementItem();
    }
    protected override void Start()
    {
        popup = Utils.createObject(ResPath.POPUP + "IllustPopup", Utils.findInParents<AchievementPopup>(rectTrf).rectTrf).GetComponent<PanelPopup>();
        popup.hide();
    }

    protected override void setButtonEvent(AchievementItem item)
    {
        item.button.onClick.AddListener(() => { popup.show(); });
    }
}
