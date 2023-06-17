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
    public const string PREF_GAME_QUIT_DATA = "ExceptionQuit";
    
    /// <summary>
    /// 나가게 되는경우 데이터를 세이브한다.
    /// </summary>
    private void quitSaveData() {

        StringBuilder sb = new StringBuilder();

        // 인벤토리 아이템 현황
        List<int> tempList = new List<int>();

        tempList = mUIController.getCurrentItem;

        if (tempList.Count == 0) {
            sb.Append("-");
        } else {
            for (int i = 0; i < tempList.Count; ++i) {
                sb.Append(tempList[i].ToString());

                if (i != tempList.Count - 1) {
                    sb.Append(BaseCsv.DELIMITER_SUB);
                }
            }
        }

        sb.Append(BaseCsv.DELIMITER_AND);

        // 선행조건 현황
        for (int i = 0; i < PrerequisitesManager.inst.mDicPrerequisites.Count; ++i) {
            sb.Append(PrerequisitesManager.inst.mDicPrerequisites[i]);

            if(i != PrerequisitesManager.inst.mDicPrerequisites.Count - 1) {
                sb.Append(BaseCsv.DELIMITER_SUB);
            }
        }

        sb.Append(BaseCsv.DELIMITER_AND);

        // 인게임 현황

        tempList.Clear();

        tempList = mStageController.getEnableItems;

        for (int i = 0; i < tempList.Count; ++i) {
            sb.Append(tempList[i].ToString());

            if (i != tempList.Count - 1) {
                sb.Append(BaseCsv.DELIMITER_SUB);
            }
        }

        PlayerPrefs.SetString(PREF_GAME_QUIT_DATA, sb.ToString());

        // 흠.. 데이터 형식을 어케 해야하나ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ
        // 1. 각 타입별로 PlayerPrefs를 적용
        // EX > 아이템/ 선행조건 / 인게임 상황
        // 2. 하나의 스트링으로 다 넣은뒤 분기처리.
    }

    /// <summary>
    /// 다시 로드했을때 저장된 데이터가 있는경우 인게임 들어왔을떄 
    /// </summary>
    public void loadQuitData() {
        string quitData= PlayerPrefs.GetString(PREF_GAME_QUIT_DATA, "-");

        int[] inventorys = null;
        int[] prerequis = null;
        int[] ingames = null;

        // & 연산으로 3개 나눔(인벤토리, 선행조건, 인게임 추리 상황)
        string[] tempToken = quitData.Split(BaseCsv.DELIMITER_AND);
        string[] subToken;

        for(int i = 0; i < tempToken.Length; ++i) {
            subToken = tempToken[i].Split(BaseCsv.DELIMITER_SUB);
            switch (i) {
                case 0:
                    inventorys = new int[subToken.Length];
                    setLoadTokenData(subToken, ref inventorys);
                    break;

                case 1:
                    prerequis = new int[subToken.Length];
                    setLoadTokenData(subToken, ref prerequis);
                    break;

                case 2:
                    ingames = new int[subToken.Length];
                    setLoadTokenData(subToken, ref ingames);
                    break;
            }
        }

        mUIController.setLoadTokenData(inventorys);
        PrerequisitesManager.inst.setLoadTokenData(prerequis);
        mStageController.setLoadTokenData(ingames);
    }

    private void setLoadTokenData(string[] token, ref int[] value) {

        for (int k = 0; k < token.Length; ++k) {
            value[k] = Utils.toInt32(token[k]);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            loadQuitData();
        }

    }

    protected override void OnDestroy() {
        base.OnDestroy();

        // 강제 종료되는 경우 저장을 시키자ㅏㅏ
        quitSaveData();
    }
}
