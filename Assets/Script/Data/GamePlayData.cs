using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 게임플레이에 관여된 데이터를 관리 및 가져올 예정
/// </summary>
public class GamePlayData
{
    private const int MAX_STATE_COUNT = 12;

    public int IntState;
    public int StrState;

    public Dictionary<int, List<int>> mPrereGetItem = new Dictionary<int, List<int>>();

    public GamePlayData() {
        initGamePlayData();
    }

    /// <summary>
    /// 임시로 미리 얻는 아이템에 대한 정보를 넣음
    /// </summary>
    public void initGamePlayData() {

        List<int> prereGetItems;

        for (int i = 0; i < mPrereGetItem.Count; ++i) {
            prereGetItems = new List<int>();
            mPrereGetItem.Add(i, prereGetItems);
        }
    }

    public List<int> getPrereItem(int index) {
        index--;

        if (mPrereGetItem.Count <= index) {
            Log.e("Not Exist PrereGetItem Data");
            return null;
        }

        return mPrereGetItem[index];
    }

    public void addPrereItem(int index, int itemIndex) {
        index--;
        mPrereGetItem[index].Add(itemIndex);
    }
    
    public void removePrereItem(int index, int itemIndex) {
        mPrereGetItem[index].Remove(itemIndex);
    }
}
