using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceCtrl : MonoBehaviour
{
    public string nowscenename = "";

    public void ChangeToDriveSence()
    {
        if (AppData.isDriver || AppData.isOutlook || AppData.isFollow)
        {
            nowscenename = SceneManager.GetActiveScene().name;

            if (nowscenename == "E1")
            {
                SceneManager.LoadScene("Driver_Car_E1");
            }
            if (nowscenename == "E2")
            {
                SceneManager.LoadScene("Driver_Car_E2");
            }
            if (nowscenename == "E3")
            {
                SceneManager.LoadScene("Driver_Car_E3");
            }
            if (nowscenename == "E4")
            {
                SceneManager.LoadScene("Driver_Car_E4");
            }

        }
    }
    public void ReturnStartScene()
    {
        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "Driver_Car_E1")
        {
            SceneManager.LoadScene("E1");
        }
        if (nowscenename == "Driver_Car_E2")
        {
            SceneManager.LoadScene("E2");
        }
        if (nowscenename == "Driver_Car_E3")
        {
            SceneManager.LoadScene("E3");
        }
        if (nowscenename == "Driver_Car_E4")
        {
            SceneManager.LoadScene("E4");
        }
    }

    public void QuitScene()
    {
        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "Driver_Car_E1")
        {
            SceneManager.LoadScene("E1_End");
        }
        if (nowscenename == "Driver_Car_E2")
        {
            SceneManager.LoadScene("E2_End");
        }
        if (nowscenename == "Driver_Car_E3")
        {
            SceneManager.LoadScene("E3_End");
        }
        if (nowscenename == "Driver_Car_E4")
        {
            SceneManager.LoadScene("E4_End");
        }
    }

    public void BackOG()
    {
        SceneManager.LoadScene("Lobby");
    }

}
