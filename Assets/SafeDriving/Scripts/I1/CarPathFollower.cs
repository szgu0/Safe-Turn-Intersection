using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarPathFollower : MonoBehaviour
{
    //public PrometeoCarController myCar;

    public GameObject UiPanel;

    public PathCreator currentPath;


    public float CarSpeed = 15;
    public float CarSpeedKMHr = 0.0f; //使用者設定的速度
    public float CarTime = 0.0f; //使用者設定的時間

    public PathCreator[] pathCreatorData;
    public PathCreator[] pathCreatorData_error;

    //  private Rigidbody rb;

    EndOfPathInstruction endOfPathInstruction;
    float _distanceTravelled = 0.0f;

    public float rightCarSpeedKMHr = 0.0f; //正確的限速
    public float rightCarTime = 0.0f; //正確的時間

    bool isStandby = true; //準備階段
    bool isEnd = false; //結束階段
    public bool isPause = false; //暫停階段

    [SerializeField]
    bool _isRun = false;
    public bool isRun
    {
        get { return _isRun; }
        set
        {
            _isRun = value;
            //切換路徑
            ChangePath(AppData.selectCard);
        }
    }

    public void SpeedSliderChange(float speed)
    {
        CarSpeed = speed / 3.6f;
        CarSpeedKMHr = speed;
    }

    public void ChangePath(CardData cardData)
    {

        //切換路徑
        if (cardData == null)
        {
            currentPath = pathCreatorData[0];
            return;
        }

        string nowscenename = SceneManager.GetActiveScene().name;

        if (AppData.selectCard.cardTypeData == CardTypeData.道路_1)
        {
            //E1：只有正確路線, E3：會有錯誤路線
            currentPath = pathCreatorData[(int)AppData.selectCard.road_1 - 1];
            if (nowscenename == "E3_Driver_Car")
            {
                if (rightCarSpeedKMHr < CarSpeedKMHr)
                {
                    currentPath = pathCreatorData_error[(int)AppData.selectCard.road_1 - 1];
                }
            }
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.迴轉半徑)
        {
            //E2, E4, E7
            currentPath = pathCreatorData[(int)AppData.selectCard.road_0_R - 1];
            if (rightCarSpeedKMHr < CarSpeedKMHr)
            {
                currentPath = pathCreatorData_error[(int)AppData.selectCard.road_0_R - 1];
            }
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.道路_3)
        {
            //E6
            currentPath = pathCreatorData[(int)AppData.selectCard.road_3 - 1];
            if (rightCarSpeedKMHr < CarSpeedKMHr)
            {
                currentPath = pathCreatorData_error[(int)AppData.selectCard.road_3 - 1];
            }
        }
        if (AppData.selectCard.cardTypeData == CardTypeData.道路_2)
        {
            //E8
            currentPath = pathCreatorData[(int)AppData.selectCard.road_2 - 1];
            if (rightCarSpeedKMHr < CarSpeedKMHr)
            {
                currentPath = pathCreatorData_error[(int)AppData.selectCard.road_2 - 1];
            }
        }

        if (nowscenename == "E5_Driver_Car")
        {
            //組合的方式
            currentPath = pathCreatorData[(int)AppData.selectCard.road_0_A - 1];
            if (rightCarSpeedKMHr < CarSpeedKMHr)
            {
                currentPath = pathCreatorData_error[(int)AppData.selectCard.road_0_A - 1];
            }
        }
    }

    void Start()
    {
        isStandby = true;
        isEnd = false;
        isPause = false;

        UiPanel.SetActive(true);
        string nowscenename = SceneManager.GetActiveScene().name;
        if (nowscenename == "E2_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E2();
        }
        else if (nowscenename == "E3_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E3();
        }
        else if (nowscenename == "E4_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E4();
        }
        else if (nowscenename == "E5_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E5();
        }
        else if (nowscenename == "E6_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E6();
        }
        else if (nowscenename == "E7_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E7();
        }
        else if (nowscenename == "E8_Driver_Car")
        {
            rightCarSpeedKMHr = calculateSpeed_E8();
        }

        ChangePath(AppData.selectCard);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentPath == null)
        {
            return;
        }

        if (!_isRun && isStandby)
        {
            //準備階段
            _distanceTravelled = 0.0f;
            transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
        }

        if (!_isRun && isEnd)
        {
            //結束階段
            _distanceTravelled = currentPath.path.length - 0.1f;
            transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
        }
        if (_isRun && !isPause)
        {
            UiPanel.SetActive(false);

            // 確保車輛不會在不必要的情況下被重置
            if (_distanceTravelled < 0)
            {
                _distanceTravelled = 0; // 距離不應該小於零
            }

            _distanceTravelled += CarSpeed * Time.deltaTime;
            if (_distanceTravelled < currentPath.path.length)
            {
                transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            }
            else
            {
                isStandby = false;
                isEnd = true;
                isPause = false;
                _isRun = false;
                EndDriver();//模擬結束
                //gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    public StepsGuide stepsGuide;

    public void EndDriver()
    {
        //gameObject.GetComponent<CarRunEnd>().ShowResult(); //顯示結果, 由事件觸發
        UiPanel.SetActive(true);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        stepsGuide.NextStep();
    }

    float[] Card_A = { 1000, 1500, 2000, 2500, 3000 }; //車重
    float[] Card_H = { 0.5f, 06f, 0.7f, 0.8f, 1.0f }; //車中心高
    float[,] Card_B = { { 40, 0, 0 }, { 20, 20, 0 }, { 10, 30, 0 }, { 10, 20, 10 }, { 30, 10, 0 } }; //半徑
    float[,] Card_C = { { 100, 50, 0, 0, 40 }, { 30, 50, 20, 100, 0 }, { 30, 100, 0, 0, 0 } }; //半徑, 凸半徑
    float[] Card_E1 = { 10, 30, 50, 80, 100 }; //迴轉半徑
    float[] Card_E2 = { 0, 0.85f, 0.8f, 0.6f, 0.9f, 0.78f }; //摩係係數
    float[] Card_E3 = { 20, 30, 40, 50, 60 }; //超高角度
    float[] Card_D = { 20, 30, 40, 50, 60 }; //凸半徑
    public float calculateSpeed_E2()
    {
        float result = 0.0f;
        float f = 10000;
        float carWeight = 0.0f;
        float r = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        r = Card_E1[AppData.cardSelectNUM_2];
        result = Mathf.Sqrt(f * r / carWeight) * 3.6f;

        return result;
    }

    public float E3_CarTime = 0.0f;
    public float calculateSpeed_E3()
    {
        //計算可以過的最大速度
        float result = 0.0f;
        float f = 10000;
        float carWeight = 0.0f;
        float r = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        r = 100.0f;
        for (int i = 0; i < 3; i++) //Card_B
        {
            if (r > Card_B[AppData.cardSelectNUM_2, i] && Card_B[AppData.cardSelectNUM_2, i] != 0)
            {
                r = Card_B[AppData.cardSelectNUM_2, i];
            }
        }
        result = Mathf.Sqrt(f * r / carWeight);//算出速度

        for (int i = 0; i < 3; i++) //Card_B
        {
            //算出距離
            r = Card_B[AppData.cardSelectNUM_2, i];
            if (r != 0.0f)
            {
                E3_CarTime += r * Mathf.PI / Mathf.Sqrt(f * r / carWeight);
            }
        }

        //回傳花費的時間
        return result;
    }

    public float calculateSpeed_E3_1()
    {
        //？
        //計算每一個彎的速度
        float result = 0.0f;
        float f = 10000;
        float carWeight = 0.0f;
        float r = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        for (int i = 0; i < 3; i++) //Card_B
        {
            r = Card_B[AppData.cardSelectNUM_2, i];
            if (r != 0.0f)
            {
                result += r * Mathf.PI / Mathf.Sqrt(f * r / carWeight);
            }
        }
        return result;
    }

    public float calculateSpeed_E4()
    {
        float result = 0.0f;
        float f = 10000;
        float carWeight = 0.0f;
        float r = 0.0f;
        float u = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        u = Card_E2[AppData.cardSelectNUM_2];
        r = Card_E1[AppData.cardSelectNUM_3];
        result = Mathf.Sqrt(9.8f * u * r) * 3.6f;

        return result;
    }

    public float calculateSpeed_E5()
    {
        float result = 0.0f;
        //float f = 10000;
        float carWeight = 0.0f;
        float r = 100.0f;
        float u = 0.0f;
        float angle = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        angle = Card_E3[AppData.cardSelectNUM_2];
        result = Mathf.Sqrt(9.8f * r * Mathf.Tan(angle / 180.0f * Mathf.PI)) * 3.6f;

        return result;
    }

    public float calculateSpeed_E6()
    {
        float result = 0.0f;
        //float f = 10000;
        float carWeight = 0.0f;
        float r = 100.0f;
        float u = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        r = Card_D[AppData.cardSelectNUM_2];
        result = Mathf.Sqrt(9.8f * r) * 3.6f;

        return result;
    }

    public float calculateSpeed_E7()
    {
        float result = 0.0f;
        //float f = 10000;
        float carWeight = 0.0f;
        float r = 100.0f;
        float u = 0.0f;
        float h = 0.0f;

        carWeight = Card_A[AppData.cardSelectNUM_1];
        h = Card_H[AppData.cardSelectNUM_1];
        r = Card_D[AppData.cardSelectNUM_2];
        result = Mathf.Sqrt(9.8f * r * h) * 3.6f;

        return result;
    }

    public float calculateSpeed_E8()
    {
        //OK
        //float[,] Card_C = { { 100, 50, 0, 0, 40 }, { 30, 50, 20, 100, 0 }, { 30, 100, 0, 0, 0 } }; //半徑, 凸半徑
        float result = 160.0f;
        if (AppData.cardSelectNUM_1 == 0)
        {
            result = Mathf.Sqrt(9.8f * 40) * 3.6f;
        }
        if (AppData.cardSelectNUM_1 == 1)
        {
            result = Mathf.Sqrt(9.8f * 0.8f * 20) * 3.6f;
        }
        if (AppData.cardSelectNUM_1 == 2)
        {
            result = Mathf.Sqrt(9.8f * 0.8f * 30) * 3.6f;
        }
        return result;
    }
}
