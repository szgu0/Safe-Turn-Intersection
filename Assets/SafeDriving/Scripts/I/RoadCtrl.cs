using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCtrl : MonoBehaviour
{
    public GameObject R1_1;
    public GameObject R1_2;
    public GameObject R1_3;

    public int R1Count;

    public GameObject R2_1;
    public GameObject R2_2;
    public GameObject R2_3;

    public int R2Count;

    public GameObject R3_1;
    public GameObject R3_2;
    public GameObject R3_3;

    public int R3Count;

    public bool isRoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoad)
        {
            if (R1Count == 0)
            {
                R1_1.SetActive(true);
                R1_2.SetActive(false);
                R1_3.SetActive(false);
            }

            else if (R1Count == 1)
            {
                R1_1.SetActive(false);
                R1_2.SetActive(true);
                R1_3.SetActive(false);
            }

            else if (R1Count == 2)
            {
                R1_1.SetActive(false);
                R1_2.SetActive(false);
                R1_3.SetActive(true);
            }

            if (R2Count == 0)
            {
                R2_1.SetActive(true);
                R2_2.SetActive(false);
                R2_3.SetActive(false);
            }

            else if (R2Count == 1)
            {
                R2_1.SetActive(false);
                R2_2.SetActive(true);
                R2_3.SetActive(false);
            }

            else if (R2Count == 2)
            {
                R2_1.SetActive(false);
                R2_2.SetActive(false);
                R2_3.SetActive(true);
            }


            if (R3Count == 0)
            {
                R3_1.SetActive(true);
                R3_2.SetActive(false);
                R3_3.SetActive(false);
            }

            else if (R3Count == 1)
            {
                R3_1.SetActive(false);
                R3_2.SetActive(true);
                R3_3.SetActive(false);
            }

            else if (R3Count == 2)
            {
                R3_1.SetActive(false);
                R3_2.SetActive(false);
                R3_3.SetActive(true);
            }
        }
    }

    public void R1ChangePuls()
    {
        if (R1Count < 3)
        {
            R1Count++;
            if(R1Count == 3)
            {
                R1Count = 2;
            }
        }
    }

    public void R1ChangeDelete()
    {
        R1Count--;

        if(R1Count <= 0)
        {
            R1Count = 0;
        }
    }

    public void R2ChangePuls()
    {
        if (R2Count < 3)
        {
            R2Count++;
            if (R2Count == 3)
            {
                R2Count = 2;
            }
        }
    }

    public void R2ChangeDelete()
    {
        R2Count--;

        if (R2Count <= 0)
        {
            R2Count = 0;
        }
    }

    public void R3ChangePuls()
    {
        if (R3Count < 3)
        {
            R3Count++;
            if (R3Count == 3)
            {
                R3Count = 2;
            }
        }
    }

    public void R3ChangeDelete()
    {
        R3Count--;

        if (R3Count <= 0)
        {
            R3Count = 0;
        }
    }
}
