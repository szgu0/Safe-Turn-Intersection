using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AppData
{
    public static string MainSceneName = "Lobby";

    public static bool isGuide = true; // true：有指引；false：無指引


    public static string GetDownloadFolder()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);

        return (temp[0] + "/Download");
    }

    //by chhuang
    //駕駛視角
    public static DriverView driverView = DriverView.Free;
    public static CardData selectCard;
    public static float selectCardInputData; //E1使用
    public static CardData selectCard_1;
    public static CardData selectCard_2;
    public static CardData selectCard_3;
    public static int cardSelectNUM_1;
    public static int cardSelectNUM_2;
    public static int cardSelectNUM_3;

/* 無法成為靜態變數，帶不到下一步
    public static CardSelect cardSelect_1;
    public static CardSelect cardSelect_2;
    public static CardSelect cardSelect_3;
  */  


    //駕駛視角
    public static bool isFree;
    public static bool isDriver;
    public static bool isOutlook;
    public static bool isFollow;
    public static bool isDriverRoom;

    public static string ChooseCardName;
    public static string ChooseCarName;
    public static string ChooseFrictionName;

    //音樂、旁白、音效
    /*
    public static AudioSource BN_Player;
    public static AudioClip BN_AudioClip;
    public static AudioSource BGM_Player;
    public static AudioClip BGM_AudioClip;
    public static AudioSource BS_Player;
    public static AudioClip BS_AudioClip;

    public static float BNvolumn; //旁白
    public static float BGMvolumn; //背景
    public static float BSvolumn; //音效
    public static string BNFile;
    public static string BGMFile;
    public static string BSFile;
    */

    //static String ID = "";
}

public enum DriverView
{
    Free=0,
    Driver=1,
    Outlook=2,
    Follow=3,
    DriverRoom=4,
}
