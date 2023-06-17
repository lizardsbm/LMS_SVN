using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HintPopup : BaseBehaviour
{
    private const float posX = 880;
    private Text text;
    public IngameHintData[] data { get; set; }
    [SerializeField] private RectTransform toggleBtnTrf;
    [SerializeField] private Button toggleBtn;

    protected override void initVariables()
    {
        base.initVariables();
        text = GetComponentInChildren<Text>();
        text.text = data == null ? "" : data[0].scripts; //강제 초기화 전 data가 null임
        StartCoroutine(timer());
    }

    public void setScript(int eventIdx)
    {
        for (int i = 0; i < data.Length; ++i)
        {
            if (eventIdx == data[i].prerequisites) //어차피 선행조건 충족된 상황일테니 같은 prerequisites값을 갖고있는 스크립트 집어넣기
            {
                if (Utils.isActive(this))
                {
                    StopCoroutine(timer());
                }
                else
                {
                    StopCoroutine(timer());
                    this.show();
                    StartCoroutine(timer());
                }
                text.text = data[i].scripts;
                break;
            }
        }
    }

    public override void show() => base.show();


    public override void hide()
    {
        base.hide();
        StopCoroutine(timer());
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(5);
        this.hide();
    }
}

