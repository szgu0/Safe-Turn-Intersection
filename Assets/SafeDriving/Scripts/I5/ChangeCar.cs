using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCar : MonoBehaviour
{
    public GameObject[] cars; // cars[0] ���� Car 1, cars[1] ���� Car 2... cars[4] ���� Car 5

    // �o�̪� CarChangeValue �O�~���t�ζǤJ���� (1~5)
    

    void Update()
    {
        // �ھ� CarChangeValue ��ܹ���������
      
            //SelectCar(CarChangeValue.CarChangeValue1);
        
    }

    private void Start()
    {
        //�ܧ󨮽�
        SelectCar(AppData.cardSelectNUM_1);
    }

    // �ھ� CarChangeValue ��ܹ����������A���è�L����
    void SelectCar(int carIndex)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (i == carIndex ) // �o�� carIndex - 1 �]���}�C�O�q 0 �}�l
            {
                cars[i].SetActive(true);  // ��ܹ���������
            }
            else
            {
                cars[i].SetActive(false); // ���è�L����
            }
        }
    }
}
