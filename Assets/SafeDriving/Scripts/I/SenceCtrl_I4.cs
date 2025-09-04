using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceCtrl_I4 : MonoBehaviour
{
    //public ViewCtrl viewCtrl;

    public void ChangeSence()
    {
        if (AppData.isDriver || AppData.isOutlook || AppData.isFollow)
        {
            //Application.LoadLevel("Driver_Car");
            SceneManager.LoadScene("Driver_Car_I4");
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

    public void ReturnI4_Start()
    {
        //Application.LoadLevel("I1");
        SceneManager.LoadScene("I4");
    }

    public void Quit_I4()
    {
        SceneManager.LoadScene("I4_End");
    }

    public void BackOG()
    {
        SceneManager.LoadScene("Lobby");
    }
}
