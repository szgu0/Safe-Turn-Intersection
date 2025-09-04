using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRPCControl : MonoBehaviour
{
    [SerializeField]
    private AppSetting appSetting;


    [Header("Active Objcet")]
    [SerializeField]
    private bool activeObject = true;
    [SerializeField]
    private bool activeWhenPC;
    [SerializeField]
    private bool activeWhenVR;
    [SerializeField]
    private GameObject target;

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnPC;
    [SerializeField]
    private UnityEvent OnVR;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        if (appSetting.IsVR)
        {
            if (activeObject)
            {
                if (target)
                    target.SetActive(activeWhenVR);
                else
                    gameObject.SetActive(activeWhenVR);
            }

            OnVR.Invoke();
        }
        else
        {
            if (activeObject)
            {
                if (target) target.SetActive(activeWhenPC);
                else gameObject.SetActive(activeWhenPC);
            }

            OnPC.Invoke();
        }
    }
}
