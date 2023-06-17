using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// 아이템 얻는때 나오는 위젯
/// </summary>
public class GetItemWidget : BaseBehaviour
{
    private TweenCombiner mTweenCombiner;

    private Image mImgItemImage;
    private CustomText mTextItemName;

    private UnityEvent endEvent;

    protected override void initVariables() {
        base.initVariables();

        mTweenCombiner = Utils.getComponent<TweenCombiner>(trf);
        mImgItemImage = Utils.getChild<Image>(trf, "ImgGetItemBg/ImgItem");
        mTextItemName = Utils.getChild<CustomText>(trf, "ImgGetItemBg/TextName");

        endEvent = new UnityEvent();

        endEvent.AddListener(hideWidget);
    }

    public void showGetItem(InventoryItem item) {
        mImgItemImage.sprite = item.pSprItem;
        mTextItemName.text = item.pSprItem.name;

        show();
        playForward();
    }

    public void playForward() {
        mTweenCombiner.play();
    }


    public void playReverse() {
        mTweenCombiner.addMainTweenEvent(0, endEvent);
        mTweenCombiner.playReverse();
    }

    private void hideWidget() {
        mTweenCombiner.removeEvent();
        hide();
    }
}
