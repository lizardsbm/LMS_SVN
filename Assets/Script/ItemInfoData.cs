using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리 아이템 데이터 번들
/// </summary>
public class ItemDataBundle
{

    public int condition;

    public ItemData[] itemData;

    public ItemDataBundle(int condition, int scriptCnt) {
        this.condition = condition;
        itemData = new ItemData[scriptCnt];
    }
}

/// <summary>
/// 인벤토리 아이템 데이터 구성요소
/// </summary>
public class ItemData
{

    public int itemIdx; // 아이템 인덱스
    public int[] prerequisites; // 선행조건
    public List<int[]> combineIdx = new List<int[]>(); // 조합 가능한 인덱스
    public List<int[]> addCondition = new List<int[]>(); // 아이템 사용시 충족시켜줄 선행 조건
    public List<int> combineScript = new List<int>(); // 조합 했을때 나올 스크립트
    public List<int> returnScript = new List<int>(); // 아이템 설명 스크립트
    public List<int> devideScript = new List<int>(); // 분해 했을떄 나올 스크립ㅌ르
    public string path;
    public string koreanName;

    public void setPrerequisites(List<int> lst) {
        prerequisites = new int[lst.Count];

        for (int i = 0; i < lst.Count; ++i) {
            prerequisites[i] = lst[i];
        }
    }

    public void setCombineIdx(List<int> lst) {
        int[] tempArray = new int[lst.Count];

        for (int i = 0; i < lst.Count; ++i) {
            tempArray[i] = lst[i];
        }

        combineIdx.Add(tempArray);
    }

    public void setAddConditionIdx(List<int> lst) {
        int[] tempArray = new int[lst.Count];

        for (int i = 0; i < lst.Count; ++i) {
            tempArray[i] = lst[i];
        }

        addCondition.Add(tempArray);
    }
}

/// <summary>
/// 인벤토리 아이템 데이터 리스트
/// </summary>
public class ItemDataList
{

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<ItemDataBundle> lstData = new List<ItemDataBundle>();

    // 외부에서 가져올수 있도록
    public List<ItemDataBundle> mLstItemsinfo {
        get {
            return lstData;
        }
    }

    public void clearList() {
        lstData.Clear();
    }

    public void parse(TextAsset asset) {

        //
        lstData.Clear();

        string text = asset.text.Replace("\r\n", "\n");
        string[] lines = text.Split('\n');

        string[] tokens;
        string[] subTokens;
        string[] andTokens;
        List<int> tempAndList = new List<int>();

        int ptr;
        int subptr;
        ItemData data;

        List<ItemData> temp = new List<ItemData>();
        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionOld = 0;
        int conditionNew = 0;

        //
        for (int i = 0; i < lines.Length; ++i) {

            if (string.IsNullOrEmpty(lines[i])) {
                continue;
            }

            ptr = -1;
            subptr = -1;
            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);

            data = new ItemData();
            data.itemIdx = Utils.toInt32(tokens[++ptr]);

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);
            tempAndList.Clear();

            for (int k = 0; k < subTokens.Length; ++k) {
                tempAndList.Add(Utils.toInt32(subTokens[k]));
            }

            data.setPrerequisites(tempAndList);

            
            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            for (int k = 0; k < subTokens.Length; ++k) {

                tempAndList.Clear();

                subptr = -1;

                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_AND);

                for (int m = 0; m < andTokens.Length; ++m) {
                    tempAndList.Add(Utils.toInt32(andTokens[++subptr]));
                }
                data.setCombineIdx(tempAndList);
            }

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            for (int k = 0; k < subTokens.Length; ++k) {

                subptr = -1;

                tempAndList.Clear();

                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_AND);

                for (int m = 0; m < andTokens.Length; ++m) {
                    tempAndList.Add(Utils.toInt32(andTokens[++subptr]));
                }
                data.setAddConditionIdx(tempAndList);
            }

            tempAndList.Clear();
            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            subptr = -1;

            for (int k = 0; k < subTokens.Length; ++k) {
                data.combineScript.Add(Utils.toInt32(subTokens[++subptr]));
            }

            subptr = -1;

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);
            for (int k = 0; k < subTokens.Length; ++k) {
                data.returnScript.Add(Utils.toInt32(subTokens[++subptr]));
            }

            subptr = -1;

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);
            for (int k = 0; k < subTokens.Length; ++k) {
                data.devideScript.Add(Utils.toInt32(subTokens[++subptr]));
            }

            data.path = tokens[++ptr];

            data.koreanName = tokens[++ptr];

            //
            if (temp.Count == 0) {

                temp.Add(data);
                conditionOld = conditionNew;
            } else {

                if (conditionOld == conditionNew) {
                    temp.Add(data);

                } else {

                    //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
                    ItemDataBundle bundle = new ItemDataBundle(conditionOld, temp.Count);

                    for (int k = 0; k < bundle.itemData.Length; ++k) {
                        bundle.itemData[k] = temp[k];
                    }

                    lstData.Add(bundle);

                    conditionOld = conditionNew;
                    temp.Clear();
                    temp.Add(data);
                }
            }//eo if
        }//eo for

        if (temp.Count != 0) {
            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            ItemDataBundle bundle = new ItemDataBundle(conditionOld, temp.Count);

            for (int k = 0; k < bundle.itemData.Length; ++k) {
                bundle.itemData[k] = temp[k];
            }

            lstData.Add(bundle);
        }
    }
}//eo class
