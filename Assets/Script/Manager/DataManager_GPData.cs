using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저 게임 플레이 데이터 관련 제어할 파셜 클래스
/// </summary>
public partial class DataManager : BaseSingleton<DataManager>
{
    public void initGPData() {
        mGPData = new GamePlayData();
    }

    public List<int> getPrereItem(int index) {
        return mGPData.getPrereItem(index);
    }

    public void addPrereItem(int index, int itemIndex) {
        mGPData.addPrereItem(index, itemIndex);
    }

    public void removePrereItem(int index, int itemIndex) {
        mGPData.removePrereItem(index, itemIndex);
    }
}
