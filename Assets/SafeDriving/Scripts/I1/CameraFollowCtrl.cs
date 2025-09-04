using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class CameraFollowCtrl : MonoBehaviour
{
    public GameObject Board;

   

    public GameObject Outlook_Camera;
    public GameObject Driver_Camera;
    public GameObject Follow_Camera;

    public GameObject Outlook_Board;
    public GameObject Driver_Board;
    public GameObject Follow_Board;

    public int cameraCount = 1;


    // Start is called before the first frame update
    void Start()
    {
       
        Outlook_Board.SetActive(false);
        Follow_Board.SetActive(false);
        Driver_Board.SetActive(false);

        if (AppData.driverView == DriverView.Driver || AppData.driverView == DriverView.Outlook || AppData.driverView == DriverView.Follow)
        {
            ChangeFollowCamera(AppData.driverView);
        }
        else
        {
            ChangeFollowCamera(DriverView.Driver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ChangeCamera();
        }
        
    }
    public void ChangeCamera()
    {
       
        cameraCount++;
        if (cameraCount == 4)
        {
            cameraCount = 1;
        }
        ChangeFollowCamera((DriverView)cameraCount);
    }


    public void ChangeFollowCamera(DriverView camera)
    {
        if (camera == DriverView.Driver)
        {
            Outlook_Camera.SetActive(false);
            Follow_Camera.SetActive(false);
            Driver_Camera.SetActive(true);
            Board.transform.localPosition = Driver_Board.transform.localPosition;
            Board.transform.localScale = Driver_Board.transform.localScale;
            Board.transform.localRotation = Driver_Board.transform.localRotation;
        }
        if (camera == DriverView.Follow)
        {
            Outlook_Camera.SetActive(false);
            Driver_Camera.SetActive(false);
            Follow_Camera.SetActive(true);
            Board.transform.localPosition = Follow_Board.transform.localPosition;
            Board.transform.localScale = Follow_Board.transform.localScale;
            Board.transform.localRotation = Follow_Board.transform.localRotation;
        }
        if (camera == DriverView.Outlook)
        {
            Driver_Camera.SetActive(false);
            Follow_Camera.SetActive(false);
            Outlook_Camera.SetActive(true);
            Board.transform.localPosition = Outlook_Board.transform.localPosition;
            Board.transform.localScale = Outlook_Board.transform.localScale;
            Board.transform.localRotation = Outlook_Board.transform.localRotation;
        }

    }
}
