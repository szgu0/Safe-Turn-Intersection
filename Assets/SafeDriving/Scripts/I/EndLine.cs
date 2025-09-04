using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{

    public GameObject B_1;
    public GameObject B_2;
    public GameObject B_3;

    public GameObject Wrad_1;
    public GameObject Wrad_2;

    public GameObject Wrad_3;
    public GameObject Wrad_4;

    public GameObject Wrad_5;
    public GameObject Wrad_6;

    public bool isOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            B_1.SetActive(true);
            B_2.SetActive(true);
            B_3.SetActive(true);

            Wrad_1.SetActive(false);
            Wrad_2.SetActive(true);
            Wrad_3.SetActive(false);
            Wrad_4.SetActive(true);
            Wrad_5.SetActive(false);
            Wrad_6.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            //B_1.SetActive(true);
            //B_2.SetActive(true);
            //B_3.SetActive(true);

            ////Wrad_1.SetActive(true);
            //Wrad_2.SetActive(true);
            ////Wrad_3.SetActive(true);
            //Wrad_4.SetActive(true);
            ////Wrad_5.SetActive(true);
            //Wrad_6.SetActive(true);

            isOver = true;
        }
    }

    public void ChangeSecne()
    {
        Application.LoadLevel("I1_End");
    }
}
