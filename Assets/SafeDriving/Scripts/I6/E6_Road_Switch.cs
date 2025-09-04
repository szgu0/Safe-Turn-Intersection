using RoadArchitect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_Road_Switch : MonoBehaviour
{
    public GameObject R20;
    public GameObject R30;
    public GameObject R40;
    public GameObject R50;
    public GameObject R60;
    // Start is called before the first frame update
    void Start()
    {
        R20.SetActive(false);
        R30.SetActive(false);
        R40.SetActive(false);
        R50.SetActive(false);
        R60.SetActive(false);
        if (AppData.cardSelectNUM_2 == 0)
        {
            R20.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 1)
        {
            R30.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 2)
        {
            R40.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 3)
        {
            R50.SetActive(true);
        }
        if (AppData.cardSelectNUM_2 == 4)
        {
            R60.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (E6Card.E6CardValue == 0)
        {
            R20.SetActive(true);
        }
        if (E6Card.E6CardValue == 1)
        {
            R30.SetActive(true);
        }
         if (E6Card.E6CardValue == 2)
        {
            R40.SetActive(true);
        }
        if (E6Card.E6CardValue == 3)
        {
            R50.SetActive(true);
        }
        if (E6Card.E6CardValue == 4)
        {
            R60.SetActive(true);
        }
        */
    }
}
