using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension
{
    public static void ArrayInitialize(this int[] objects) {
        if(objects != null) {
            objects.Initialize();
        }
    }

    public static void ArrayInitialize(this int[][] objects) {
        if (objects != null) {
            objects.Initialize();
        }
    }

    public static void setLocalX(this Transform trf, float x) {
        Vector3 localPos = trf.localPosition;
        localPos.x = x;
        trf.localPosition = localPos;
    }

    public static void setLocalY(this Transform trf, float y) {
        Vector3 localPos = trf.localPosition;
        localPos.y = y;
        trf.localPosition = localPos;
    }

    public static int[] convertLstToArr(this List<int> lst) {
        int[] arr = new int[lst.Count];

        for(int i = 0; i < arr.Length; ++i) {
            arr[i] = lst[i];
        }

        return arr;
    }

    public static void setAlpha(this MaskableGraphic ui, float alpha)
    {
        Color color = ui.color;
        color.a = alpha;
        ui.color = color;
    }

    public static void setRectTrfX(this RectTransform rectTrf, float posX)
    {
        Vector2 pos = rectTrf.anchoredPosition;
        pos.x = posX;

        rectTrf.anchoredPosition = pos;
    }

    public static void setPos(this RectTransform rectTrf, Vector2 pos)
    {
        rectTrf.anchoredPosition = pos;
    }

    public static void setRectPosY(this RectTransform rectTrf, float posY) {
        Vector2 pos = rectTrf.anchoredPosition;
        pos.y = posY;

        rectTrf.anchoredPosition = pos;
    }

    public static void setRotation(this RectTransform rectTrf, Vector3 rotate) {
        rectTrf.localEulerAngles = rotate;
    }

    public static void setTextSize(this RectTransform rectTrf, float width, float height)
    {
        Vector2 size = new Vector2(width, height);
        rectTrf.sizeDelta = size;
    }
}
