using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AchievementPanel : BaseBehaviour
{
    protected List<bool> panelData;
    protected string itemPath = ResPath.UI_ITEM + "Achievement/";
    protected string spritePath;
    public Achieve_Panel panelName { get; set; }

    //업적 이미지 생성
    protected void setAchievementItem()
    {
        for (int i = 0; i < 20; i++)
        {
            Utils.createObject(itemPath + panelName, trf);
        }

        AchievementItem[] items = new AchievementItem[panelData.Count];
        items = trf.GetComponentsInChildren<AchievementItem>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(spritePath);
        Sprite emptySpr = Resources.Load<Sprite>(ResPath.ACHIEVEMENT + panelName + "Frame");

        for (int i = 0; i < panelData.Count; i++)
        {
            if (panelData[i])
            {
                items[i].image.sprite = sprites[i];
                setButtonEvent(items[i]);
            }
            items[i].backGround.sprite = emptySpr;
        }
    }

    protected abstract void setButtonEvent(AchievementItem item);
}