using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchCtrl : MonoBehaviour
{
    [SerializeField]
    public void ChangeCameraView(int view)
    {
        AppData.driverView = (DriverView)view;
    }
}

/*
public enum DriverView
{
    Free = 0,
    Driver = 1,
    Outlook = 2,
    Follow = 3,
    DriverRoom = 4,
}
*/

