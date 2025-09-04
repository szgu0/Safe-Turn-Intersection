using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardExp : MonoBehaviour
{
    public CardSelect cardSelect_1;
    public CardSelect cardSelect_2;
    public CardSelect cardSelect_3;
    public InputField[] cardInputDataText;
    public float cardInputData = 0.0f;
    public string nowscenename = "";

    void Start()
    {
        TalesFromTheRift.CanvasKeyboard.Close();
        gameObject.SetActive(false);
        cardInputDataText[0].text = "0";
        cardInputDataText[1].text = "0";
        cardInputDataText[2].text = "0";

        nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "I1" || nowscenename == "E1")
        {
            cardSelect_1.CardType = CardTypeData.道路_1;
            cardSelect_2.CardType = CardTypeData.道路_1;
            cardSelect_3.CardType = CardTypeData.道路_1;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 1;
            cardSelect_3.CardNum = 3;
        }
        if (nowscenename == "I2" || nowscenename == "E2")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.迴轉半徑;
            cardSelect_3.CardType = CardTypeData.NULL;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 2;
            cardSelect_3.CardNum = 0;
        }
        if (nowscenename == "I3" || nowscenename == "E3")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.道路_1;
            cardSelect_3.CardType = CardTypeData.NULL;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 2;
            cardSelect_3.CardNum = 0;
        }
        if (nowscenename == "I4" || nowscenename == "E4")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.摩擦係數;
            cardSelect_3.CardType = CardTypeData.迴轉半徑;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 1;
            cardSelect_3.CardNum = 3;
        }
        if (nowscenename == "I5" || nowscenename == "E5")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.超高角度;
            cardSelect_3.CardType = CardTypeData.迴轉半徑;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 4;
            cardSelect_3.CardNum = 4;
        }

        if (nowscenename == "I6" || nowscenename == "E6")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.道路_3;
            cardSelect_3.CardType = CardTypeData.NULL;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 2;
            cardSelect_3.CardNum = 0;
        }
        if (nowscenename == "I7" || nowscenename == "E7")
        {
            cardSelect_1.CardType = CardTypeData.車輛;
            cardSelect_2.CardType = CardTypeData.迴轉半徑;
            cardSelect_3.CardType = CardTypeData.NULL;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 2;
            cardSelect_3.CardNum = 0;
        }

        if (nowscenename == "I8" || nowscenename == "E8")
        {
            cardSelect_1.CardType = CardTypeData.道路_2;
            cardSelect_2.CardType = CardTypeData.摩擦係數;
            cardSelect_3.CardType = CardTypeData.道路_2;
            cardSelect_1.CardNum = 0;
            cardSelect_2.CardNum = 2;
            cardSelect_3.CardNum = 0;
        }

        AppData.selectCard_1 = cardSelect_1.SelectCardData;
        AppData.selectCard_2 = cardSelect_2.SelectCardData;
        AppData.selectCard_3 = cardSelect_3.SelectCardData;
        AppData.selectCard = cardSelect_1.SelectCardData;
        AppData.cardSelectNUM_1 = -1;
        AppData.cardSelectNUM_2 = -1;
        AppData.cardSelectNUM_3 = -1;
    }

    public void toCheckSelectCard()
    {
        //確認最後的卡牌
        /*
        AppData.cardSelect_1 = cardSelect_1;
        AppData.cardSelect_2 = cardSelect_2;
        AppData.cardSelect_3 = cardSelect_3;
        */
        /*
        myApp.instance.cardSelect_1 = cardSelect_1;
        myApp.instance.cardSelect_2 = cardSelect_2;
        myApp.instance.cardSelect_3 = cardSelect_3;
        */
        //選擇的卡牌
        AppData.selectCard_1 = cardSelect_1.SelectCardData;
        AppData.selectCard_2 = cardSelect_2.SelectCardData;
        AppData.selectCard_3 = cardSelect_3.SelectCardData;

        AppData.selectCard_1.SelectNum = cardSelect_1.CardNum;
        AppData.selectCard_2.SelectNum = cardSelect_2.CardNum;
        AppData.selectCard_3.SelectNum = cardSelect_3.CardNum;

        AppData.cardSelectNUM_1 = cardSelect_1.CardNum;
        AppData.cardSelectNUM_2 = cardSelect_2.CardNum;
        AppData.cardSelectNUM_3 = cardSelect_3.CardNum;


        //AppData.selectCard 用在道路選擇
        if (nowscenename == "E1")
        {

        }else if (nowscenename == "E2")
        {
            AppData.selectCard = cardSelect_2.SelectCardData;
        }
        else if (nowscenename == "E3")
        {
            AppData.selectCard = cardSelect_2.SelectCardData;
        }
        else if (nowscenename == "E4")
        {
            AppData.selectCard = cardSelect_3.SelectCardData;
        }
        else if (nowscenename == "E5")
        {
            //有組合
            AppData.selectCard = cardSelect_2.SelectCardData;
        }
        else if (nowscenename == "E6")
        {
            AppData.selectCard = cardSelect_2.SelectCardData;
        }
        else if (nowscenename == "E7")
        {
            AppData.selectCard = cardSelect_2.SelectCardData;
        }
        else if (nowscenename == "E8")
        {
            AppData.selectCard = cardSelect_1.SelectCardData;
        }
    }

    public void SelectCardName(int CardIndex)
    {
        SelectAllCard();

        //For E1
        //選擇的卡牌
        if (CardIndex == 1)
        {
            AppData.ChooseCardName = cardSelect_1.CardNumObject[cardSelect_1.CardNum].name;
            AppData.selectCard = cardSelect_1.SelectCardData;
            if (cardInputDataText[0] != null)
            {
                AppData.selectCardInputData = Convert.ToSingle(cardInputDataText[0].text);
            }
        }
        if (CardIndex == 2)
        {
            AppData.ChooseCardName = cardSelect_2.CardNumObject[cardSelect_2.CardNum].name;
            AppData.selectCard = cardSelect_2.SelectCardData;
            if (cardInputDataText[1] != null)
            {
                AppData.selectCardInputData = Convert.ToSingle(cardInputDataText[1].text);
            }
        }
        if (CardIndex == 3)
        {
            AppData.ChooseCardName = cardSelect_3.CardNumObject[cardSelect_3.CardNum].name;
            AppData.selectCard = cardSelect_3.SelectCardData;
            if (cardInputDataText[2] != null)
            {
                AppData.selectCardInputData = Convert.ToSingle(cardInputDataText[2].text);
            }
        }
    }

    public void SelectAllCard()
    {
        //選擇的卡牌
        AppData.selectCard_1 = cardSelect_1.SelectCardData;
        AppData.selectCard_2 = cardSelect_2.SelectCardData;
        AppData.selectCard_3 = cardSelect_3.SelectCardData;
    }

    public void CardRandom()
    {
        /*
        if (nowscenename != "I1" && nowscenename != "E1")
        {
            return;
        }
        */
        int[] cardnum = { 0, 0, 0 };

        cardnum[0] = UnityEngine.Random.Range(0, cardSelect_1.CardNumObject.Length-1);
        do
        {
            cardnum[1] = UnityEngine.Random.Range(0, cardSelect_2.CardNumObject.Length - 1);
        } while (cardnum[0] == cardnum[1]);
        do
        {
            cardnum[2] = UnityEngine.Random.Range(0, cardSelect_3.CardNumObject.Length - 1);
        } while (cardnum[0] == cardnum[2] || cardnum[1] == cardnum[2]);

        cardSelect_1.CardNum = cardnum[0];
        cardSelect_2.CardNum = cardnum[1];
        cardSelect_3.CardNum = cardnum[2];
    }
}
