using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StoryScriptBundle {

    public int condition;

    // 퍼즐 진행 전이냐 후냐.
    public int type;

    // 첫번째 인덱스는 시나리오, 2번째 인덱스는 인게임 전인가 후인가.
    public StoryScript[] prevScripts;
    public StoryScript[] afterScripts;

    public StoryScriptBundle(int condition) {
        this.condition = condition;
    }

    public void setPrevScriptInfo(int scriptCnt) {
        prevScripts = new StoryScript[scriptCnt];
    }

    public void setNextScriptInfo(int scriptCnt) {
        afterScripts = new StoryScript[scriptCnt];
    }
}

public class StoryScript {

    public int order;
    public string characterName;
    public float charPos;
    public int displayType;
    public string emotion;
    public string name;
    public int focus;
    public List<string> lstEvent = new List<string>();
    public List<string> lstEventContent = new List<string>();

    public string textKr;
    public string textEn;

    public void toData() {
        Debug.Log("index = " + order);
        Debug.Log("charName = " + characterName);
        Debug.Log("charPos = " + charPos);
        Debug.Log("DType = " + displayType);
        Debug.Log("emo = " + emotion);
        Debug.Log("Name = " + name);
        Debug.Log("focus = " + focus);

        for(int i = 0; i < lstEvent.Count; ++i) {
            Debug.Log("Event " + i + lstEvent[i]);
        }

        for (int i = 0; i < lstEventContent.Count; ++i) {
            Debug.Log("Event " + i + lstEventContent[i]);
        }

        Debug.Log("Text = " + textKr);
    }
}

/// <summary>
/// 스토리 진행 스크립트 데이터
/// </summary>
public class StoryScriptDataList {

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<StoryScriptBundle> lstData = new List<StoryScriptBundle>();

    // 외부에서 가져올수 있도록
    public List<StoryScriptBundle> mLstScript {
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
        StoryScript data;

        int typeNew = 0;
        int typeOld = 0;

        // 이벤트 정보 임시 리스트
        List<string> tempStrList = new List<string>();

        List<StoryScript> temp = new List<StoryScript>();
        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionOld = 0;
        int conditionNew = 0;

        //
        for(int i = 0; i < lines.Length; ++i) {

            if(string.IsNullOrEmpty(lines[i])) {
                continue;
            }

            ptr = -1;
            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);
            typeNew = Utils.toInt32(tokens[++ptr]);

            data = new StoryScript();
            data.order = Utils.toInt32(tokens[++ptr]);
            data.characterName = tokens[++ptr];
            Utils.toFloat(tokens[++ptr], out data.charPos);
            data.displayType = Utils.toInt32(tokens[++ptr]);
            data.emotion = tokens[++ptr];
            data.name = tokens[++ptr];
            data.focus = Utils.toInt32(tokens[++ptr]);

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            for (int k = 0; k < subTokens.Length; ++k)
            {
                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_AND);

                for (int j = 0; j < andTokens.Length; ++j)
                {
                    data.lstEvent.Add(andTokens[j]);
                }
            }

            subTokens = tokens[++ptr].Split(BaseCsv.DELIMITER_SUB);

            for (int k = 0; k < subTokens.Length; ++k)
            {
                andTokens = subTokens[k].Split(BaseCsv.DELIMITER_AND);

                for (int j = 0; j < andTokens.Length; ++j)
                {
                    data.lstEventContent.Add(andTokens[j]);
                }
            }

            data.textKr = tokens[++ptr];
            data.textEn = tokens[++ptr];

            //
            if(temp.Count == 0) {

                temp.Add(data);
                conditionOld = conditionNew;
                typeOld = typeNew;
            } else {

                if(conditionOld == conditionNew && typeOld == typeNew) {
                    temp.Add(data);
                } else {

                    // 오더 순서대로 정렬함
                    temp.Sort((StoryScript left, StoryScript right) => {

                        if(left.order < right.order) {
                            return -1;
                        } else if(left.order == right.order) {
                            return 0;
                        } else {
                            return 1;
                        }
                    });

                    StoryScriptBundle bundle;

                    if (typeOld == 0) {
                        //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
                        bundle = new StoryScriptBundle(conditionOld);
                        bundle.setPrevScriptInfo(temp.Count);
                    } else {
                        bundle = lstData[conditionOld];
                        bundle.setNextScriptInfo(temp.Count);
                    }

                    if (typeOld == 0) {
                        for (int k = 0; k < temp.Count; ++k) {
                            bundle.prevScripts[k] = temp[k];
                        }
                    } else {
                        for (int k = 0; k < temp.Count; ++k) {
                            bundle.afterScripts[k] = temp[k];
                        }
                    }

                    if(typeOld == 0) {
                        lstData.Add(bundle);
                    }

                    conditionOld = conditionNew;
                    typeOld = typeNew;
                    temp.Clear();
                    temp.Add(data);
                }
            }//eo if
        }//eo for

        if(temp.Count != 0) {
            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            StoryScriptBundle bundle = new StoryScriptBundle(conditionOld);

            if (typeNew == 0) {
                for (int k = 0; k < temp.Count; ++k) {
                    bundle.prevScripts[k] = temp[k];
                }
            } else {

                bundle.setNextScriptInfo(temp.Count);

                for (int k = 0; k < temp.Count; ++k) {
                    bundle.afterScripts[k] = temp[k];
                }
            }

            lstData.Add(bundle);
        }
    }
}//eo class
