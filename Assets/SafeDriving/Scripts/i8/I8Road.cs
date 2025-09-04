using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I8Road : MonoBehaviour
{


    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    // Start is called before the first frame update
    void Start()
    {
        road1.SetActive(false);
        road2.SetActive(false);
        road3.SetActive(false);
        if (AppData.cardSelectNUM_1 == 0)
        {
            road1.SetActive(true);
        }
        if (AppData.cardSelectNUM_1 == 1)
        {
            road2.SetActive(true);
        }
        if (AppData.cardSelectNUM_1 == 2)
        {
            road3.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {




        /*
        if (i8Card.value == 0)
        {
            road1.SetActive(true);
        }
        if (i8Card.value == 1)
        {
            road2.SetActive(true);
        }
        if (i8Card.value == 2)
        {
            road3.SetActive(true);
        }
        */
    }
}
