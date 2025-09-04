using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInject : MonoBehaviour
{
    [SerializeField]
    private TextSetReference textSetReference;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private TextMeshProUGUI UItext;

    [SerializeField]
    private bool playSound = false;

    void Start()
    {
        if (!playSound)
            SetText(LevelManager.Instance.GetText(textSetReference.ID));
        else
        {
            LevelManager.Instance.GetTextAndAudioClip(textSetReference.ID, out string text, out AudioClip clip);
            SetText(text);
            AudioManager.ins.Play(clip);
        }
    }

    void SetText(string _text)
    {
        if (text)
        {
            text.text = _text;
        }
        else if (UItext)
        {
            UItext.text = _text;
        }
    }

    void Reset()
    {
        text = GetComponent<TextMeshPro>();
        UItext = GetComponent<TextMeshProUGUI>();
    }

    [ContextMenu("Preview (PC)")]
    void TestInject_PC()
    {
        var levelManager = FindObjectOfType<LevelManager>();

        if (!levelManager)
        {
            Debug.LogError("No LevelManager found in scene");
            return;
        }

        string _text = levelManager.GetText(textSetReference.ID, false);

        if (text)
        {
            text.text = _text;
        }
        else if (UItext)
        {
            UItext.text = _text;
        }
    }

    [ContextMenu("Preview (VR)")]
    void TestInject_VR()
    {
        var levelManager = FindObjectOfType<LevelManager>();

        if (!levelManager)
        {
            Debug.LogError("No LevelManager found in scene");
            return;
        }

        string _text = levelManager.GetText(textSetReference.ID, true);

        if (text)
        {
            text.text = _text;
        }
        else if (UItext)
        {
            UItext.text = _text;
        }
    }
}
