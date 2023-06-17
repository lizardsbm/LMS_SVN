using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPopup : BaseBehaviour
{
    // 캐릭터 이미지
    private Image mImgCharacter;

    // 클리어 했을떄 하단부에 나올 텍스트
    private CustomText mTextClear;

    protected override void initVariables()
    {
        base.initVariables();
        mTextClear = Utils.getChild<CustomText>(trf, "sprBg/SwipePanel/Contents/ResultPage/SpeechBubble/TextResult");
    }

    public void setClearText(string text) {
        mTextClear.text = text;
    }

    public void onClickEvent(GameObject obj)
    {
        switch (obj.name)
        {
            case "BtnContinue":
                GameSettingMgr.inst.mPlayState = GamePlayStates.Clear;
                SceneController.inst.startSceneLoad(SceneController.STORY);
                break;

            case "BtnReturnMenu":
                GameSettingMgr.inst.mPlayState = GamePlayStates.Clear;
                SceneController.inst.startSceneLoad(SceneController.MENU);
                break;
        }
    }
}
