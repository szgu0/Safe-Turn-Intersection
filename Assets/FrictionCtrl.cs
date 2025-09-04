using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionCtrl : MonoBehaviour
{
    public string Friction_0 = "Card_0";
    public string Friction_1 = "Card_1";
    public string Friction_2 = "Card_2";
    public string Friction_3 = "Card_3";
    public string Friction_4 = "Card_4";
    public string Friction_5 = "Card_5";

    public bool isFriction_0;
    public bool isFriction_1;
    public bool isFriction_2;
    public bool isFriction_3;
    public bool isFriction_4;
    public bool isFriction_5;

    // Update is called once per frame
    void Update()
    {
        if(AppData.ChooseFrictionName == Friction_0)
        {
            isFriction_0 = true;
        }

        else if (AppData.ChooseFrictionName == Friction_1)
        {
            isFriction_1 = true;
        }

        else if (AppData.ChooseFrictionName == Friction_2)
        {
            isFriction_2 = true;
        }

        else if (AppData.ChooseFrictionName == Friction_3)
        {
            isFriction_3 = true;
        }

        else if (AppData.ChooseFrictionName == Friction_4)
        {
            isFriction_4 = true;
        }

        else if (AppData.ChooseFrictionName == Friction_5)
        {
            isFriction_5 = true;
        }
    }
}
