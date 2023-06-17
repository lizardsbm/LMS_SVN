using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : BaseBehaviour
{
    private int hour, minute, second;
    const float hourAngle = 30, minuteAngle = 6, oneMinute = 60;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform hourHand;

    protected override void initVariables()
    {
        base.initVariables();
        StartCoroutine(TickTock());
    }
    IEnumerator TickTock()
    {
        SetHands();
        return new WaitForSecondsRealtime(oneMinute - second);// 1분 지날 때
    }

    void SetHands()//시스템 시간 받아와서 각도 설정
    {
        hour = int.Parse(System.DateTime.Now.ToString("hh"));
        minute = int.Parse(System.DateTime.Now.ToString("mm"));
        second = int.Parse(System.DateTime.Now.ToString("ss"));
        hourHand.localEulerAngles = new Vector3(0, 0, hourAngle * hour + (minute * 0.5f));
        minuteHand.localEulerAngles = new Vector3(0, 0, minuteAngle * minute);
    }
}
