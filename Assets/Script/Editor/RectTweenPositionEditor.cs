using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RectTweenPosition), true)]
public class RectTweenPositionEditor : BaseTweenEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(6f);
        EditorGUIUtility.labelWidth = 120f;

        RectTweenPosition tw = target as RectTweenPosition;
        GUI.changed = false;

        Vector2 from = EditorGUILayout.Vector2Field("From", tw.from);
        Vector2 to = EditorGUILayout.Vector2Field("To", tw.to);

        if (GUI.changed)
        {
            tw.from = from;
            tw.to = to;
        }

        drawCommonProperties();
    }
}
