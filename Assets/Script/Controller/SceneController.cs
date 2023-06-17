using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 진입점에서 컨트롤 할 녀석
/// </summary>
public partial class SceneController : BaseSingleton<SceneController>
{
    public const string BACK_METHOD = "onBackPressed";

    // 사전준비 화면 및 타이틀
    public const string PREPARE = "Prepare";
#if KHAM
    public const string MENU = "Menu_Backup";
#else
    public const string MENU = "Menu";
#endif

    public const string INGAME = "Ingame";

    public const string STORY = "Story";

    public const string EXTERN_TEST = "GooglePlayConnect";

    // 다음 로드할 씬 이름
    private string willLoadSceneName;
    private SceneChangeController mSceneChange;

    // 씬을 이동했을떄 실행할 함수가 있는지.
    public FunKind willExecuteFun;

    //back 이벤트를 전달받을 UI 오브젝트.
    public static BaseUIController uiController;

    /// <summary>
    /// 페이드인/아웃 중인가(씬전환중인지)
    /// </summary>
    public bool isFading
    {
        get
        {
            return mSceneChange.isFading;
        }
    }

    protected override void initVariables()
    {
        t = GetComponent<SceneController>();
        base.initVariables();

        if (mSceneChange == null)
        {
            GameObject SceneChangePanel = GameObject.Find("SceneChangeCanvas");

            if (SceneChangePanel != null)
            {
                mSceneChange = SceneChangePanel.GetComponent<SceneChangeController>();
            }
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case PREPARE:

                // 얘는 지우지않고 쭉 쓰도록 하자
                DontDestroyOnLoad(mSceneChange);
                mSceneChange.SCENE_FADE_OUT();
                loadLocalize();
                createPrepare();
                break;

            case MENU:
                mSceneChange.SCENE_FADE_OUT();
                SoundManager.inst.playBGM(BGMSound.Main_BGM.ToString());
                break;

            case STORY:
                mSceneChange.SCENE_FADE_OUT();
                break;

            case INGAME:
                mSceneChange.SCENE_FADE_OUT();
                break;

            case EXTERN_TEST:
                createPrepare();
                break;
        }
    }

    public void startSceneLoad(string sceneName)
    {
        willLoadSceneName = sceneName;
        mSceneChange.loadSceneFade();
    }

    public void loadScene()
    {
        SceneManager.LoadSceneAsync(willLoadSceneName);
    }

    /// <summary>
    /// 사운드 재생, 게임정보 등등 사용할 싱글톤 객체를 만들어둠
    /// </summary>
    private void createPrepare()
    {
        GameSettingMgr.inst.init();
        PrerequisitesManager.inst.init();
        CommonUIController.inst.init();
        DataManager.inst.init();
        FirebaseManager.inst.init();
        SoundManager.inst.init();

        DontDestroyOnLoad(CommonUIController.inst);
        DontDestroyOnLoad(GameSettingMgr.inst);
        DontDestroyOnLoad(PrerequisitesManager.inst);
        DontDestroyOnLoad(SoundManager.inst);
        DontDestroyOnLoad(DataManager.inst);
        DontDestroyOnLoad(FirebaseManager.inst);
    }

    private void loadLocalize()
    {
        TextAsset text = Resources.Load<TextAsset>(ResPath.BASE_CSV + "Localize");
        Localize.lcoalizeIdx = 0;
        Localize.parse(text);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(PlayerPrefs.GetString("ExceptionQuit"));
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            uiController.SendMessage(BACK_METHOD);
        }
    }

    /// <summary>
    /// 메뉴와 인게임에서 공통적으로 사용할 UI를 만듬
    /// </summary>
    private void createCommonUI()
    {

    }

    //onBackPressed와 비슷하지만 뷰단위에 이벤트 진행을 묻지않고 걍 빽함
    public static void goBackForced()
    {
        if (uiController != null)
        {
            uiController.onBackPressed();
        }
    }
}
