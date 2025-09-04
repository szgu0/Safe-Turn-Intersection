using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChoose : MonoBehaviour
{
    public GameObject R_1;
    public GameObject R_2;
    public GameObject R_3;
    public GameObject R_4;
    public GameObject R_5;

    //public GameObject R_Path_1;
    //public GameObject R_Path_2;
    //public GameObject R_Path_3;
    //public GameObject R_Path_4;
    //public GameObject R_Path_5;

    public bool path_1;
    public bool path_2;
    public bool path_3;
    public bool path_4;
    public bool path_5;

    //public Crad_Ctrl crad_Ctrl;

    public string Road_1 = "Card_0";
    public string Road_2 = "Card_1";
    public string Road_3 = "Card_2";
    public string Road_4 = "Card_3";
    public string Road_5 = "Card_4";

    public bool isR_1;
    public bool isR_2;
    public bool isR_3;
    public bool isR_4;
    public bool isR_5;

    // Start is called before the first frame update
    void Start()
    {
        //crad_Ctrl = GameObject.Find("Crad_Ctrl").GetComponent<Crad_Ctrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AppData.ChooseCardName == Road_1)
        {
            R_1.SetActive(true);
            R_2.SetActive(false);
            R_3.SetActive(false);
            R_4.SetActive(false);
            R_5.SetActive(false);

            path_1 = true;

            isR_1 = true;
            isR_2 = false;
            isR_3 = false;
            isR_4 = false;
            isR_5 = false;

            //R_Path_1.SetActive(true);
            //R_Path_2.SetActive(false);
            //R_Path_3.SetActive(false);
            //R_Path_4.SetActive(false);
            //R_Path_5.SetActive(false);

        }

        else if (AppData.ChooseCardName == Road_2)
        {
            R_1.SetActive(false);
            R_2.SetActive(true);
            R_3.SetActive(false);
            R_4.SetActive(false);
            R_5.SetActive(false);

            path_2 = true;

            isR_2 = true;

            //R_Path_1.SetActive(false);
            //R_Path_2.SetActive(true);
            //R_Path_3.SetActive(false);
            //R_Path_4.SetActive(false);
            //R_Path_5.SetActive(false);
        }

        else if (AppData.ChooseCardName == Road_3)
        {
            R_1.SetActive(false);
            R_2.SetActive(false);
            R_3.SetActive(true);
            R_4.SetActive(false);
            R_5.SetActive(false);

            path_3 = true;

            isR_3 = true;

            //R_Path_1.SetActive(false);
            //R_Path_2.SetActive(false);
            //R_Path_3.SetActive(true);
            //R_Path_4.SetActive(false);
            //R_Path_5.SetActive(false);
        }

        else if (AppData.ChooseCardName == Road_4)
        {
            R_1.SetActive(false);
            R_2.SetActive(false);
            R_3.SetActive(false);
            R_4.SetActive(true);
            R_5.SetActive(false);

            path_4 = true;

            isR_4 = true;

            //R_Path_1.SetActive(false);
            //R_Path_2.SetActive(false);
            //R_Path_3.SetActive(false);
            //R_Path_4.SetActive(true);
            //R_Path_5.SetActive(false);
        }

        else if (AppData.ChooseCardName == Road_5)
        {
            R_1.SetActive(false);
            R_2.SetActive(false);
            R_3.SetActive(false);
            R_4.SetActive(false);
            R_5.SetActive(true);

            path_5 = true;

            isR_5 = true;

            //R_Path_1.SetActive(false);
            //R_Path_2.SetActive(false);
            //R_Path_3.SetActive(false);
            //R_Path_4.SetActive(false);
            //R_Path_5.SetActive(true);
        }
    }
}
