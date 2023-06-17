using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RectTweenRotation), true)]
public class RectTweenRotationEditor : BaseTweenEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(6f);
        EditorGUIUtility.labelWidth = 120f;

        RectTweenRotation tw = target as RectTweenRotation;
        GUI.changed = false;

        Vector3 from = EditorGUILayout.Vector3Field("From", tw.from);
        Vector3 to = EditorGUILayout.Vector3Field("To", tw.to);

        if (GUI.changed)
        {
            tw.from = from;
            tw.to = to;
        }

        drawCommonProperties();
    }
}
