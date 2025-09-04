using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RoadChange : MonoBehaviour
{
    private Rigidbody rb;
   
    public GameObject UiPanel;
    public PathCreator[] pathCreators; // 多个 PathCreator 路径
    private PathCreator currentPath;    // 当前选择的 PathCreator
    public int ChangeCardValue;         // 用于改变路径的卡片值

    private bool _isTrigger;

    public float CarSpeed = 15f;
    public  float CarSpeedKMHr = 0.0f;

    private float _distanceTravelled = 0.0f;
    private bool _isRun = false;

    public float rightCarSpeedKMHr = 0.0f;

    bool isStandby = true; //準備階段
    bool isEnd = false; //結束階段
    public bool isPause = false; //暫停階段

    public bool isRun
    {
        get { return _isRun; }
        set { _isRun = value; }
    }

    void Start()
    {
        isStandby = true;
        isEnd = false;
        isPause = false;

        UiPanel.SetActive(true);
        // 根据 ChangeCardValue 选择相应的路径
        SelectPathByCardValue(selectCard.ChangeCardValue);

        _isTrigger = false;
    }

    public void SpeedSliderChange(float speed)
    {
        CarSpeed = speed / 3.6f;
        CarSpeedKMHr = speed;
    }

    void Update()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

      /*  if (rightCarSpeedKMHr < CarSpeedKMHr && _distanceTravelled > 110)
        {

            StepsGuide stepsGuide = FindObjectOfType<StepsGuide>();
            int skipStepIndex = 4; // 要跳過的步驟索引
             stepsGuide.JumpToStep(skipStepIndex) ;

        }*/
    

        Debug.Log(CarSpeedKMHr);

      if (_isTrigger == true)
        {
            stepsGuide.NextStep();
            
        }

        if (currentPath == null)
        {
            return;
        }

        if (!_isRun && isStandby)
        {
            //準備階段
            _distanceTravelled = 0.0f;
            transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled);
            transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled);
        }

        if (!_isRun && isEnd)
        {
            //結束階段
            _distanceTravelled = currentPath.path.length - 0.1f;
            transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled);
            transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled);
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
                transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled);
                transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled);
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

            if (_isRun && rightCarSpeedKMHr < CarSpeedKMHr && _distanceTravelled > 110)
            {
                isStandby = false;
                isEnd = false;
                isPause = false;
                _isRun = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, CarSpeed);
                stepsGuide.NextStep();
                StartCoroutine(SmoothStop()); // 平滑停止
                UiPanel.SetActive(true);
            }
        }

        /*if (!_isRun)
        {
            CorrectVehicleRotation();
       }*/
    }

    /* private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.tag == "end")
         {
             CarSpeed = 0;
             _isTrigger = true;
             _isRun = false; // 停止車輛運動
             Debug.Log("到達終點，停止車輛");
         }

         if (other.gameObject.tag == "del_rig")
         {
             Rigidbody rb = gameObject.GetComponent<Rigidbody>();
             if (rb != null)
             {
                 rb.velocity = Vector3.zero; // 清空速度
             }

             // 僅調整角度，不更改位置
             Vector3 finalRotation = transform.eulerAngles;
             finalRotation.z = 0;
             transform.eulerAngles = finalRotation;

             Debug.Log("車輛旋轉已修正");
         }
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IsKinematic")
        {
            rb.isKinematic = false;
        }
        if (other.gameObject.tag == "IsKinematic_END")
        {
            rb.isKinematic = true;
        }

    }
    private IEnumerator SmoothStop()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        while (rb.velocity.magnitude > 0.1f)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 2);
            yield return null;
        }
        rb.velocity = Vector3.zero;

        // 確保車輛停止後的位置與 `_distanceTravelled` 對應
        _distanceTravelled = currentPath.path.length;
    }
    public void SelectPathByCardValue(int value)
    {
        if (pathCreators.Length == 0) return;

        // 根据 ChangeCardValue 选择 PathCreator
        int index = (value ) / (25 / pathCreators.Length);
        index = Mathf.Clamp(index, 0, pathCreators.Length - 1);

        currentPath = pathCreators[index];

        // 激活当前选择的 PathCreator 并禁用其他的
        for (int i = 0; i < pathCreators.Length; i++)
        {
            pathCreators[i].gameObject.SetActive(i == index);
        }
    }

   public void CorrectVehicleRotation()
    {
        Vector3 correctedEulerAngles = transform.eulerAngles;
        correctedEulerAngles.y = 182.88F; // 重置y轴旋转，使车辆摆正
        correctedEulerAngles.z = 0; // 锁定z轴旋转，防止自转
        transform.eulerAngles = correctedEulerAngles;
    }

    public StepsGuide stepsGuide;

   public void EndDriver()
    {
        UiPanel.SetActive(true);

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
       Vector3 finalRotation = transform.eulerAngles;
        finalRotation.z = 0;
       transform.eulerAngles = finalRotation;
        stepsGuide.NextStep();
        CorrectVehicleRotation();
    }



    float[] carWeightData = { 1000, 1500, 2000, 2500, 3000 };
    float[] road_0_R = { 10, 30, 50, 80, 100 };
    float[] road_0_U = { 0, 0.85f, 0.8f, 0.6f, 0.9f, 0.78f };

    public float calculateSpeed_E2()
    {
        float f = 10000;
        float carWeight = carWeightData[AppData.cardSelectNUM_1];
        float r = road_0_R[AppData.cardSelectNUM_2];

        return Mathf.Sqrt(f * r / carWeight) * 3.6f;
    }

    public float calculateSpeed_E4()
    {
        float f = 10000;
        float carWeight = carWeightData[AppData.cardSelectNUM_1];
        float u = road_0_U[AppData.cardSelectNUM_2];
        float r = road_0_R[AppData.cardSelectNUM_3];

        return Mathf.Sqrt(9.8f * u * r) * 3.6f;
    }
}
