using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AppSetting appSetting;
    [SerializeField]
    private AudioClip pcClip;
    [SerializeField]
    private AudioClip vrClip;
    [SerializeField]
    private bool playOnStart;

    void Start()
    {
        if (playOnStart)
            Play();
    }

    public void Play()
    {
        if (appSetting)
        {
            if (pcClip && !vrClip)
                AudioManager.ins?.Play(pcClip);
            else if (!pcClip && vrClip)
                AudioManager.ins?.Play(vrClip);
            else if (pcClip && vrClip)
            {
                if (appSetting.IsVR)
                    AudioManager.ins?.Play(vrClip);
                else
                    AudioManager.ins?.Play(pcClip);
            }
        }
        else
        {
            if (pcClip && !vrClip)
                AudioManager.ins?.Play(pcClip);
            else if (!pcClip && vrClip)
                AudioManager.ins?.Play(vrClip);
            else
                AudioManager.ins?.Play(pcClip);
        }
    }
}
