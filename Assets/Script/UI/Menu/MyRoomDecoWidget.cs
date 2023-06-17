using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomDecoWidget : BaseBehaviour
{
    protected override void initVariables() {
        base.initVariables();

    }

    /// <summary>
    /// 버튼 종류에 따른 이벤트 
    /// </summary>
    /// <param name="kind"></param>
    public void clickButton(string kind) {
        switch (kind) {
            case "Close":
                hide();
                break;
        }
    }
}
