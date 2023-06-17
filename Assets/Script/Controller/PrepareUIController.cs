using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareUIController : BaseBehaviour
{
    // 2초 더미 로드
    private const float LOAD_TIME = 2.0f;

    private const string MENU = "Menu";

    // 로딩 슬라이더
    private Slider mLoadSlider;

    private float time = 0;

    public GameObject mBtnStart;
    [SerializeField] private GameObject mSpeechBox; 
    protected override void initVariables() {
        base.initVariables();
        mLoadSlider = Utils.getChild<Slider>(trf, "LoadingBar");
        loadStart();
    }

    public void loadStart() {
        StartCoroutine(coLoadStart());
    }

    private IEnumerator coLoadStart() {

        while(time < LOAD_TIME) {

            time += Time.deltaTime;
            mLoadSlider.value = time / LOAD_TIME;
            yield return null;
        }

        onCompleteLoad();
    }

    private void onCompleteLoad() {
        mBtnStart.SetActive(true);
        mSpeechBox.SetActive(false);
    }

    public void startGame() {
        SceneController.inst.startSceneLoad(SceneController.MENU);
    }
}
