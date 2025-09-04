using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceCtrl_I3 : MonoBehaviour
{
    //public ViewCtrl viewCtrl;

    public void ChangeSence()
    {
        if (AppData.isDriver || AppData.isOutlook || AppData.isFollow)
        {
            //Application.LoadLevel("Driver_Car");
            SceneManager.LoadScene("Driver_Car_I3");
        }

        //else if (viewCtrl.isOutlook)
        //{
        //    Application.LoadLevel("OutLook_Car");
        //}

        //else if (viewCtrl.isFollow)
        //{
        //    Application.LoadLevel("Follow_Car");
        //}
       
    }

    public void ReturnI3_Start()
    {
        //Application.LoadLevel("I1");
        SceneManager.LoadScene("I3");
    }

    public void Quit_I3()
    {
        SceneManager.LoadScene("I3_End");
    }

    public void BackOG()
    {
        SceneManager.LoadScene("Lobby");
    }
}
