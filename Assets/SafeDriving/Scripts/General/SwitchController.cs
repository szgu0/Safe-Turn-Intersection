using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.VRModuleManagement;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public GameObject PC_Controller;
    public GameObject VR_Controller;

    public Camera PC_Camera;
    [SerializeField]
    float PC_FOV_V;
    [SerializeField]
    float PC_FOV_H;
    [SerializeField]
    private AppSetting appSetting;

    void Awake()
    {
        if (appSetting.IsVR)
        {
            PC_Controller.SetActive(false);
        }
        if (appSetting.IsPC)
        {
            VR_Controller.SetActive(false);
        }

    }
    IEnumerator Start()
    {
        if (appSetting.IsPC)
        {
            VR_Controller.SetActive(false);
            PC_Controller.SetActive(true);
            //配合解析度，修改FOV，預設1920x1080
            PC_FOV_V = PC_Camera.fieldOfView;
            float L = 1080.0f / 2.0f / Mathf.Tan(PC_FOV_V / 2.0f / 180.0f * Mathf.PI);
            PC_FOV_H = Mathf.Atan2(1920.0f / 2.0f, L) / Mathf.PI * 180.0f * 2.0f;
            L = Screen.width / 2.0f / Mathf.Tan(PC_FOV_H / 2.0f / 180.0f * Mathf.PI);
            PC_FOV_V = Mathf.Atan2(Screen.height / 2.0f, L) / Mathf.PI * 180.0f * 2.0f;
            PC_Camera.fieldOfView = PC_FOV_V;
            //PC_Camera.transform.localPosition = new Vector3(0.0f, 1.6f, 0.0f);


        }
        else if (appSetting.IsVR)
        {
            PC_Controller.SetActive(false);

// #if UNITY_EDITOR
            yield return null;
            yield return null;
// #else
//             yield return new WaitForSeconds(f);
// #endif

            // PC_Controller.SetActive(false);
            VR_Controller.SetActive(true);
        }
    }


}
