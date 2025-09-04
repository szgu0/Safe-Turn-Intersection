using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCar : MonoBehaviour
{
    public GameObject[] cars; // cars[0] 對應 Car 1, cars[1] 對應 Car 2... cars[4] 對應 Car 5

    // 這裡的 CarChangeValue 是外部系統傳入的值 (1~5)
    

    void Update()
    {
        // 根據 CarChangeValue 選擇對應的車輛
      
            //SelectCar(CarChangeValue.CarChangeValue1);
        
    }

    private void Start()
    {
        //變更車輛
        SelectCar(AppData.cardSelectNUM_1);
    }

    // 根據 CarChangeValue 顯示對應的車輛，隱藏其他車輛
    void SelectCar(int carIndex)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (i == carIndex ) // 這裡 carIndex - 1 因為陣列是從 0 開始
            {
                cars[i].SetActive(true);  // 顯示對應的車輛
            }
            else
            {
                cars[i].SetActive(false); // 隱藏其他車輛
            }
        }
    }
}
