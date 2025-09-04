using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I1_EndWord : MonoBehaviour
{
    public GameObject W1;
    public GameObject W2;
    public GameObject W3;

    public GameObject B1;
    public GameObject B2;
    public GameObject B3;
    public GameObject B4;

    public GameObject B_Re;
    public GameObject B_Lift;
    public GameObject B_Menu;

    public GameObject B_Next;

    public int WordCount;

    public void ChangeWord()
    {
        WordCount++;
        if(WordCount == 1)
        {
            W1.SetActive(false);
            W2.SetActive(true);

            B_Next.SetActive(false);
            B_Re.SetActive(true);
            B_Lift.SetActive(true);
        }

        //else if(WordCount == 2)
        //{
        //    W2.SetActive(false);
        //    W3.SetActive(true);

        //    B_Re.SetActive(false);
        //    B_Lift.SetActive(false);

        //    B_Menu.SetActive(true);
        //}

        //else if(WordCount == 3)
        //{
        //    W3.SetActive(false);

        //    B_Menu.SetActive(false);

        //    B1.SetActive(true);
        //    B2.SetActive(true);
        //    B3.SetActive(true);
        //    B4.SetActive(true);
        //}
    }
}
