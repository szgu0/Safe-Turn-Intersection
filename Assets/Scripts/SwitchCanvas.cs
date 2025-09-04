using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public AppSetting appSettings;
    public Vector3 mysizeDelta = new Vector2(1920, 1080);
    public Vector3 mylocalPosition = new Vector3(0.3f, 1.5f, -2.3f);
    public Vector3 mylocalRotation = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 mylocalScale = new Vector3(0.0016f, 0.0016f, 0.0016f);

    void Start()
    {
        if (appSettings.IsPC)
        {
            switchToPCCanvas();
        }
        // else if (appSettings.IsVR)
        // {
        //     switchToVRCanvas();
        // }
    }

    public void switchToPCCanvas()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void switchToVRCanvas()
    {
        GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        GetComponent<RectTransform>().localPosition = mylocalPosition;
        GetComponent<RectTransform>().localRotation = Quaternion.Euler(mylocalRotation);
        GetComponent<RectTransform>().sizeDelta = mysizeDelta;
        GetComponent<RectTransform>().localScale = mylocalScale;
    }

}
