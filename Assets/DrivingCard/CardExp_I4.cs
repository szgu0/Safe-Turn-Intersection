using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExp_I4 : MonoBehaviour
{
    public CardSelect cardSelect_1;
    public CardSelect cardSelect_2;
    public CardSelect cardSelect_3;

    void Start()
    {
        cardSelect_1.CardType = CardTypeData.車輛;
        cardSelect_2.CardType = CardTypeData.摩擦係數;
        cardSelect_3.CardType = CardTypeData.迴轉半徑;
        cardSelect_1.CardNum = 0;
        cardSelect_2.CardNum = 1;
        cardSelect_3.CardNum = 3;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
