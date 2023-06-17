using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataManager), true)]
[CanEditMultipleObjects]
public class DataManagerInspector : Editor {

    private DataManager mTarget;

    void OnEnable() {
        //Character 컴포넌트를 얻어오기
        mTarget = (DataManager)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        drawInspector();
    }

    protected void drawInspector() {

        string label = null;

        EditorGUILayout.BeginVertical("캐릭터 데이터 현황");

        label = string.Format("캐릭터 = {0}", mTarget.mCharacterDataManager.charKind.ToString());
        EditorGUILayout.LabelField(label);

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("업적 데이터 현황");

        EditorGUILayout.LabelField("가구 현황");

        for (int i = 0; i < mTarget.mAchievementsDataManager.mLstFurniture.Count; ++i) {

            label = string.Format("{0} 번째 가구 오픈 상태 = ",i+1);

            if (mTarget.mAchievementsDataManager.mLstFurniture[i]) {
                label = string.Format("{0}{1}", label, "True");
            } else {
                label = string.Format("{0}{1}", label, "false");
            }

            EditorGUILayout.LabelField(label);
        }

        EditorGUILayout.LabelField("일러스트 현황");

        for (int i = 0; i < mTarget.mAchievementsDataManager.mLstIllust.Count; ++i) {

            label = string.Format("{0} 번째 일러스트 오픈 상태 = ", i + 1);

            if (mTarget.mAchievementsDataManager.mLstIllust[i]) {
                label = string.Format("{0}{1}", label, "True");
            } else {
                label = string.Format("{0}{1}", label, "false");
            }

            EditorGUILayout.LabelField(label);
        }

        EditorGUILayout.LabelField("쥐 패턴 현황");

        for (int i = 0; i < mTarget.mAchievementsDataManager.mLstPattern.Count; ++i) {

            label = string.Format("{0} 번째 패턴 오픈 상태 = ", i + 1);

            if (mTarget.mAchievementsDataManager.mLstPattern[i]) {
                label = string.Format("{0}{1}", label, "True");
            } else {
                label = string.Format("{0}{1}", label, "false");
            }

            EditorGUILayout.LabelField(label);
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("마이룸 현황");

        for (int i = 0; i < mTarget.mMyRoomDataManager.mLstFurnitureInfo.Count; ++i) {

            label = string.Format("가구 이름 {0} 위치 인덱스 {1}", mTarget.mMyRoomDataManager.mLstFurnitureInfo[i].furniture, mTarget.mMyRoomDataManager.mLstFurnitureInfo[i].posIdx);

            EditorGUILayout.LabelField(label);
        }

        EditorGUILayout.EndVertical();

        EditorUtility.SetDirty(mTarget);
    }
}
