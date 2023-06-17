using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderDowner : MonoBehaviour
{
    private MeshRenderer _render;

    private void Awake() {
        _render = transform.GetComponent<MeshRenderer>();
        _render.sortingLayerName = "BgRender";
        _render.sortingOrder = -1000;
    }
}
