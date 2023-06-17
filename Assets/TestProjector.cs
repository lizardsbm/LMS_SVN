using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjector : BaseBehaviour
{
    private const float ROTE_Z = 30.25f;

    protected override void initVariables() {
        base.initVariables();

    }

    // 업데이트에서 일정시간마다 돌려...주자
    private void Update() {
        // 테스트
        trf.Rotate(0, 0, ROTE_Z * Time.deltaTime);

    }

}
