using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본적으로 방 배치에 사용할 스크립트
/// </summary>
public class BaseRoomProp : BaseBehaviour
{
    // 기본 방꾸미기 경로
    protected const string BASE_TEXTURE_PATH = "RoomTexture/";
    protected const string BASE_MESH_PATH = "RoomMesh/";

    // 기본 렌더를 가지고 있을 녀석
    protected Renderer mBaseRender;

    // 메쉬를 뭘로 넣을것인가.
    protected MeshFilter mBaseFilter;

    protected override void initVariables() {
        base.initVariables();
        mBaseRender = Utils.getComponent<Renderer>(trf);
        mBaseFilter = Utils.getComponent<MeshFilter>(trf);
    }

    public virtual void setType(PropType _type,string mesh) {

    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Q)) {
            mBaseFilter.mesh = Utils.loadRes<Mesh>(BASE_MESH_PATH + "tv");
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            mBaseRender.material.mainTexture = Utils.loadRes<Texture>(BASE_TEXTURE_PATH + "room_maya");
        }
    }
}
