using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메뉴에서 왼쪽 패널을 담당할 녀석
/// </summary>
public class LeftMenuPanel : BaseBehaviour
{
    private System.Action<string> cbClickEvent;

    public void init(System.Action<string> cb) {
        cbClickEvent = cb;
    }

    public void clickEvent(string eventName) {
        if(cbClickEvent != null) {
            cbClickEvent(eventName);
        }
    }
}
