using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePanel : AchievementPanel
{
    protected override void initVariables()
    {
        base.initVariables();
        DataManager.inst.mAchievementsDataManager.mLstFurniture[0] = true;
        panelData = DataManager.inst.mAchievementsDataManager.mLstFurniture;
        panelName = Achieve_Panel.Furniture;
        spritePath = ResPath.ACHIEVEMENT + panelName;
        setAchievementItem();
    }
    protected override void setButtonEvent(AchievementItem item)
    {
    }
}
