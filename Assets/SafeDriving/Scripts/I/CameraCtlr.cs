using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtlr : MonoBehaviour
{
    //public ViewCtrl viewCtrl;

    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;

    public GameObject Board_1;
    public GameObject Board_2;
    public GameObject Board_3;

    public bool isSwish;
    public int cameraCount;

    


    // Start is called before the first frame update
    void Start()
    {
        //viewCtrl = GameObject.Find("View_Ctrl").GetComponent<ViewCtrl>();

        if (AppData.isDriver)
        {
            cameraCount = 2;
        }
        else if (AppData.isFollow)
        {
            cameraCount = 3;
        }
        else if (AppData.isOutlook)
        {
            cameraCount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
       



        if (Input.GetKeyDown("p"))
        {
            if(cameraCount == 3)
            {
                cameraCount = 0;
            }
            cameraCount++;
        }



        if (cameraCount == 1)
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);
            Camera3.SetActive(false);

            Board_1.SetActive(true);
            Board_2.SetActive(false);
            Board_3.SetActive(false);
        }
        else if (cameraCount == 2)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            Camera3.SetActive(false);

            Board_1.SetActive(false);
            Board_2.SetActive(true);
            Board_3.SetActive(false);
        }
        else if (cameraCount == 3)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(false);
            Camera3.SetActive(true);

            Board_1.SetActive(false);
            Board_2.SetActive(false);
            Board_3.SetActive(true);
        }
    }

    public void ChangeCamera()
    {
        if (cameraCount == 3)
        {
            cameraCount = 0;
        }
        cameraCount++;
    }
}
