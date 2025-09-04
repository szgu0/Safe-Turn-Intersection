using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_Road_Chang : MonoBehaviour
{
    public GameObject r50d20;
    public GameObject r50d30;
    public GameObject r50d40;
    public GameObject r50d50;
    public GameObject r50d60;

    public GameObject r10d20;
    public GameObject r10d30;
    public GameObject r10d40;
    public GameObject r10d50;
    public GameObject r10d60;

    public GameObject r30d20;
    public GameObject r30d30;
    public GameObject r30d40;
    public GameObject r30d50;
    public GameObject r30d60;

    public GameObject r80d20;
    public GameObject r80d30;
    public GameObject r80d40;
    public GameObject r80d50;
    public GameObject r80d60;

    public GameObject r100d20;
    public GameObject r100d30;
    public GameObject r100d40;
    public GameObject r100d50;
    public GameObject r100d60;
    // Start is called before the first frame update
    void Start()
    {
        r100d20.SetActive(false);
        r100d50.SetActive(false);
        r100d30.SetActive(false);
        r100d40.SetActive(false);
        r100d60.SetActive(false);

        r10d20.SetActive(false);
        r10d50.SetActive(false);
        r10d30.SetActive(false);
        r10d40.SetActive(false);
        r10d60.SetActive(false);

        r50d20.SetActive(false);
        r50d50.SetActive(false);
        r50d30.SetActive(false);
        r50d40.SetActive(false);
        r50d60.SetActive(false);

        r30d20.SetActive(false);
        r30d50.SetActive(false);
        r30d30.SetActive(false);
        r30d40.SetActive(false);
        r30d60.SetActive(false);

        r80d20.SetActive(false);
        r80d50.SetActive(false);
        r80d30.SetActive(false);
        r80d40.SetActive(false);
        r80d60.SetActive(false);

        if (AppData.cardSelectNUM_2 == 0)
        {
            r100d20.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 1)
        {
            r100d30.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 2)
        {
            r100d40.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 3)
        {
            r100d50.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 4)
        {
            r100d60.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (selectCard.ChangeCardValue == 0)
        {
            r10d20.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 1)
        {
            r30d20.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 2)
        {
            r50d20.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 3)
        {
            r80d20.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 4)
        {
            r100d20.SetActive(true);
        }


        if (selectCard.ChangeCardValue == 5)
        {
            r10d30.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 6)
        {
            r30d30.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 7)
        {
            r50d30.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 8)
        {
            r80d30.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 9)
        {
            r100d30.SetActive(true);
        }

        if (selectCard.ChangeCardValue == 10)
        {
            r10d40.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 11)
        {
            r30d40.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 12)
        {
            r50d40.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 13)
        {
            r80d40.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 14)
        {
            r100d40.SetActive(true);
        }


        if (selectCard.ChangeCardValue == 15)
        {
            r10d50.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 16)
        {
            r30d50.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 17)
        {
            r50d50.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 18)
        {
            r80d50.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 19)
        {
            r100d50.SetActive(true);
        }

        if (selectCard.ChangeCardValue == 20)
        {
            r10d60.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 21)
        {
            r30d60.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 22)
        {
            r50d60.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 23)
        {
            r80d60.SetActive(true);
        }
        if (selectCard.ChangeCardValue == 24)
        {
            r100d60.SetActive(true);
        }
        */
    }
}
