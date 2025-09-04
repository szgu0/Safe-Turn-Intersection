using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceSwitchCtrl : MonoBehaviour
{
    public string nowscenename = "";

    public void ChangeToDriveSence()
    {
        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "E1")
        {
            SceneManager.LoadScene("E1_Driver_Car");
        }
        if (nowscenename == "E2")
        {
            SceneManager.LoadScene("E2_Driver_Car");
        }
        if (nowscenename == "E3")
        {
            SceneManager.LoadScene("E3_Driver_Car");
        }
        if (nowscenename == "E4")
        {
            SceneManager.LoadScene("E4_Driver_Car");
        }
        if (nowscenename == "E5")
        {
            SceneManager.LoadScene("E5_Driver_Car");
        }

        if (nowscenename == "E6")
        {
            SceneManager.LoadScene("E6_Driver_Car");
        }
        if (nowscenename == "E7")
        {
            SceneManager.LoadScene("E7_Driver_Car");
        }
        if (nowscenename == "E8")
        {
            SceneManager.LoadScene("E8_Driver_Car");
        }
    }
    public void ReStartScene()
    {
        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "E1_Driver_Car" || nowscenename == "E1_End")
        {
            SceneManager.LoadScene("E1");
        }
        if (nowscenename == "E2_Driver_Car" || nowscenename == "E2_End")
        {
            SceneManager.LoadScene("E2");
        }
        if (nowscenename == "E3_Driver_Car" || nowscenename == "E3_End")
        {
            SceneManager.LoadScene("E3");
        }
        if (nowscenename == "E4_Driver_Car" || nowscenename == "E4_End")
        {
            SceneManager.LoadScene("E4");
        }
        if (nowscenename == "E5_Driver_Car" || nowscenename == "E5_End")
        {
            SceneManager.LoadScene("E5");
        }
        if (nowscenename == "E6_Driver_Car" || nowscenename == "E6_End")
        {
            SceneManager.LoadScene("E6");
        }
        if (nowscenename == "E7_Driver_Car" || nowscenename == "E7_End")
        {
            SceneManager.LoadScene("E7");
        }
        if (nowscenename == "E8_Driver_Car" || nowscenename == "E8_End")
        {
            SceneManager.LoadScene("E8");
        }
    }

    public void QuitScene()
    {
        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "E1_Driver_Car")
        {
            SceneManager.LoadScene("E1_End");
        }
        if (nowscenename == "E2_Driver_Car")
        {
            SceneManager.LoadScene("E2_End");
        }
        if (nowscenename == "E3_Driver_Car")
        {
            SceneManager.LoadScene("E3_End");
        }
        if (nowscenename == "E4_Driver_Car")
        {
            SceneManager.LoadScene("E4_End");
        }
        if (nowscenename == "E5_Driver_Car")
        {
            SceneManager.LoadScene("E5_End");
        }
        if (nowscenename == "E6_Driver_Car")
        {
            SceneManager.LoadScene("E6_End");
        }
        if (nowscenename == "E7_Driver_Car")
        {
            SceneManager.LoadScene("E7_End");
        }
        if (nowscenename == "E8_Driver_Car")
        {
            SceneManager.LoadScene("E8_End");
        }
    }

    public void BackOG()
    {
        SceneManager.LoadScene("Lobby");
    }

}
