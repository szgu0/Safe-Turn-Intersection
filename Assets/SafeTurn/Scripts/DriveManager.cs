using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DriveManager : MonoBehaviour
{
    public CarController busCarController;
    public GameObject outUI;
    public void StartPlay()
    {
        busCarController.StartMoving();

    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Leave()
    {
        SceneManager.LoadScene(0);
    }

    void LateUpdate()
    {
        if (busCarController.coroutineFinished)
        {
            outUI.SetActive(true);
            busCarController.CarStopHard();
        }
    }
}
