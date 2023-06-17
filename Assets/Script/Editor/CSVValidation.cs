using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(CSVValidation))]
/// <summary>
/// Csv 데이터 유효성 검증 커스텀 에디터 입니다.
/// </summary>
public class CSVValidation : EditorWindow
{
    private PrerequisitesManager mPrerequisitesManager;

    public TextAsset prevInven;
    public TextAsset prevPrere;


    public TextAsset CSVInvenInput;
    public TextAsset CSVPrerequisites;

    // 인벤토리 아이템 리스트
    private ItemDataList mItemDataList;

    private PrerequisitesDataList mPrerequisitesDataList;

    private bool isDataReady;

    // 스테이지 인덱스가 다르면 비교할 수 없다.
    private bool isNotEqualStageData;

    [MenuItem("ForProgMenu/CSVValidation")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(CSVValidation));
    }

    private void Awake() {
        isDataReady = false;
        mPrerequisitesManager = GameObject.Find("PrerequisitesManager").GetComponent<PrerequisitesManager>();
    }

    void OnGUI() {

        GUILayout.Label("이곳에 인벤토리 Csv 데이터를 넣어주세염", EditorStyles.boldLabel);
        CSVInvenInput = EditorGUILayout.ObjectField("Inventory CSV Input",
                                               CSVInvenInput,
                                               typeof(TextAsset),
                                               true) as TextAsset;

        if(CSVInvenInput != prevInven) {
            prevInven = CSVInvenInput;

            if(prevInven == null) {
                mItemDataList.clearList();
                mItemDataList = null;
                initValidData();
            }
        }

        GUILayout.Label("이곳에 선행조건 Csv 데이터를 넣어주세염", EditorStyles.boldLabel);
        CSVPrerequisites = EditorGUILayout.ObjectField("Prerequisites CSV Input",
                                               CSVPrerequisites,
                                               typeof(TextAsset),
                                               true) as TextAsset;

        if (CSVPrerequisites != prevPrere) {
            prevPrere = CSVPrerequisites;

            if (prevPrere == null) {
                mPrerequisitesDataList.clearList();
                mPrerequisitesDataList = null;
                initValidData();
            }
        }

        // 둘의 CSV 데이터가 모두 들어와야 데이터를 띄운다.
        if (CSVInvenInput != null && CSVPrerequisites != null) {
            startCsvData();
        } else {

            string tempStr = null;

            if(CSVInvenInput == null) {
                tempStr += "인벤토리 CSV ";
            }

            if(CSVPrerequisites == null) {
                tempStr += "선행조건 CSV ";
            }

            tempStr += "가 들어와있지 않습니다.";

            GUILayout.Label(tempStr, EditorStyles.boldLabel);
        }
    }

    private void startCsvData() {
        if(mItemDataList == null) {
            mItemDataList = new ItemDataList();

            if(mItemDataList.mLstItemsinfo.Count == 0) {
                mItemDataList.parse(CSVInvenInput);
            }
        }

        if (mPrerequisitesDataList == null) {
            mPrerequisitesDataList = new PrerequisitesDataList();

            if(mPrerequisitesDataList.mLstPrerequisitesInfo.Count == 0) {
                mPrerequisitesDataList.parse(CSVPrerequisites);
            }
        }

        // 데이터는 준비되어있는데 아직 처음으로 할당을 하지 않았다면
        if (!isDataReady && isReadyData()) {
            isDataReady = true;
            checkStageIndex();
        }

        if (isNotEqualStageData) {
            GUILayout.Label("인벤토리 데이터와 선행조건 데이터의 스테이지 인덱스 데이터의 숫자가 맞지 않습니다", EditorStyles.boldLabel);
        }else if (isDataReady) {
            drawCsvData();
        }
    }

    int stageIndex;

    int prevStageIndex;

    ItemData[] curItemData;
    PrerequisitesDataBundle prereData;

    private void drawCsvData() {
        EditorGUILayout.BeginVertical();

        stageIndex = EditorGUILayout.Popup(stageIndex, mLstStageIndex);

        if(prevStageIndex != stageIndex) {
            curItemData = null;
            prereData = null;
            prevStageIndex = stageIndex;
        }

        EditorGUILayout.BeginVertical();

        if(prereData == null) {
            prereData = mPrerequisitesDataList.mLstPrerequisitesInfo[stageIndex];
            mPrerequisitesManager.initPrerequisites(prereData.preIndex, prereData.strPre);
        }

        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginHorizontal();



        if(curItemData == null) {
            curItemData = mItemDataList.mLstItemsinfo[stageIndex].itemData;
        }

        for(int i=0;i< curItemData.Length; ++i) {
            GUILayout.Label(curItemData[i].path, EditorStyles.boldLabel);
        }

        if (GUILayout.Button("조합 테스트", EditorStyles.toolbarDropDown)) {
            for (int i = 0; i < curItemData.Length; ++i) {

                for (int k = 0; k < curItemData.Length; ++k) {
                    if(curItemData[i] == curItemData[k]) {
                        break;
                    }

                    try {
                        tryCombine(curItemData[i], curItemData[k]);
                    } catch (System.Exception) {
                        Log.e("에러",curItemData[i].itemIdx + " 와 " + curItemData[k].itemIdx + " 순으로 조합했을때 문제가 있음");
                    }
                }
            }
            
        }

        EditorGUILayout.EndHorizontal();



        EditorGUILayout.EndVertical();
    }

    private void initValidData() {
        isNotEqualStageData = false;
        isDataReady = false;

        mLstStageIndex = null;
    }

    private string[] mLstStageIndex;
    private List<bool> mPrereToggles = new List<bool>();

    private bool isReadyData() {
        return mPrerequisitesDataList.mLstPrerequisitesInfo.Count > 0 && mItemDataList.mLstItemsinfo.Count > 0;
    }

    /// <summary>
    /// 선행조건과 인벤토리에 저장되어있는 스테이지 인덱스가 같은지 체크
    /// </summary>
    private void checkStageIndex() {

        List<int> mLstInvenStageIndex = new List<int>();
        List<int> mLstPrereStageIndex = new List<int>();

        bool isExistStageIndex = false;

        for(int i = 0; i < mItemDataList.mLstItemsinfo.Count; ++i) {
            
            isExistStageIndex = false;

            for (int k = 0; k < mLstInvenStageIndex.Count; ++k) {
                if(mLstInvenStageIndex[k] == i+1) {
                    isExistStageIndex = true;
                    break;
                }
            }

            if (!isExistStageIndex) {
                mLstInvenStageIndex.Add(i+1);
            }
        }

        for (int i = 0; i < mPrerequisitesDataList.mLstPrerequisitesInfo.Count; ++i) {

            isExistStageIndex = false;

            for (int k = 0; k < mLstPrereStageIndex.Count; ++k) {
                if (mLstPrereStageIndex[k] == i+1) {
                    isExistStageIndex = true;
                    break;
                }
            }

            if (!isExistStageIndex) {
                mLstPrereStageIndex.Add(i+1);
                mPrereToggles.Add(false);
            }
        }
        
        if(mLstPrereStageIndex.Count != mLstInvenStageIndex.Count) {
            isNotEqualStageData = true;
        } else {

            mLstStageIndex = new string[mLstPrereStageIndex.Count];

            for (int i = 0; i < mLstPrereStageIndex.Count; ++i) {
                mLstStageIndex[i] = i.ToString();
            }
        }
    }


    private bool tryCombine(ItemData item1, ItemData item2) {

        for(int i = 0; i < item1.combineScript.Count; ++i) {
            if(item1.combineScript[i] != 0) {
                break;
            }

            if(i == item1.combineScript.Count - 1) {
                return false;
            }
        }

        for (int i = 0; i < item2.combineScript.Count; ++i) {
            if (item2.combineScript[i] != 0) {
                break;
            }

            if (i == item2.combineScript.Count - 1) {
                return false;
            }
        }

        Log.d("조합 안내",item1.koreanName + " + " + item2.koreanName + " 조합시도");

        bool isCombineFail = false;
        bool isCombineSuccess = false;

        // 기준점이 될 For 인덱스
        int standardReIndex = 0;

        // 기준이 되는 선행조건 인덱스
        int standardPreIndex = 0;
        int standardCombineIndex = 0;

        // 비교 대상이 되는 선행조건 인덱스
        int comparePreIndex = 0;
        int compareCombineIndex = 0;

        #region 선행 조건 체크

        standardReIndex = item1.prerequisites.Length - 1;

        // 조합 실패 판정이 나올때까지 되돌림.
        while (!isCombineFail) {

            if (standardReIndex < 0) {
                isCombineFail = true;
                break;
            }

            if (mPrerequisitesManager.isSatisfyPre(item1.prerequisites[standardReIndex])) {
                standardPreIndex = standardReIndex;
            } else {
                standardReIndex--;
                continue;
            }

            for (int i = item2.prerequisites.Length - 1; i >= 0; --i) {
                if (item2.prerequisites[i] == item1.prerequisites[standardPreIndex]) {
                    comparePreIndex = i;
                    isCombineSuccess = true;
                    break;
                }

                // 전부 비교했는데 조합이 실패되면
                if (i == 0 && standardReIndex == 0) {
                    isCombineFail = true;
                }
            }

            if (isCombineSuccess) {
                break;
            }

            standardReIndex--;
        }

        if (isCombineFail) {
            Log.w("조합 안내","조합 실패 혹은 선행조건이 올바르게 입력되지 않음");
            isCombineSuccess = false;
            return false;
        }

        #endregion

        #region 기준이 되는 아이템 기준으로 어떤 아이템을 조합하는지 체크

        for (int i = 0; i < item1.combineIdx[standardPreIndex].Length; ++i) {
            if (item1.combineIdx[standardPreIndex][i] == item2.itemIdx) {
                standardCombineIndex = i;
                break;
            }

            if (i == item1.combineIdx[standardPreIndex].Length - 1) {
                Log.w("조합 안내","기준 아이템과 비교 아이템의 조합 및 조합당하는 아이템 인덱스가 맞지않음");
                isCombineSuccess = false;
                return false;
            }
        }

        for (int i = 0; i < item2.combineIdx[comparePreIndex].Length; ++i) {
            if (item2.combineIdx[comparePreIndex][i] == item1.itemIdx) {
                compareCombineIndex = i;
                break;
            }

            if (i == item1.combineIdx[compareCombineIndex].Length - 1) {
                Log.w("조합 안내","비교 아이템과 기준 아이템의 조합 및 조합당하는 아이템 인덱스가 맞지않음");
                isCombineSuccess = false;
                return false;
            }
        }

        #endregion

        if (item1.combineScript[standardPreIndex] ==
           item2.combineScript[comparePreIndex]) {
            Log.i("조합 안내","조합 이상 없습니닷");
            isCombineSuccess = true;
        } else {
            isCombineSuccess = false;
        }

        return true;
    }
}