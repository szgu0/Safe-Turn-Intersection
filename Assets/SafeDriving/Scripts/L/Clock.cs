using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    
    void Start()
    {
        // 根據物件名稱查找 GameObject
        hourHand = GameObject.Find("hour_hand_0.002").transform;
        minuteHand  = GameObject.Find("hour_hand_0.003").transform;
        secondHand  = GameObject.Find("hour_hand_0.001").transform;
    }
    // 更新
    void FixedUpdate()
    {
        DateTime currentTime = DateTime.Now;

        // 秒針的角度，每秒鐘轉動360度 / 60秒 = 6度
        float secondsAngle = currentTime.Second * 6;
        secondHand.localRotation = Quaternion.Euler(secondsAngle-36, 0, -90);

        // 分針的角度，每分鐘轉動360度 / 60分鐘 = 6度，再加上每秒的微小移動
        float minutesAngle = currentTime.Minute * 6 + (currentTime.Second / 60f) * 6;
        minuteHand.localRotation = Quaternion.Euler(minutesAngle, 0, -90);

        // 時針的角度，每小時轉動360度 / 12小時 = 30度，再加上每分鐘的微小移動
        float hoursAngle = (currentTime.Hour % 12) * 30 + (currentTime.Minute / 60f) * 30;
        hourHand.localRotation = Quaternion.Euler(hoursAngle+90, 0, -90);
    }
}
