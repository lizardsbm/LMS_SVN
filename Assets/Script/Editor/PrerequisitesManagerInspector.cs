using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrerequisitesManager), true)]
[CanEditMultipleObjects]
public class PrerequisitesManagerInspector : Editor {

    private PrerequisitesManager mTarget;

    void OnEnable()
    {
        //Character 컴포넌트를 얻어오기
        mTarget = (PrerequisitesManager)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        drawInspector();
    }

    protected void drawInspector()
    {

        EditorGUILayout.BeginVertical();
        GUILayout.Label("선행조건 현황");

        DrawPreString(ref mTarget.mDicPrerequisites, mTarget.mLstPre);

        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(mTarget);
    }

    private void DrawPreString(ref Dictionary<int, bool> dic, List<string> str) {

        string label = null;

        for (int i = 0; i < dic.Count; ++i) {

            label = null;

            if (dic[i]) {
                label = string.Format("{0} = {1}", str[i], " 충족");
            } else {
                label = string.Format("{0} = {1}", str[i], " 불충족");
            }

            if (GUILayout.Button(str[i] + " 조건 변경")) {
                dic[i] = !dic[i];
            }

            EditorGUILayout.LabelField(label);
        }
    }

}
