using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] myBGT;
    public int volume = 5;
    void Start()
    {
        myBGT = GetComponentsInChildren<Transform>();
        switch (gameObject.name)
        {
            case "Narrator_block":
                volume = Convert.ToInt32(AudioPlayerControl.BN_Player.volume * 10);
                break;
            case "sound_block":
                volume = Convert.ToInt32(AudioPlayerControl.BS_Player.volume * 10);
                break;
            case "BG_block":
                volume = Convert.ToInt32(AudioPlayerControl.BGM_Player.volume * 10);
                break;
        }

        switchvolume(volume);
    }

    public void switchvolume(int tovolume)
    {
        for (int i = 1; i < myBGT.Length; i++)
        {
            myBGT[i].gameObject.SetActive(false);
        }
        for (int i = 1; i <= tovolume; i++)
        {
            myBGT[i].gameObject.SetActive(true);
        }

        switch (gameObject.name)
        {
            case "Narrator_block":
                AudioPlayerControl.BN_Player.volume = volume / 10.0f;
                break;
            case "sound_block":
                AudioPlayerControl.BS_Player.volume = volume / 10.0f;
                break;
            case "BG_block":
                AudioPlayerControl.BGM_Player.volume = volume / 10.0f;
                break;
        }
    }

    public void addvolume()
    {
        volume += 1;
        if(volume>10)
        {
            volume = 10;
        }
        switchvolume(volume);
    }

    public void subvolume()
    {
        volume -= 1;
        if (volume < 0)
        {
            volume = 0;
        }
        switchvolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
