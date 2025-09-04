using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataSetting : MonoBehaviour
{
    public CardData cardData; //選擇的卡牌資料

    public bool isChoose;

    private void Start()
    {
        isChoose = true;
    }
}
