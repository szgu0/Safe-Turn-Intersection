using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class toShowKeyboard : MonoBehaviour, ISelectHandler
{
    InputField m_InputField;

    void Start()
    {
        m_InputField = GetComponent<InputField>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        TalesFromTheRift.CanvasKeyboard.Open(gameObject);
    }
}
