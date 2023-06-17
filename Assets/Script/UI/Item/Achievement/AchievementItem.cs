using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AchievementItem : BaseBehaviour
{
    public Image image { get; set; }
    public Image backGround { get; set; }
    public Button button { get; set; }
    protected override void initVariables()
    {
        base.initVariables();
        image = Utils.getChild<Image>(trf, "itemImage");
        backGround = Utils.getChild<Image>(trf, "backGround");
        button = GetComponent<Button>();

    }
}
