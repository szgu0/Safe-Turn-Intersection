using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    CardTypeData _CardType = 0;
    int _CardNum = 0;
    CardData _SelectCardData = null;

    public Text LabelText;
    public GameObject[] CardTypeObject;//卡牌類型
    public GameObject[] CardNumObject;//同一類型下的所有卡牌

    public CardTypeData CardType //0~8
    {
        get
        {
            return _CardType;
        }
        set
        {
            _CardType = value;
            showCardType(value);
        }

    }

    public int CardNum
    {
        get
        {
            return _CardNum;
        }
        set
        {
            _CardNum = value;
            showCardNum(value);
        }
    }
    public CardData SelectCardData
    {
        get
        {
            //_SelectCardData = CardNumObject[_CardNum].GetComponent<CardDataSetting>().cardData;
            return _SelectCardData;
        }
    }

    void Start()
    {
        //CardType = 1;
    }

    public void showCardType(CardTypeData type)
    {
        int num = (int)type;

        //顯示某一類型的卡牌
        if (num < 0)
        {
            num = 0;
        }
        if (num >= CardTypeObject.Length)
        {
            num = CardTypeObject.Length - 1;
        }
        for (int i = 0; i < CardTypeObject.Length; i++)
        {
            CardTypeObject[i].SetActive(false);
        }
        CardTypeObject[num].SetActive(true);

        _CardType = (CardTypeData)num;

        LabelText.text = Enum.GetName(typeof(CardTypeData), num);

        //找出此類別的所有卡牌
        CardNumObject = new GameObject[CardTypeObject[num].transform.childCount];
        for (int i = 0; i < CardNumObject.Length; i++)
        {
            CardNumObject[i] = CardTypeObject[num].transform.GetChild(i).gameObject;
        }
        CardNum = 0;

        if (num == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void showCardNum(int num)
    {
        //顯示此類型的其中一張卡牌
        if (num < 0)
        {
            num = 0;
        }
        if (num >= CardNumObject.Length)
        {
            num = CardNumObject.Length - 1;
        }
        for (int i = 0; i < CardNumObject.Length; i++)
        {
            CardNumObject[i].SetActive(false);
            if(CardNumObject[i].name.EndsWith(num.ToString()))
            {
                CardNumObject[i].SetActive(true);
                _SelectCardData = CardNumObject[i].GetComponent<CardDataSetting>().cardData;
                //_SelectCardData.SelectNum = num;
            }
        }

        //CardNumObject[num].SetActive(true);
        _CardNum = num;
    }

    public void BTN_L()
    {
        //往左按
        _CardNum = _CardNum - 1;
        if (_CardNum < 0)
        {
            _CardNum = CardNumObject.Length - 1;
        }
        CardNum = _CardNum;
    }

    public void BTN_R()
    {
        //往右按
        _CardNum = _CardNum + 1;
        if (_CardNum >= CardNumObject.Length)
        {
            _CardNum = 0;
        }
        CardNum = _CardNum;
    }


}
