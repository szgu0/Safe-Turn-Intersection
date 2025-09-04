using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crad_Ctrl_I4 : MonoBehaviour
{
    //public string ChooseCardName;

    //public RandomCtrl randomCtrl;

    public CardSelect cardSelect;

    public CardSelect carSelect;

    public CardSelect frictionSelect;


    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject); 
    }

    private void Update()
    {
        SetName1();
        SetName2();
        SetName3();
    }

    public void SetName1()
    {
        int card_1 = cardSelect.CardNum;
        AppData.ChooseCardName = cardSelect.CardNumObject[card_1].name;
        //ChooseCardName = randomCtrl.randomObject1.name;
    }

    public void SetName2()
    {
        int card_2 = carSelect.CardNum;
        AppData.ChooseCarName = carSelect.CardNumObject[card_2].name;
        //ChooseCardName = randomCtrl.randomObject1.name;
    }

    public void SetName3()
    {
        int card_3 = frictionSelect.CardNum;
        AppData.ChooseFrictionName = frictionSelect.CardNumObject[card_3].name;
        //ChooseCardName = randomCtrl.randomObject1.name;
    }









}
