using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I4CurrentValue : MonoBehaviour
{
    public static int u;
    public static int r;
    public static int car;

    public GameObject u0;
    public GameObject u1;
    public GameObject u2;
    public GameObject u3;
    public GameObject u4;
    public GameObject u5;

    public GameObject r1;
    public GameObject r2;
    public GameObject r3;
    public GameObject r4;
    public GameObject r5;

    public GameObject CAR1;
    public GameObject CAR2;
    public GameObject CAR3;
    public GameObject CAR4;
    public GameObject CAR5;
    // Start is called before the first frame update
    void Start()
    {
        u = -1;
        r = 0;
        car = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (u0.activeInHierarchy)
        {
            u = 0;
        }
        if (u1.activeInHierarchy)
        {
            u = 1;
        }
        if(u2.activeInHierarchy)
        {
            u = 2;
        }
        if(u3.activeInHierarchy)
        {
            u = 3;
        }
        if (u4.activeInHierarchy)
        {
            u = 4;
        }
        if(u5.activeInHierarchy)
        {
            u = 5;
        }



        if (r1.activeInHierarchy)
        {
            r = 1;
        }
        if (r2.activeInHierarchy)
        {
            r = 2;
        }
        if (r3.activeInHierarchy)
        {
            r = 3;
        }
        if (r4.activeInHierarchy)
        {
            r = 4;
        }
        if (r5.activeInHierarchy)
        {
            r = 5;
        }


        if (CAR1.activeInHierarchy)
        {
            car = 1;
        }
        if (CAR2.activeInHierarchy)
        {
            car = 2;
        }
        if (CAR3.activeInHierarchy)
        {
            car = 3;
        }
        if (CAR4.activeInHierarchy)
        {
            car = 4;
        }
        if (CAR5.activeInHierarchy)
        {
            car= 5;
        }
    }
}
