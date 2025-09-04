using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class AudioPlayerControl : MonoBehaviour
{
    //音樂、旁白、音效
    public static AudioSource BN_Player;//旁白
    public AudioClip BN_AudioClip;
    public static AudioSource BGM_Player;//背景
    public AudioClip BGM_AudioClip;
    public static AudioSource BS_Player;//音效
    public AudioClip BS_AudioClip;

    //音效設定檔

    static string AudioSetupFileName = "audiosetup.csv";

    public static AudioPlayerControl Instance { get; set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            BN_Player = transform.Find("BNPlayer").GetComponent<AudioSource>();
            BGM_Player = transform.Find("BGMPlayer").GetComponent<AudioSource>();
            BS_Player = transform.Find("BSPlayer").GetComponent<AudioSource>();
            Instance = this;
            DontDestroyOnLoad(this);
        }
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        loadAudioSetupFile();

        BGM_Player.clip = BGM_AudioClip;
        BGM_Player.playOnAwake = true;
        BGM_Player.Play();
    }

    public static bool loadAudioSetupFile()
    {
        BN_Player.volume = PlayerPrefs.GetFloat("BN_Player", 0.5f);
        BGM_Player.volume = PlayerPrefs.GetFloat("BGM_Player", 0.2f);
        BS_Player.volume = PlayerPrefs.GetFloat("BS_Player", 0.5f);

        /*
        string FileName = AudioSetupFileName;
        //確定檔案存在
        string filePath = Application.streamingAssetsPath + "/Setup/" + FileName;
        if (FileName == null || (!File.Exists(filePath)))
        {
            return false;
        }

        List<string[]> stringlist = new List<string[]>();
        string line;
        StreamReader stream = new StreamReader(filePath);
        while ((line = stream.ReadLine()) != null)
        {
            line.Trim();
            if (!line.StartsWith("#"))
            {
                stringlist.Add(line.Split(','));
            }
        }
        stream.Close();
        stream.Dispose();

        BN_Player.volume = Convert.ToInt32(stringlist[0][1]) / 10.0f;
        BGM_Player.volume = Convert.ToInt32(stringlist[1][1]) / 10.0f;
        BS_Player.volume = Convert.ToInt32(stringlist[2][1]) / 10.0f;
        */
        return true;
    }

    public static bool saveAudioSetupFile()
    {
         PlayerPrefs.SetFloat("BN_Player", BN_Player.volume);
         PlayerPrefs.SetFloat("BGM_Player", BGM_Player.volume);
         PlayerPrefs.SetFloat("BS_Player", BS_Player.volume);

        /*
        string FileName = AudioSetupFileName;
        //確定檔案存在
        string filePath = Application.streamingAssetsPath + "/Setup/" + FileName;
        if (FileName == null)
        {
            return false;
        }
        if (!File.Exists(filePath))
        {
            File.CreateText(filePath).Dispose();

        }

        StreamWriter stream = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
        string mystring = "";
        mystring = "BN," + Convert.ToInt32(BN_Player.volume * 10).ToString();
        stream.WriteLine(mystring);
        mystring = "BGM," + Convert.ToInt32(BGM_Player.volume * 10).ToString();
        stream.WriteLine(mystring);
        mystring = "BS," + Convert.ToInt32(BS_Player.volume * 10).ToString();
        stream.WriteLine(mystring);
        stream.Close();
        stream.Dispose();
        */
        return true;
    }
}
