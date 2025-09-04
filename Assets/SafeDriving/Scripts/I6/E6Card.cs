using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6Card : MonoBehaviour
{
    public GameObject r20;
    public GameObject r30;
    public GameObject r40;
    public GameObject r50;
    public GameObject r60;

    public static int E6CardValue;
    // Start is called before the first frame update
    void Start()
    {
        E6CardValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!r20.activeInHierarchy&& !r30.activeInHierarchy && !r40.activeInHierarchy && !r50.activeInHierarchy && !r60.activeInHierarchy)
        {
            E6CardValue = -1;
        }

        if (r20.activeInHierarchy)
        {
            E6CardValue = 0;
        }
        if (r30.activeInHierarchy)
        {
            E6CardValue = 1;
        }
        if (r40.activeInHierarchy)
        {
            E6CardValue = 2;
        }
        if (r50.activeInHierarchy)
        {
            E6CardValue = 3;
        }
        if (r60.activeInHierarchy)
        {
            E6CardValue = 4;
        }

        Debug.Log(E6CardValue);
    }
}
