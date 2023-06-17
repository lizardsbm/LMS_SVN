using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseJsonConvert
{
    /// <summary>
    /// 캐릭터 데이터를 Json형식으로 반환
    /// </summary>
    /// <returns></returns>
    public string getJsonData() {
        return JsonUtility.ToJson(this);
    }
}
