using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takecardvalue : MonoBehaviour
{
    public static int u;
    public static int r;

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
    // Start is called before the first frame update
    void Start()
    {
        u = 0;
        r = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(u1.activeInHierarchy)
        {
            u = 1;
        }
        if (u2.activeInHierarchy)
        {
            u = 2;
        }
        if (u3.activeInHierarchy)
        {
            u = 3;
        }
        if (u4.activeInHierarchy)
        {
            u = 4;
        }
        if (u5.activeInHierarchy)
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
    }
}
