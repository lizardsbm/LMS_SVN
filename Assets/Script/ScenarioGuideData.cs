using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 메뉴 -> 인게임 진입 직전 시나리오 데이터 번들
/// </summary>
public class ScenarioGuideDataBundle
{

    public int scenario;

    public ScenarioGuideData ScenarioGuideData;

    public ScenarioGuideDataBundle(int scenario) {
        this.scenario = scenario;
    }
}

/// <summary>
/// 게임 진입 직전 데이터
/// </summary>
public class ScenarioGuideData
{
    // 에피소드 이름, 에피소드 내용
    public string episodeName;
    public string episodeContent;

    // 그냥 배경
    public string episodeBgPath;

    // 에피소드 내용의 배경
    public string episodeGameBgPath;

    // 에피소드 내용의 배경
    public string episodeContentBgPath;
}

/// <summary>
/// 아이템 데이터 리스트
/// </summary>
public class ScenarioGuideDataList
{

    //구조는 미정인게 많아서 대충대충. 나중에 확정되면 손볼예정
    private List<ScenarioGuideDataBundle> lstData = new List<ScenarioGuideDataBundle>();

    // 외부에서 가져올수 있도록
    public List<ScenarioGuideDataBundle> mLstIngameItemInfo {
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

        int ptr;
        ScenarioGuideData data;

        // 첫 비교해야할 스크립트 넘버가 1부터 시작하므로
        int conditionNew = 0;

        //
        for (int i = 0; i < lines.Length; ++i)
        {

            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }

            ptr = -1;
            data = new ScenarioGuideData();
            tokens = lines[i].Split(BaseCsv.DELIMITER);

            conditionNew = Utils.toInt32(tokens[++ptr]);
            data.episodeName = tokens[++ptr];
            data.episodeContent = tokens[++ptr];
            data.episodeBgPath = tokens[++ptr];
            data.episodeGameBgPath = tokens[++ptr];
            data.episodeContentBgPath  = tokens[++ptr];

            //스크립트 번호가 달라졌다면 새 스크립트이므로 쌓인 스크립트를 딕셔너리로
            ScenarioGuideDataBundle bundle = new ScenarioGuideDataBundle(conditionNew);

            bundle.ScenarioGuideData = data;

            lstData.Add(bundle);

        }
    }
}//eo class
