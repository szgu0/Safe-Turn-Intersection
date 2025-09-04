using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCtrl : MonoBehaviour
{
    public PrometeoCarController car;

    public void SpeedSliderChange(float speed)
    {
        car.maxSpeed = (int)speed;
    }


   
}
