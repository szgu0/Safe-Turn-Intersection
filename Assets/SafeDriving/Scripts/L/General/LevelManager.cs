using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private AppSetting appSetting;
    [SerializeField]
    private LevelTextData textData;
    [SerializeField]
    private LevelTextData baseTextData;

    void Awake()
    {
        Instance = this;
    }

    public string GetText(string id)
    {
        if (textData.GetText(id, appSetting ? appSetting.IsVR : false, out string data))
            return data;
        else if (baseTextData.GetText(id, appSetting ? appSetting.IsVR : false, out data))
            return data;
        else
            return string.Empty;
    }

    public string GetText(string id, bool isVR)
    {
        if (textData.GetText(id, appSetting ? isVR : false, out string data))
            return data;
        else if (baseTextData.GetText(id, appSetting ? isVR : false, out data))
            return data;
        else
            return string.Empty;
    }

    public bool TryGetText(string id, out string data)
    {
        if (textData.GetText(id, appSetting ? appSetting.IsVR : false, out data))
            return true;
        else if (baseTextData.GetText(id, appSetting ? appSetting.IsVR : false, out data))
            return true;
        
        data = string.Empty;
        return false;
    }

    public void GetTextAndAudioClip(string id, out string text, out AudioClip clip)
    {
        if (textData.GetTextAndAudioClip(id, appSetting ? appSetting.IsVR : false, out text, out clip))
            return;
        else if (baseTextData.GetTextAndAudioClip(id, appSetting ? appSetting.IsVR : false, out text, out clip))
            return;
        else
        {
            text = string.Empty;
            clip = null;
        }
    }

    public bool TryGetTextAndAudioClip(string id, out string text, out AudioClip clip)
    {
        if (textData.GetTextAndAudioClip(id, appSetting ? appSetting.IsVR : false, out text, out clip))
            return true;
        else if (baseTextData.GetTextAndAudioClip(id, appSetting ? appSetting.IsVR : false, out text, out clip))
            return true;
        else
        {
            text = string.Empty;
            clip = null;
            return false;
        }
    }

    public TextSet GetRawTextSet(string id)
    {
        if (textData.GetRawTextSet(id, out TextSet _set))
            return _set;
        else if (baseTextData.GetRawTextSet(id, out _set))
            return _set;
        else
            return new TextSet();
    }


    public bool TryGetRawTextSet(string id, out TextSet _set)
    {
        if (textData.GetRawTextSet(id, out _set))
            return true;
        else if (baseTextData.GetRawTextSet(id, out _set))
            return true;
        _set = new TextSet();
        return false;
    }
}
