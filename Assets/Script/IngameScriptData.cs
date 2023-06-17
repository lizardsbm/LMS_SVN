using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인게임 대사 번들
/// </summary>
public class IngameScriptDataBundle {

    public int condition;

    public List<IngameScriptData[]> ingameScriptData = new List<IngameScriptData[]>();

    public IngameScriptDataBundle(int condition) {
        this.condition = condition;
    }

    public void addData(IngameScriptData[] _data) {
        ingameScriptData.Add(_data);
    }
}

/// <summary>
/// 인게임 대사 구성요소
/// </summary>
public class IngameScriptData {

    public int order;
    public int scriptIdx;
    public string textKr;
    public List<string> scriptEvent = new List<string>();
    public List<List<string>> eventIdx = new List<List<string>>();
}

/// <summary>
/// 인게임 대사 리스트
/// </summary>
public class IngameScriptDataList {

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<IngameScriptDataBundle> lstData = new List<IngameScriptDataBundle>();

    // 외부에서 가져올수 있도록
    public List<IngameScriptDataBundle> mLstIngameScriptInfo {
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

        int ptr;

        IngameScriptData data;

        List<IngameScriptData> temp = new List<IngameScriptData>();
        IngameScriptData[] tempData;
        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionOld = 0;
        int conditionNew = 0;

        int scriptIdxOld = 0;
        int scriptIdxNew = 0;

        IngameScriptDataBundle bundle = null;

        //
        for(int i = 0; i < lines.Length; ++i) {

            if(string.IsNullOrEmpty(lines[i])) {
                continue;
            }

            ptr = -1;
            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);
            scriptIdxNew = Utils.toInt32(tokens[++ptr]);

            if(conditionOld != conditionNew) {

                tempData = new IngameScriptData[temp.Count];

                // 오더 순서대로 정렬함
                temp.Sort((IngameScriptData left, IngameScriptData right) => {

                    if(left.order < right.order) {
                        return -1;
                    } else if(left.order == right.order) {
                        return 0;
                    } else {
                        return 1;
                    }
                });

                for(int k = 0; k < temp.Count; ++k) {
                    tempData[k] = temp[k];
                }

                bundle.addData(tempData);
                lstData.Add(bundle);
                temp.Clear();
            }

            data = new IngameScriptData();
            data.order = Utils.toInt32(tokens[++ptr]);
            data.scriptIdx = scriptIdxNew;
            data.textKr = tokens[++ptr];

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_AND);

            for(int k = 0; k < subTokens.Length; ++k) {

                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_SUB);

                for(int j = 0; j < andTokens.Length; ++j) {
                    data.scriptEvent.Add(andTokens[j]);
                }
            }

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_AND);

            List<string> tempEventList = new List<string>();

            for (int k = 0; k < subTokens.Length; ++k) {

                tempEventList = new List<string>();

                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_SUB);

                for (int j = 0; j < andTokens.Length; ++j) {
                    tempEventList.Add(andTokens[j]);
                }
                data.eventIdx.Add(tempEventList);
            }


            //
            if(temp.Count == 0) {

                temp.Add(data);
                conditionOld = conditionNew;
                scriptIdxOld = scriptIdxNew;
                bundle = new IngameScriptDataBundle(conditionOld);

            } else {

                if(conditionOld == conditionNew && scriptIdxNew == scriptIdxOld) {
                    temp.Add(data);

                } else if(conditionOld == conditionNew && scriptIdxNew != scriptIdxOld) {
                    
                    tempData = new IngameScriptData[temp.Count];

                    // 오더 순서대로 정렬함
                    temp.Sort((IngameScriptData left, IngameScriptData right) => {

                        if(left.order < right.order) {
                            return -1;
                        } else if(left.order == right.order) {
                            return 0;
                        } else {
                            return 1;
                        }
                    });

                    for(int k = 0; k < temp.Count; ++k) {
                        tempData[k] = temp[k];
                    }

                    bundle.addData(tempData);
                    temp.Clear();
                    temp.Add(data);
                } else {
                    lstData.Add(bundle);

                    temp.Clear();
                    temp.Add(data);
                }

                scriptIdxOld = scriptIdxNew;
                conditionOld = conditionNew;
            }
        }//eo for

        if(temp.Count != 0) {
            
            tempData = new IngameScriptData[temp.Count];

            // 오더 순서대로 정렬함
            temp.Sort((IngameScriptData left, IngameScriptData right) => {

                if(left.order < right.order) {
                    return -1;
                } else if(left.order == right.order) {
                    return 0;
                } else {
                    return 1;
                }
            });

            for(int k = 0; k < temp.Count; ++k) {
                tempData[k] = temp[k];
            }

            bundle.addData(tempData);
            lstData.Add(bundle);
        }
    }
}//eo class
