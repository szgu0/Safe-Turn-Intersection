using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsCtrl : MonoBehaviour
{
    public RoadCtrl roadCtrl;

    public GameObject titleW;
    public GameObject W1;
    public GameObject W2;
    public GameObject W2_5;
    public GameObject W3;
    public GameObject W4;

    public GameObject P1;
    public GameObject P1_2;
    public GameObject P1_3;
    public GameObject P2,P2_2,P2_3;
    public GameObject P3,P3_2,P3_3;

    public GameObject B1_1;
    public GameObject B1_2;
    public GameObject B2_1;
    public GameObject B2_2;
    public GameObject B3_1;
    public GameObject B3_2;

    public GameObject B_M1;
    public GameObject B_M2;
    public GameObject B_M3;
    public GameObject B_M4;
    public GameObject B_M5;

    public int wordCount;

    private void Awake()
    {
        
    }

    public void ChangeWords()
    {
        wordCount++;
        if (wordCount == 1)
        {
            W1.SetActive(false);
            W2.SetActive(true);
            //W1.GetComponent<Text>().text = "內容";
        }

        else if (wordCount == 2)
        {
            roadCtrl.isRoad = true;

            titleW.SetActive(false);
            W2.SetActive(false);
            W2_5.SetActive(true);
            P1.SetActive(true);
            P2.SetActive(true);
            P3.SetActive(true);

            B1_1.SetActive(true);
            B1_2.SetActive(true);
            B2_1.SetActive(true);
            B2_2.SetActive(true);
            B3_1.SetActive(true);
            B3_2.SetActive(true);
        }

        else if (wordCount == 3)
        {
            roadCtrl.isRoad = false;
            W2_5.SetActive(false);
            W3.SetActive(true);

            //P1.SetActive(false);
            //P1_2.SetActive(false);
            //P1_3.SetActive(false);
            //P2.SetActive(false);
            //P2_2.SetActive(false);
            //P2_3.SetActive(false);
            //P3.SetActive(false);
            //P3_2.SetActive(false);
            //P3_3.SetActive(false);

            B1_1.SetActive(false);
            B1_2.SetActive(false);
            B2_1.SetActive(false);
            B2_2.SetActive(false);
            B3_1.SetActive(false);
            B3_2.SetActive(false);
        }

        else if (wordCount == 4)
        {
            P1.SetActive(false);
            P1_2.SetActive(false);
            P1_3.SetActive(false);
            P2.SetActive(false);
            P2_2.SetActive(false);
            P2_3.SetActive(false);
            P3.SetActive(false);
            P3_2.SetActive(false);
            P3_3.SetActive(false);

            W3.SetActive(false);
            W4.SetActive(true);
        }

        else if (wordCount == 5)
        {
            W4.SetActive(false);

            B_M1.SetActive(true);
            B_M2.SetActive(true);
            B_M3.SetActive(true);
            B_M4.SetActive(true);
            B_M5.SetActive(true);
        }

        else if(wordCount == 6)
        {
            Application.LoadLevel(3);
        }
    }
}
