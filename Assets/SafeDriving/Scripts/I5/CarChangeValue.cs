using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChangeValue : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject car4;
    public GameObject car5;

    public static int CarChangeValue1;
    // Start is called before the first frame update
    void Start()
    {
        CarChangeValue1 = -1;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CarChangeValue1);

        if (car1.activeInHierarchy)
        {
            CarChangeValue1 = 0;
        }
        if (car2.activeInHierarchy)
        {
            CarChangeValue1 = 1;
        }
        if (car3.activeInHierarchy)
        {
            CarChangeValue1 = 2;
        }
        if (car4.activeInHierarchy)
        {
            CarChangeValue1 = 3;
        }
        if (car5.activeInHierarchy)
        {
            CarChangeValue1 = 4;
        }
    }
}
