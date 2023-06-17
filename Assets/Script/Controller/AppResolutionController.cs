using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 앱을 실행했을때 비율에따라서 인게임 및 UI 의 비율을 설정해준다.
/// </summary>
public class AppResolutionController : BaseBehaviour
{
    private float RESOL_REAL {
        get {
            return ((float)Screen.width / (float)Screen.height);
        }
    }

    // 기본 베이스 비율 16:9의 비율
    private float RESOL_BASE {
        get {
            return BASED_WIDTH / BASED_HEIGHT;
        }
    }

    private const int BASED_WIDTH = 1920;

    private const int BASED_HEIGHT = 1080;

    protected override void initVariables() {
        base.initVariables();

    }

    /// <summary>
    /// 인게임에서 사용하고있는 2D 스프라이트 객체를 보여주는 뷰와 sprite 의 정보를 세팅한다.
    /// 생각에는 2D 스프라이트의 전체 Scale 과 cam 의 Size 값을 조정하면 되지 않을까..9
    /// cam은 size Height / 2고정.
    /// </summary>
    /// <param name="cam"></param>
    private void set2DSprite(ref Transform stageTrf) {

    }

    /// <summary>
    /// 필요한 화면 대비 스케일을 가져온다.
    /// </summary>
    /// <returns></returns>
    private float getApplyResol() {

        if (RESOL_BASE <= RESOL_REAL) {
            return RESOL_REAL / (RESOL_BASE + (RESOL_REAL - RESOL_BASE));
        } else {
            return RESOL_REAL / RESOL_BASE;
        }
    }

}
