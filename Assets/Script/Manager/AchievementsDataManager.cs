using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업적에서 쓰일 데이터 관리 클래스
/// </summary>
public class AchievementsDataManager : BaseJsonConvert
{
    public List<bool> mLstPattern = new List<bool>();
    public List<bool> mLstFurniture = new List<bool>();
    public List<bool> mLstIllust = new List<bool>();

    public AchievementsDataManager() {
        initData();
    }

    private void initData() {

        int enumCount;

        enumCount = Enum.GetNames(typeof(Achieve_Mouse_Pattern)).Length;

        mLstPattern.Clear();

        for (int i = 0; i < enumCount-1; ++i) {
            mLstPattern.Add(false);
        }

        enumCount = Enum.GetNames(typeof(Achieve_Furniture)).Length;

        mLstFurniture.Clear();

        for (int i = 0; i < enumCount - 1; ++i) {

            mLstFurniture.Add(false);
        }

        enumCount = Enum.GetNames(typeof(Achieve_Illust)).Length;

        mLstIllust.Clear();

        for (int i = 0; i < enumCount - 1; ++i) {

            mLstIllust.Add(false);
        }
    }
}
