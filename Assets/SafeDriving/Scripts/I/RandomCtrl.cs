using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCtrl : MonoBehaviour
{
    public GameObject[] group1;
    public GameObject[] group2;
    public GameObject[] group3;

    public int randomObject1;
    public int randomObject2;
    public int randomObject3;

    public CardSelect cardSelect1;
    public CardSelect cardSelect2;
    public CardSelect cardSelect3;

    private HashSet<int> selectedIndices = new HashSet<int>();

    public void CardRandom()
    {
        selectedIndices.Clear(); // 清空之前選中的索引

        // 隨機選取每組中的一個物體並確保不重複
        randomObject1 = SelectUniqueRandomObject(group1);
        randomObject2 = SelectUniqueRandomObject(group2);
        randomObject3 = SelectUniqueRandomObject(group3);

        cardSelect1.showCardNum(randomObject1);
        cardSelect2.showCardNum(randomObject2);
        cardSelect3.showCardNum(randomObject3);

        // 輸出選取的物體名稱
        Debug.Log("Selected object from group 1: " + group1[randomObject1].name);
        Debug.Log("Selected object from group 2: " + group2[randomObject2].name);
        Debug.Log("Selected object from group 3: " + group3[randomObject3].name);
    }

    int SelectUniqueRandomObject(GameObject[] group)
    {
        int randomIndex = -1;
        do
        {
            randomIndex = Random.Range(0, group.Length);
        } while (selectedIndices.Contains(randomIndex));

        selectedIndices.Add(randomIndex);
        return randomIndex;
    }


}
