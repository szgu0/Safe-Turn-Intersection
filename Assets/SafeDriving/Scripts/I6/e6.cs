    using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class e6 : MonoBehaviour
{
    public GameObject PATH;
    public float time;

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
    private Rigidbody rb;
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
        SelectPathByCardValue(E6Card.E6CardValue);

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
        //   Debug.Log(CarSpeedKMHr);

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

        /* if (!_isRun)
         {
             CorrectVehicleRotation();
         }*/
    }

    /*   private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.tag == "end")
           {
               CarSpeed = 0;
               _isTrigger = true;
           }

           if (other.gameObject.tag == "del_rig")
           {
               // 使用阻力減速而非立即停止
               StartCoroutine(SmoothStop());
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
    // 新增平滑減速的協程
    /* private IEnumerator SmoothFlyAndStop(float flightDistance = 15.0f)
     {
         Rigidbody rb = gameObject.GetComponent<Rigidbody>();
         rb.isKinematic = false; // 取消路径控制，启用物理引擎
         PATH.SetActive(false);  // 停止路径绑定
         Vector3 startPosition = transform.position; // 记录初始位置
         bool hasStopped = false;

         // 飞行阶段
         while (!hasStopped)
         {
             // 计算车辆飞行的总距离
             float travelledDistance = Vector3.Distance(startPosition, transform.position);

             // 如果达到预设飞行距离，则停止
             if (travelledDistance >= flightDistance)
             {
                 rb.velocity = Vector3.zero; // 停止速度
                 rb.angularVelocity = Vector3.zero; // 停止旋转
                 hasStopped = true;
             }

             yield return null; // 等待下一帧更新
         }
         stepsGuide.NextStep();
         _isRun = false;
         // 激活 UI 面板或其他逻辑
         UiPanel.SetActive(true);
     }*/

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

        UiPanel.SetActive(true);
        stepsGuide.NextStep();
    }

    public void SelectPathByCardValue(int value)
    {
        if (pathCreators.Length == 0) return;

        // 根据 ChangeCardValue 选择 PathCreator
        int index = (value ) / (5 / pathCreators.Length);
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
        correctedEulerAngles.y = 188.827F; // 重置y轴旋转，使车辆摆正
        correctedEulerAngles.z = 0; // 锁定z轴旋转，防止自转
        transform.eulerAngles = correctedEulerAngles;
    }

    public StepsGuide stepsGuide;

   public void EndDriver()
    {
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

    /*public float calculateSpeed_E2()
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
    }*/
}
