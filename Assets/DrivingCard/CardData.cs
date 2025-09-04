using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string ID; //卡牌編號
    public string Title; //卡牌名稱
    public string Content;//卡牌說明
    public int SelectNum;//選擇那一張
    [Header("卡牌類型：")]
    public CardTypeData cardTypeData;
    [Header("車輛類型：")]
    public CarTypeData carTypeData;//A
    [Header("迴轉半徑：")]
    public Road_0_R road_0_R;//E1
    [Header("摩擦係數：")]
    public Road_0_U road_0_U;//E2
    [Header("超高角度：")]
    public Road_0_A road_0_A;//E3
    [Header("道路_1：")]
    public Road_1 road_1;//B
    [Header("道路_2：")]
    public Road_2 road_2;//C
    [Header("道路_3：")]
    public Road_3 road_3;//D

}

[System.Serializable]
public enum CardTypeData
{
    NULL,
    車輛,
    迴轉半徑,
    摩擦係數,
    超高角度,
    道路_1,
    道路_2,
    道路_3,
}

[System.Serializable]
public enum CarTypeData
{
    NULL,
    車輛1,
    車輛2,
    車輛3,
    車輛4,
    車輛5,
}

[System.Serializable]
public enum Road_0_R
{
    NULL,
    迴轉半徑10m,
    迴轉半徑30m,
    迴轉半徑50m,
    迴轉半徑80m,
    迴轉半徑100m,
}

[System.Serializable]
public enum Road_0_U
{
    //摩擦係數
    NULL,
    摩擦係數0,
    摩擦係數1,
    摩擦係數2,
    摩擦係數3,
    摩擦係數4,
    摩擦係數5,
}

public enum Road_0_A
{
    //超高角度
    NULL,
    超高角度0,
    超高角度1,
    超高角度2,
    超高角度3,
    超高角度4,
    超高角度5,
}


[System.Serializable]
public enum Road_1
{
    NULL,
    道路1,
    道路2,
    道路3,
    道路4,
    道路5,
}

[System.Serializable]
public enum Road_2
{
    NULL,
    道路1,
    道路2,
    道路3,
    道路4,
    道路5,
}

[System.Serializable]
public enum Road_3
{
    NULL,
    道路1,
    道路2,
    道路3,
    道路4,
    道路5,
}
