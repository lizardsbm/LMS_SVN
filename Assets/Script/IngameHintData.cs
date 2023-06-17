using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인게임 좌측 상단의 힌트 데이터를 번들화 시켜 시나리오 인덱스 별로 가져올 예정
/// </summary>
public class IngameHintBundle
{
    public int condition;

    public IngameHintData[] ingameHintData;

    public IngameHintBundle(int condition, int scriptCnt)
    {
        this.condition = condition;
        ingameHintData = new IngameHintData[scriptCnt];
    }
}

/// <summary>
/// 인게임 좌측 상단의 힌트 데이터 구성요소
/// </summary>
public class IngameHintData
{
    // 선행 조건
    public int prerequisites;

    // 타입
    public int types;

    public int values;

    public string scripts;
}

/// <summary>
/// 인게임 좌측 상단의 힌트 데이터 리스트
/// </summary>
public class IngameHintDataList
{

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<IngameHintBundle> lstData = new List<IngameHintBundle>();

    // 외부에서 가져올수 있도록
    public List<IngameHintBundle> mLstHintinfo {
        get {
            return lstData;
        }
    }

    public void parse(TextAsset asset)
    {

        //
        lstData.Clear();

        string text = asset.text.Replace("\r\n", "\n");
        string[] lines = text.Split('\n');

        string[] tokens;


        int ptr;
        IngameHintData data;

        List<IngameHintData> temp = new List<IngameHintData>();
        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionOld = 0;
        int conditionNew = 0;

        //
        for (int i = 0; i < lines.Length; ++i)
        {

            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }

            ptr = -1;
            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);

            data = new IngameHintData();

            data.prerequisites = Utils.toInt32(tokens[++ptr]);
            data.types = Utils.toInt32(tokens[++ptr]);
            data.values = Utils.toInt32(tokens[++ptr]);
            data.scripts = tokens[++ptr];

            //
            if (temp.Count == 0)
            {
                temp.Add(data);
                conditionOld = conditionNew;
            }
            else
            {

                if (conditionOld == conditionNew)
                {
                    temp.Add(data);

                }
                else
                {

                    //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
                    IngameHintBundle bundle = new IngameHintBundle(conditionOld, temp.Count);

                    for (int k = 0; k < bundle.ingameHintData.Length; ++k)
                    {
                        bundle.ingameHintData[k] = temp[k];
                    }

                    lstData.Add(bundle);

                    conditionOld = conditionNew;
                    temp.Clear();
                    temp.Add(data);
                }
            }//eo if
        }//eo for

        if (temp.Count != 0)
        {
            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            IngameHintBundle bundle = new IngameHintBundle(conditionOld, temp.Count);

            for (int k = 0; k < bundle.ingameHintData.Length; ++k)
            {
                bundle.ingameHintData[k] = temp[k];
            }

            lstData.Add(bundle);
        }
    }
}//eo class
