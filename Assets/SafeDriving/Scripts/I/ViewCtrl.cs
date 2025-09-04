using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewCtrl : MonoBehaviour
{
    /*
    public bool isFree;
    public bool isDriver;
    public bool isOutlook;
    public bool isFollow;
    public bool isDriverRoom;
    */
    //public GameObject nextB;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void FreeMode()
    {
        AppData.isFree = true;
        AppData.isDriver = false;
        AppData.isDriverRoom = false;
        AppData.isFollow = false;
        AppData.isOutlook = false;

        //Application.LoadLevel("Driver_Car");
        SceneManager.LoadScene("Driver_Car_I1");
    }

    public void DriverMode()
    {
        AppData.isDriver = true;
        AppData.isFree = false;
        AppData.isDriverRoom = false;
        AppData.isFollow = false;
        AppData.isOutlook = false;

        //Application.LoadLevel("Driver_Car");
        SceneManager.LoadScene("Driver_Car_I1");
    }

    public void OutlookMode()
    {
        AppData.isOutlook = true;
        AppData.isDriver = false;
        AppData.isDriverRoom = false;
        AppData.isFollow = false;
        AppData.isFree = false;

        //Application.LoadLevel("Driver_Car");
        SceneManager.LoadScene("Driver_Car_I1");
    }

    public void FollowMode()
    {
        AppData.isFollow = true;
        AppData.isDriver = false;
        AppData.isDriverRoom = false;
        AppData.isFree = false;
        AppData.isOutlook = false;

        //Application.LoadLevel("Driver_Car");
        SceneManager.LoadScene("Driver_Car_I1");
    }

    public void DriverRoomMode()
    {
        AppData.isDriverRoom = true;
        AppData.isFree = false;
        AppData.isOutlook = false;
        AppData.isFollow = false;
        AppData.isDriver = false;

        //Application.LoadLevel("Driver_Car");
        SceneManager.LoadScene("Driver_Car_I1");
    }

}
