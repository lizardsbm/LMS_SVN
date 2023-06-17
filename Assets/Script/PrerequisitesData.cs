using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인게임 아이템 데이터 번들
/// </summary>
public class PrerequisitesDataBundle
{
    public int scenario;

    public int[] preIndex;

    public string[] strPre;

    public PrerequisitesDataBundle(int scenaIdx, List<int> mLstPre, List<string> mLstStr)
    {
        scenario = scenaIdx;

        preIndex = new int[mLstPre.Count];
        strPre = new string[mLstStr.Count];

        for (int i = 0; i < mLstPre.Count; ++i)
        {
            preIndex[i] = mLstPre[i];
            strPre[i] = mLstStr[i];
        }
    }

}

/// <summary>
/// 아이템 데이터 리스트
/// </summary>
public class PrerequisitesDataList
{

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<PrerequisitesDataBundle> lstData = new List<PrerequisitesDataBundle>();

    // 외부에서 가져올수 있도록
    public List<PrerequisitesDataBundle> mLstPrerequisitesInfo {
        get {
            return lstData;
        }
    }

    public void clearList() {
        lstData.Clear();
    }

    public void parse(TextAsset asset)
    {
        //
        lstData.Clear();

        string text = asset.text.Replace("\r\n", "\n");
        string[] lines = text.Split('\n');

        string[] tokens;

        List<int> tempAndList = new List<int>();
        List<string> tempStrList = new List<string>();
        int ptr;

        int preIdx = 0;
        string strPre = null;

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
            preIdx = Utils.toInt32(tokens[++ptr]);

            strPre = tokens[++ptr];

            if (tempAndList.Count == 0)
            {
                tempAndList.Add(preIdx);
                tempStrList.Add(strPre);

                conditionOld = conditionNew;
            }
            else
            {

                if (conditionOld == conditionNew)
                {
                    tempAndList.Add(preIdx);
                    tempStrList.Add(strPre);
                }
                else
                {
                    //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
                    PrerequisitesDataBundle bundle = new PrerequisitesDataBundle(conditionOld, tempAndList, tempStrList);

                    lstData.Add(bundle);

                    conditionOld = conditionNew;
                    tempAndList.Clear();
                    tempAndList.Add(preIdx);

                    tempStrList.Clear();
                    tempStrList.Add(strPre);
                }
            }//eo if
        }//eo for

        if (tempAndList.Count != 0)
        {
            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            PrerequisitesDataBundle bundle = new PrerequisitesDataBundle(conditionOld, tempAndList, tempStrList);
            lstData.Add(bundle);
        }
    }
}//eo class

