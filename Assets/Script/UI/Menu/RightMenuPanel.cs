using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메뉴에서 오른쪽 패널을 담당할 녀석
/// </summary>
public class RightMenuPanel : BaseBehaviour
{
    // 스와이프 자동으로 촤락 해주는애
    private PagingScrollViewController mPagingScrollViewController;

    private RomanceWidget mRomanceWidget;

    protected override void initVariables()
    {
        base.initVariables();
        mRomanceWidget = Utils.getChild<RomanceWidget>(trf, "Contents/RomanceWidget");
        mPagingScrollViewController = Utils.getComponent<PagingScrollViewController>(trf);
    }
}
