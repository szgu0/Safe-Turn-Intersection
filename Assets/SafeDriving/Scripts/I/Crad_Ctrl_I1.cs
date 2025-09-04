using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crad_Ctrl_I1 : MonoBehaviour
{
    //public string ChooseCardName;

    //public RandomCtrl randomCtrl;

    public CardSelect cardSelect_1;

    public CardSelect cardSelect_2;

    public CardSelect cardSelect_3;


    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject); 
    }

    public void SetName1()
    {
        int card_1 = cardSelect_1.CardNum;
        AppData.ChooseCardName = cardSelect_1.CardNumObject[card_1].name;
        //Debug.Log("Card "+ )
        //ChooseCardName = randomCtrl.randomObject1.name;
    }

    public void SetName2()
    {
        int card_2 = cardSelect_2.CardNum;
        AppData.ChooseCardName = cardSelect_2.CardNumObject[card_2].name;
        //ChooseCardName = randomCtrl.randomObject1.name;
    }

    public void SetName3()
    {
        int card_3 = cardSelect_3.CardNum;
        AppData.ChooseCardName = cardSelect_3.CardNumObject[card_3].name;
        //ChooseCardName = randomCtrl.randomObject1.name;
    }









}
