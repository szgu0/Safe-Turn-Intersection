using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectCard : MonoBehaviour
{
    
    public static int ChangeCardValue;
    public GameObject angle20_2;
    public GameObject angle20;
    public GameObject angle30;
    public GameObject angle40;
    public GameObject angle50;
    public GameObject angle60;

    public GameObject r10;
    public GameObject r30;
    public GameObject r50;
    public GameObject r80;
    public GameObject r100;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is callengd once per frame
    void Update()
    {
        Debug.Log(ChangeCardValue);


        if(!angle20_2.activeInHierarchy && !angle20.activeInHierarchy&& !angle30.activeInHierarchy && !angle40.activeInHierarchy && !angle50.activeInHierarchy && !angle60.activeInHierarchy &&!r10.activeInHierarchy&& !r30.activeInHierarchy && !r50.activeInHierarchy && !r80.activeInHierarchy && !r100.activeInHierarchy)
        {
            ChangeCardValue = -1;
        }

        //angle20 with all r
        if(angle20.activeInHierarchy&&r10.activeInHierarchy|| angle20_2.activeInHierarchy && r10.activeInHierarchy)
        {
            ChangeCardValue = 0;
        }
        if (angle20.activeInHierarchy && r30.activeInHierarchy || angle20_2.activeInHierarchy && r30.activeInHierarchy)
        {
            ChangeCardValue = 1;
        }
        if (angle20.activeInHierarchy && r50.activeInHierarchy || angle20_2.activeInHierarchy && r50.activeInHierarchy)
        {
            ChangeCardValue = 2;
        }
        if (angle20.activeInHierarchy && r80.activeInHierarchy || angle20_2.activeInHierarchy && r80.activeInHierarchy)
        {
            ChangeCardValue = 3;
        }
        if (angle20.activeInHierarchy && r100.activeInHierarchy || angle20_2.activeInHierarchy && r100.activeInHierarchy)
        {
            ChangeCardValue = 4;
        }

        //angle30 with all r
        if (angle30.activeInHierarchy && r10.activeInHierarchy)
        {
            ChangeCardValue = 5;
        }
        if (angle30.activeInHierarchy && r30.activeInHierarchy)
        {
            ChangeCardValue = 6;
        }
        if (angle30.activeInHierarchy && r50.activeInHierarchy)
        {
            ChangeCardValue = 7;
        }
        if (angle30.activeInHierarchy && r80.activeInHierarchy)
        {
            ChangeCardValue = 8;
        }
        if (angle30.activeInHierarchy && r100.activeInHierarchy)
        {
            ChangeCardValue = 9;
        }

        //angle40 with all r
        if (angle40.activeInHierarchy && r10.activeInHierarchy)
        {
            ChangeCardValue = 10;
        }
        if (angle40.activeInHierarchy && r30.activeInHierarchy)
        {
            ChangeCardValue = 11;
        }
        if (angle40.activeInHierarchy && r50.activeInHierarchy)
        {
            ChangeCardValue = 12;
        }
        if (angle40.activeInHierarchy && r80.activeInHierarchy)
        {
            ChangeCardValue = 13;
        }
        if (angle40.activeInHierarchy && r100.activeInHierarchy)
        {
            ChangeCardValue = 14;
        }

        //angle50 with all r
        if (angle50.activeInHierarchy && r10.activeInHierarchy)
        {
            ChangeCardValue = 15;
        }
        if (angle50.activeInHierarchy && r30.activeInHierarchy)
        {
            ChangeCardValue = 16;
        }
        if (angle50.activeInHierarchy && r50.activeInHierarchy)
        {
            ChangeCardValue = 17;
        }
        if (angle50.activeInHierarchy && r80.activeInHierarchy)
        {
            ChangeCardValue = 18;
        }
        if (angle50.activeInHierarchy && r100.activeInHierarchy)
        {
            ChangeCardValue = 19;
        }

        //angle60 with all r
        if (angle60.activeInHierarchy && r10.activeInHierarchy)
        {
            ChangeCardValue = 20;
        }
        if (angle60.activeInHierarchy && r30.activeInHierarchy)
        {
            ChangeCardValue = 21;
        }
        if (angle60.activeInHierarchy && r50.activeInHierarchy)
        {
            ChangeCardValue = 22;
        }
        if (angle60.activeInHierarchy && r80.activeInHierarchy)
        {
            ChangeCardValue = 23;
        }
        if (angle60.activeInHierarchy && r100.activeInHierarchy)
        {
            ChangeCardValue = 24;
        }


      /*  if (angle60.activeInHierarchy )
        {
            ChangeCardValue = 25;
        }
        if (angle60.activeInHierarchy )
        {
            ChangeCardValue = 26;
        }
        if (angle60.activeInHierarchy )
        {
            ChangeCardValue = 27;
        }
        if (angle60.activeInHierarchy)
        {
            ChangeCardValue = 28;
        }
        if (angle60.activeInHierarchy)
        {
            ChangeCardValue = 29;
        }*/
    }
}
