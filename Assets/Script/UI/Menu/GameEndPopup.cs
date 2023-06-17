using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 종료 팝업
/// </summary>
public class GameEndPopup : BaseUIController
{
    public override bool onBackPressed() {
        return base.onBackPressed();
    }

    public void onClickAnswer(bool isOk) {
        if (isOk) {
            Application.Quit();
        } else {
            hide();
        }
    }
}
