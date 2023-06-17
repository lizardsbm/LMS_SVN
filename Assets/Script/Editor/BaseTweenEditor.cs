using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseTween), true)]
public class BaseTweenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        drawCommonProperties();
    }

    protected void drawCommonProperties()
    {
        BaseTween bt = target as BaseTween;

        GUILayout.BeginVertical();
        GUILayout.Space(2f);
        EditorGUIUtility.labelWidth = 110;

        GUI.changed = false;

        BaseTween.Style style = (BaseTween.Style)EditorGUILayout.EnumPopup("Play Style", bt.style);
        AnimationCurve curve = EditorGUILayout.CurveField("Animation Curve", bt.curve, GUILayout.Width(170f), GUILayout.Height(62f));
        //UITweener.Method method = (UITweener.Method)EditorGUILayout.EnumPopup("Play Method", tw.method);

        GUILayout.BeginHorizontal();
        float dur = EditorGUILayout.FloatField("Duration", bt.duration, GUILayout.Width(170f));
        GUILayout.Label("seconds");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        float del = EditorGUILayout.FloatField("Start Delay", bt.delay, GUILayout.Width(170f));
        GUILayout.Label("seconds");
        GUILayout.EndHorizontal();

        if (GUI.changed)
        {
            bt.curve = curve;
            //tw.method = method;
            bt.style = style;
            bt.duration = dur;
            bt.delay = del;
        }

        GUILayout.EndVertical();

    }
}
