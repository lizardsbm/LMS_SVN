using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


/// <summary>^
/// 인게임에 들어갔을때의 시작점이 될 게임 매니저.
/// 각종 필요한 스크립트 데이터 등을 로드 한 뒤 필요한 곳으로 넘겨준다.
/// + 인게임 도중 게임이 종료되는 경우 예외 처리또한 하도록
/// </summary>
public partial class GameManager : BaseBehaviour
{
    public static int StageIndex {
        get {
            return GameSettingMgr.inst.currentSettingScenario;
        }
    }

    public static bool isBeforeEventType(string txt) {
        switch (txt) {
            case RESET_SCRIPT_PANEL_POS:
                return false;

            case DISAPPEAR_GET_ITEM:
                return false;

            default:
                return true;
        }
    }

    public const string SHOW_SUB_POPUP = "Show_Sub_Popup";
    public const string SHOW_CHAGNE_STATE = "ShowChangeCondition";

    public const string ENTER_INGAME_SCRIPT = "Enter_Ingame_Script";
    public const string ENABLE_INGAME_ITEM = "Enable_Ingame_Item";
    public const string DISABLE_INGAME_ITEM = "Disable_Ingame_Item";
    public const string GET_INVENTORY_ITEM = "Get_InventoryItem";
    public const string REMOVE_INVENTORY_ITEM = "Remove_InventoryItem";
    public const string ADD_CONDITION = "Add_Condition";
    public const string REMOVE_CONDITION = "Remove_Condition";
    public const string APPEAR_GET_ITEM = "AppearGetItem";
    public const string DISAPPEAR_GET_ITEM = "DisappearGetItem";
    public const string END_GAME = "End_Game";

    public const string CHANGE_Y_POSITION = "Change_Y_Position";
    public const string CHANGE_INGAME_ITME_POS = "Change_Ingame_ItemPos";
    public const string RESET_SCRIPT_PANEL_POS = "ResetScriptPanelPosition";

    private const string ADD_PREREITEM = "Add_PrereItem";
    private const string REMOVE_PREREITEM = "Remove_PrereItem";

    private const string PLAY_UI_SOUND = "PLAY_UI_SOUND";
    private const string PLAY_BGM_SOUND = "PLAY_BGM_SOUND";
    private const string PLAY_EAX_SOUND = "PLAY_EAX_SOUND";

    private const string STOP_BGM_SOUND = "STOP_BGM_SOUND";
    private const string STOP_EAX_SOUND = "STOP_EAX_SOUND";

    private const string CLEAR_STAGE = "CLEAR_STAGE";

    // 외부 UI와 통할 컴포넌트 할당
    public IngameUIController mUIController;

    // 스테이지 정보 컨트롤
    public StageController mStageController;

    // 인게임에서 쓰일 아이템 모음
    public IngameItemDataList mIngameDataList;
    public TextAsset _IngameItemInfoText;

    // 인게임에서 쓰일 대사 모음
    public IngameScriptDataList mIngameScriptList;
    public TextAsset _ingameScriptText;

    // 인게임 조합 아이템에서 쓰일 데이터 모음
    public ItemDataList mItemDataList;
    public TextAsset _ItemDataText;

    // 인게임 서브팝업을 나타내는데 사용
    public IngameSubPopupDataList mIngameSubPopupList;
    public TextAsset _ingameSubPopupText;

    // 선행 조건 데이터
    public PrerequisitesDataList mPreList;
    public TextAsset _prerequisitesText;

    // 인게임 좌측 상단의 힌트 데이터
    public IngameHintDataList mIngameHintDataList;
    public TextAsset _ingameHintText;

    /// <summary>
    /// 게임매니저 초기화시에 필요한 콜백이나 스크립트를 등록 할당함
    /// </summary>
    protected override void initVariables()
    {
        base.initVariables();

        IngameDataManager.inst.init();

        loadScript();
        loadPrerequisitesData();
        loadStatePrefab();

        mUIController.initMemberVariables();

        mUIController.init(mItemDataList.mLstItemsinfo[StageIndex], mIngameHintDataList.mLstHintinfo[StageIndex]);
        mUIController.initCallback(cbEndScript);

        checkCharacterStatus();
        checkEnterIngameEvent();
        checkGetPrereItem();
    }

    /// <summary>
    /// 선행조건 데이터 파싱
    /// </summary>
    private void loadPrerequisitesData()
    {
        mPreList = new PrerequisitesDataList();
        mPreList.parse(_prerequisitesText);
    }

    /// <summary>
    /// 스테이지 정보가 담긴 프리팹을 로드 및 할당한다.
    /// </summary>
    private void loadStatePrefab() {

        GameObject ingameParent = GameObject.Find("Ingame");
        string stageName = string.Format("{0}{1}", "Stage", StageIndex);

        GameObject go = Utils.createObject(string.Format("{0}{1}", ResPath.INGAME, stageName),ingameParent.transform,stageName);

        mStageController = Utils.getComponent<StageController>(go);

        mStageController.init(onClickIngameItem);
        mStageController.initStageState(mIngameDataList.mLstIngameItemInfo[StageIndex].ingameScriptData);
        mStageController.initPrerequisitesData(mPreList.mLstPrerequisitesInfo[StageIndex]);
    }   

    private void loadScript() {

        // 인게임 데이터 파싱
        mIngameDataList = new IngameItemDataList();
        mIngameDataList.parse(_IngameItemInfoText);

        // 인게임 대사집 파싱
        mIngameScriptList = new IngameScriptDataList();
        mIngameScriptList.parse(_ingameScriptText);

        IngameDataManager.inst.setIngameScriptData(mIngameScriptList.mLstIngameScriptInfo[StageIndex].ingameScriptData);

        // 인게임 조합 관련 아이템 파싱
        mItemDataList = new ItemDataList();
        mItemDataList.parse(_ItemDataText);

        mIngameSubPopupList = new IngameSubPopupDataList();
        mIngameSubPopupList.parse(_ingameSubPopupText);

        IngameDataManager.inst.loadItemSprite(mItemDataList);
        IngameDataManager.inst.loadSubPopupSprite(mIngameSubPopupList);

        // 인게임 가이드 관련 파싱
        mIngameHintDataList = new IngameHintDataList();
        mIngameHintDataList.parse(_ingameHintText);
    }

    /// <summary>
    /// 아이템을 눌렀을떄의 이벤트를 처리함
    /// </summary>
    /// <param name="item"></param>
    private void onClickIngameItem(IngameItem item) {

        // 선행조건 인덱스
        int preidx = 0;

        // 선행조건 우선적으로 할당
        for(int i = item.mPrerequisites.Count-1; i > 0 ; --i) {
            if(PrerequisitesManager.inst.isSatisfyPre(item.mPrerequisites[i])) {
                preidx = i;
                break;
            }
        }

        // 만일 아이템을 사용한 상태라면 
        if (mUIController.isUsingItem) {

            // 아이템 사용했는데 아무런 조건 충족이 되지 않았다면..
            if(preidx == 0) {
                showScript(IngameDataManager.inst.getFailUseItemScript());
            } else {
                showScript(item.mReturnScript[preidx]);
                mUIController.offItemSelect();
            }
        } else {
            showScript(item.mReturnScript[preidx]);
        }
    }

    /// <summary>
    /// 스테이지에 첫 진입했을때 체크할 정보를 체크하고 필요한 이벤트가 있다면 실행한다.
    /// 진입 이벤트의 비교는 가장 첫 컬럼의 정보로만 판단을 하도록 하자
    /// </summary>
    private void checkEnterIngameEvent()
    {
        IngameScriptData[] mTempListStartScript = null;

        for (int i = 0; i < mIngameScriptList.mLstIngameScriptInfo[StageIndex].ingameScriptData.Count; ++i)
        {
            if (mIngameScriptList.mLstIngameScriptInfo[StageIndex].ingameScriptData[i][0].scriptEvent[0] == ENTER_INGAME_SCRIPT)
            {
                mTempListStartScript = mIngameScriptList.mLstIngameScriptInfo[StageIndex].ingameScriptData[i];
            }
        }

        if(mTempListStartScript != null)
        {
            mUIController.setNextIngameScriptData(mTempListStartScript);
        }
    }

    /// <summary>
    /// 미리 얻을 아이템 리스트가 있는지 체크할것이야
    /// </summary>
    private void checkGetPrereItem() {
        List<int> lstPrereItem = DataManager.inst.getPrereItem(GameSettingMgr.inst.currentSettingScenario);

        if(lstPrereItem == null) {
            Log.d("Up DDa Ga Jyeol Item E");
            return;
        }

        if(lstPrereItem.Count > 0) {
            for(int i=0;i< lstPrereItem.Count; ++i) {
                mUIController.addItem(lstPrereItem[i]);
            }
        }
    }

    /// <summary>
    /// 캐릭터 스테이터스에 따른 이벤트 체크를 함
    /// </summary>
    private void checkCharacterStatus()
    {
        // 추후에 하겠죠?
    }

    /// <summary>
    /// 대사를 끝냈을때 실행해야 할 이벤트에 따라 실행함
    /// </summary>
    /// <param name="scriptEvent"></param>
    /// <param name="idx"></param>
    private void cbEndScript(string scriptEvent, List<string> idx) {

        int exclusiveIdx = 0;
        int stageIndex = 0;
        int itemIndex = 0;

        // 단독 이벤트의 경우
        if (idx.Count == 1) {

            switch (scriptEvent) {

                case CHANGE_INGAME_ITME_POS:
                    int itemIdx = 0;
                    float posX = 0;
                    float posY = 0;

                    itemIdx = int.Parse(idx[0]);
                    posX = float.Parse(idx[1]);
                    posY = float.Parse(idx[2]);

                    mStageController.changeIngameItemPos(itemIdx, posX, posY);

                    break;

                case CHANGE_Y_POSITION:
                    exclusiveIdx = int.Parse(idx[0]);
                    mUIController.changeScriptPos(exclusiveIdx);
                    break;

                case RESET_SCRIPT_PANEL_POS:
                    mUIController.resetScriptPos();
                    break;

                // 인게임 상에서 아이템을 활성화s
                case ENABLE_INGAME_ITEM:
                    exclusiveIdx = int.Parse(idx[0]);
                    mStageController.showItem(exclusiveIdx);
                    break;

                // 인게임 상에서 아이템 비 활성화
                case DISABLE_INGAME_ITEM:
                    exclusiveIdx = int.Parse(idx[0]);
                    mStageController.hideItem(exclusiveIdx);
                    break;

                // 인벤토리 아이템 제거
                case REMOVE_INVENTORY_ITEM:
                    exclusiveIdx = int.Parse(idx[0]);
                    mUIController.removeItem(exclusiveIdx);
                    break;

                // 인벤토리 아이템 활성화
                case GET_INVENTORY_ITEM:
                    exclusiveIdx = int.Parse(idx[0]);
                    mUIController.addItem(exclusiveIdx);
                    break;

                case APPEAR_GET_ITEM:
                    exclusiveIdx = int.Parse(idx[0]);
                    mUIController.showGetItemWidget(exclusiveIdx);
                    break;

                case DISAPPEAR_GET_ITEM:
                    mUIController.disapperGetItemWidget();
                    break;

                // 선행조건 충족 활성화
                case ADD_CONDITION:
                    exclusiveIdx = int.Parse(idx[0]);
                    PrerequisitesManager.inst.enablePrerequisites(exclusiveIdx, true);
                    mUIController.mHintPopup.setScript(exclusiveIdx);

                    break;

                // 선행조건 충족 비 활성화
                case REMOVE_CONDITION:
                    exclusiveIdx = int.Parse(idx[0]);
                    PrerequisitesManager.inst.enablePrerequisites(exclusiveIdx, false);
                    mUIController.mHintPopup.hide();
                    break;

                // 미리 얻을 아이템을 세팅한다.
                case ADD_PREREITEM:
                    stageIndex = int.Parse(idx[0]);
                    itemIndex = int.Parse(idx[1]);

                    DataManager.inst.addPrereItem(stageIndex, itemIndex);
                    break;

                case REMOVE_PREREITEM:
                    stageIndex = int.Parse(idx[0]);
                    itemIndex = int.Parse(idx[1]);

                    DataManager.inst.removePrereItem(stageIndex, itemIndex);
                    break;

                // 게임을 마친다.
                case END_GAME:
                    // 추후에 UI랑 연결해야함.
                    mUIController.showGameClearPopup(idx[0]);
                    break;

                case PLAY_UI_SOUND:
                    SoundManager.inst.playUISound(idx[0]);
                    break;

                case PLAY_BGM_SOUND:
                    SoundManager.inst.playBGM(idx[0]);
                    break;

                case PLAY_EAX_SOUND:
                    SoundManager.inst.playEAXSound(idx[0]);
                    break;

                case STOP_BGM_SOUND:
                    SoundManager.inst.fadeOutBGM();
                    break;

                case STOP_EAX_SOUND:
                    SoundManager.inst.fadeOutEAX();
                    break;

                case CLEAR_STAGE:
                    GameSettingMgr.inst.clearStage();
                    break;
            }
            // 추후에 여러 이벤트를 받아서 하는 경우 따로 생성하자
        } else {
            switch (scriptEvent) {

                case CHANGE_INGAME_ITME_POS:
                    int itemIdx = 0;
                    float posX = 0;
                    float posY = 0;

                    itemIdx = int.Parse(idx[0]);
                    posX = float.Parse(idx[1]);
                    posY = float.Parse(idx[2]);

                    mStageController.changeIngameItemPos(itemIdx, posX, posY);

                    break;
            }
        }
    }

    /// <summary>
    /// 인덱스에 맞는 스크립트를 불러준다.
    /// </summary>
    /// <param name="idx"></param>
    private void showScript(int idx) {

        IngameScriptData[] _data = getNextIdx(idx);
        if(_data == null) {
            return;
        }

        mUIController.setNextIngameScriptData(_data);
    }

    /// <summary>
    /// 외부로부터 들어온 인덱스를 스크립트 데이터에서 맞는 인덱스로 반환해준다.
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    private IngameScriptData[] getNextIdx(int idx) {

        IngameScriptData[] _data = null;

        for(int i = 0; i < IngameDataManager.inst.mIngameScriptData.Count; ++i) {
            for(int k = 0; k < IngameDataManager.inst.mIngameScriptData[i].Length; ++k) {
                if(idx == IngameDataManager.inst.mIngameScriptData[i][k].scriptIdx) {
                    _data = IngameDataManager.inst.mIngameScriptData[i];
                }
            }
        }

        return _data;
    }
}
