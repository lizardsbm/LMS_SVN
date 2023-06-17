using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 내 방 꾸미기 및 메뉴화면에서 해당 영역을 클릭했을때 제어 역할을 한다
/// </summary>
public class MyRoomController : BaseBehaviour{

    // 내 방 꾸미기 카메라
    private MyRoomCam mMyRoomCamera;

    protected override void initVariables() {
        base.initVariables();
        mMyRoomCamera = Utils.getChild<MyRoomCam>(trf, "MyRoomCam");
    }

    public void setMyRoomInfo() {

    }

    private void Update() {

        if (Input.GetKeyUp(KeyCode.A)) {
            mMyRoomCamera.extrudeCamView(true);
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            mMyRoomCamera.extrudeCamView(false);
        }
    }

}
