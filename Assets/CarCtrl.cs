using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{


    public string Car_1 = "Card_0";
    public string Car_2 = "Card_1";
    public string Car_3 = "Card_2";
    public string Car_4 = "Card_3";
    public string Car_5 = "Card_4";

    public bool isCar_1;
    public bool isCar_2;
    public bool isCar_3;
    public bool isCar_4;
    public bool isCar_5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AppData.ChooseCarName == Car_1)
        {
            isCar_1 = true;
        }

        else if (AppData.ChooseCarName == Car_2)
        {
            isCar_2 = true;
        }

        else if (AppData.ChooseCarName == Car_3)
        {
            isCar_3 = true;
        }

        else if (AppData.ChooseCarName == Car_4)
        {
            isCar_4 = true;
        }

        else if (AppData.ChooseCarName == Car_5)
        {
            isCar_5 = true;
        }
    }
}
