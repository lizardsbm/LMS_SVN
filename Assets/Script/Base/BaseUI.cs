using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임 오브젝트나 그런게 아니라 UI에서 기본적으로 있으면 좋을법한 함수인경우 해당 스크립트를 상속받아보자..
/// </summary>
public class BaseUI : BaseBehaviour
{

    // 프리팹으로 생성하는 경우 오프셋의 사이즈를 최대로 보이게 하기위해서 호출
    public virtual void setFullSize()
    {
        rectTrf.offsetMin = Vector2.zero;
        rectTrf.offsetMax = Vector2.zero;
    }
}
