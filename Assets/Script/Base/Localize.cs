using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 다국어 지원을 위한 로컬라이즈 기본 스크립트
/// 일단은 한국,영어만 만들어서 쓰자.
/// </summary>
public class Localize 
{
    private static Dictionary<string, List<string>> mDicLocalizeData = new Dictionary<string, List<string>>();

    // 로컬라이즈 인덱스
    public static int lcoalizeIdx;

    public static void parse(TextAsset asset) {
        // TODO 엑셀 데이터 읽어들어 와서
        // 첫번쨰 인덱스는 문자의 키, 그 이후 리스트를 만들어서 0 한글, 1 영어 로 넣자.

        string text = asset.text.Replace("\r\n", "\n");
        string[] lines = text.Split('\n');

        List<string> tempList;

        string[] tokens;

        string key;

        for (int i = 0; i < lines.Length; ++i) {

            tempList = new List<string>();
            key = null;

            tokens = lines[i].Split(BaseCsv.DELIMITER);

            for(int k = 0; k < tokens.Length; ++k) {
                if(k == 0) {
                    key = tokens[k];
                } else {
                    tempList.Add(tokens[k]);
                }
            }

            mDicLocalizeData.Add(key, tempList);
        }
    }

    public static string Get(string key) {
        return mDicLocalizeData[key][lcoalizeIdx];
    }

    public static string Format(string format, object arg0) {
        string formatKey = string.Format(format, arg0);
        return mDicLocalizeData[formatKey][lcoalizeIdx];
    }
}
