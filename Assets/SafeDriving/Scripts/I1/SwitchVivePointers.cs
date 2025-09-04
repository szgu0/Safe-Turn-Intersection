using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchVivePointers : MonoBehaviour
{
    public CarPathFollower carPathFollower;
    public RoadChange roadChange;
    public e6 mye6;
    public I8RoadStart i8RoadStart;

    public GameObject VivePointers;

    string sceneName = "";
    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void switchPointer(bool value)
    {

        if (sceneName == "E1_Driver_Car" || sceneName == "E2_Driver_Car" || sceneName == "E3_Driver_Car" || sceneName == "E4_Driver_Car" || sceneName == "E5_Driver_Car" || sceneName == "E6_Driver_Car" || sceneName == "E7_Driver_Car" || sceneName == "E8_Driver_Car")
        {
            if (carPathFollower.isRun)
            {
                carPathFollower.isPause = value;
                VivePointers.SetActive(value);
            }
        }
        /*
        else if (sceneName == "E5_Driver_Car")
        {
            if (roadChange.isRun)
            {
                roadChange.isPause = value;
                VivePointers.SetActive(value);
            }
        }
        else if (sceneName == "E6_Driver_Car")
        {
            if (mye6.isRun)
            {
                mye6.isPause = value;
                VivePointers.SetActive(value);
            }
        }
        */
        /*
        else if (sceneName == "E8_Driver_Car")
        {
            if (i8RoadStart.isRun)
            {
                i8RoadStart.isPause = value;
                VivePointers.SetActive(value);
            }
        }
        */


    }
}
