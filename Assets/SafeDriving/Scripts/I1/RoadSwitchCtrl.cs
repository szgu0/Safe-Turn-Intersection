using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class RoadSwitchCtrl : MonoBehaviour
{
 
    public GameObject[] R1;
    public GameObject[] R2;
    public GameObject[] R3;


    void Start()
    {
        //AppData.selectCard.cardTypeData == CardTypeData.道路_1
        if (AppData.selectCard != null)
        {
            ChangeRoad(AppData.selectCard.cardTypeData);
        }

        /*
        if (AppData.ChooseCardName != "" && AppData.ChooseCardName != null)
        {
            ChangeRoad(AppData.ChooseCardName);
        }
        else
        {
            ChangeRoad("Card_0");
        }
        */
    }

    public void ChangeRoad(CardTypeData cardTypeData)
    {
        if (R1.Length > 0)
        {
            for (int i = 0; i < R1.Length; i++)
            {
                R1[i].SetActive(false);
            }
        }
        if (R2.Length > 0)
        {
            for (int i = 0; i < R2.Length; i++)
            {
                R2[i].SetActive(false);
            }
        }
        if (R3.Length > 0)
        {
            for (int i = 0; i < R3.Length; i++)
            {
                R3[i].SetActive(false);
            }
        }

        if (AppData.selectCard.cardTypeData == CardTypeData.道路_1)
        {
            //E1, E3
            R1[(int)AppData.selectCard.road_1 - 1].SetActive(true);
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.迴轉半徑)
        {
            //E2, E4
            R1[(int)AppData.selectCard.road_0_R - 1].SetActive(true);
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.道路_2)
        {
            R2[(int)AppData.selectCard.road_2 - 1].SetActive(true);
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.道路_3)
        {
            R3[(int)AppData.selectCard.road_3 - 1].SetActive(true);
        }

    }

}
