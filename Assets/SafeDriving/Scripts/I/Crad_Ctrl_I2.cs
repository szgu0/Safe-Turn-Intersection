

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crad_Ctrl_2 : MonoBehaviour
{
    public CardSelect cardSelect;

    public CardSelect carSelect;

    private void Update()
    {
        SetName1();
        SetName2();
    }

    public void SetName1()
    {
        int card_1 = cardSelect.CardNum;
        AppData.ChooseCardName = cardSelect.CardNumObject[card_1].name;
    }

    public void SetName2()
    {
        int card_2 = carSelect.CardNum;
        AppData.ChooseCarName = carSelect.CardNumObject[card_2].name;
    }
}
