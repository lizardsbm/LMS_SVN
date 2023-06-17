using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인게임 아이템 데이터 번들
/// </summary>
public class IngameItemDataBundle {

    public int scenario;

    public IngameItemData[] ingameScriptData;

    public IngameItemDataBundle(int scenario, int dataCnt) {
        this.scenario = scenario;
        ingameScriptData = new IngameItemData[dataCnt];
    }
}

/// <summary>
/// 인게임 아이템 데이터 구성요소
/// </summary>
public class IngameItemData {

    // 아이템 인덱스
    public int inagmeItemIdx;

    // 선행 조건
    public List<int[]> prerequisites = new List<int[]> ();

    // 리턴 스크립트
    public List<int> returnScript = new List<int>();

    public void setPrerequisites(List<int> lst)
    {
        int[] tempPre = new int[lst.Count];

        for (int i = 0; i < tempPre.Length; ++i)
        {
            tempPre[i] = lst[i];
        }

        prerequisites.Add(tempPre);
    }
}

/// <summary>
/// 아이템 데이터 리스트
/// </summary>
public class IngameItemDataList {

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<IngameItemDataBundle> lstData = new List<IngameItemDataBundle>();

    // 외부에서 가져올수 있도록
    public List<IngameItemDataBundle> mLstIngameItemInfo {
        get {
            return lstData;
        }
    }

    public void parse(TextAsset asset) {

        //
        lstData.Clear();

        string text = asset.text.Replace("\r\n", "\n");
        string[] lines = text.Split('\n');

        string[] tokens;
        string[] subTokens;
        string[] andTokens;

        List<int> tempAndList;
        int ptr;
        int subptr;
        IngameItemData data;

        List<IngameItemData> temp = new List<IngameItemData>();
        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionOld = 0;
        int conditionNew = 0;

        //
        for(int i = 0; i < lines.Length; ++i) {

            if(string.IsNullOrEmpty(lines[i])) {
                continue;
            }

            ptr = -1;
            subptr = -1;

            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);

            data = new IngameItemData();
            data.inagmeItemIdx = Utils.toInt32(tokens[++ptr]); // 아이템 인덱스

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            for(int k = 0; k < subTokens.Length; ++k) {

                tempAndList = new List<int>();

                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_AND);

                for(int j = 0; j < andTokens.Length; ++j) {
                    tempAndList.Add(Utils.toInt32(andTokens[j]));
                }

                data.setPrerequisites(tempAndList);
            }

            subptr = -1;

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);
            for(int k = 0; k < subTokens.Length; ++k) {
                data.returnScript.Add(Utils.toInt32(subTokens[++subptr]));
            }

            //
            if(temp.Count == 0) {

                temp.Add(data);
                conditionOld = conditionNew;
            } else {

                if(conditionOld == conditionNew) {
                    temp.Add(data);

                } else {


                    //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
                    IngameItemDataBundle bundle = new IngameItemDataBundle(conditionOld, temp.Count);

                    for(int k = 0; k < bundle.ingameScriptData.Length; ++k) {
                        bundle.ingameScriptData[k] = temp[k];
                    }

                    lstData.Add(bundle);

                    conditionOld = conditionNew;
                    temp.Clear();
                    temp.Add(data);
                }
            }//eo if
        }//eo for

        if(temp.Count != 0) {
            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            IngameItemDataBundle bundle = new IngameItemDataBundle(conditionOld, temp.Count);

            for(int k = 0; k < bundle.ingameScriptData.Length; ++k) {
                bundle.ingameScriptData[k] = temp[k];
            }

            lstData.Add(bundle);
        }
    }
}//eo class
