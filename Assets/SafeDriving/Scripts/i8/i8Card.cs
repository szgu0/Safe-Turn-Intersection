using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i8Card : MonoBehaviour
{
    public GameObject card0;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public static int value;
    // Start is called before the first frame update
    void Start()
    {
        value = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (card1.activeInHierarchy)
        {
            value = 0;
        }
        if (card0.activeInHierarchy || card3.activeInHierarchy)
        {
            value = 1;
        }
        if (card2.activeInHierarchy||card4.activeInHierarchy)
        {
            value = 2;
        }

    }
}
