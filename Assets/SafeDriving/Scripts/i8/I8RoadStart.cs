using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class I8RoadStart : MonoBehaviour
{
    public GameObject UiPanel;
    public PathCreator[] pathCreators;
    public PathCreator[] error_path;// 多个 PathCreator 路径
    private PathCreator currentPath;    // 当前选择的 PathCreator
    public int ChangeCardValue;         // 用于改变路径的卡片值

    private bool _isTrigger;

    public float CarSpeed = 15f;
    public float CarSpeedKMHr = 0.0f;

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
        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (pathCreators[i] != null) pathCreators[i].gameObject.SetActive(true);
            if (error_path[i] != null) error_path[i].gameObject.SetActive(false);
        }

        isStandby = true;
        isEnd = false;
        isPause = false;

        UiPanel.SetActive(true);
        // 根据 ChangeCardValue 选择相应的路径
        //修改 chhuang
        SelectPathByCardValue(AppData.cardSelectNUM_1);
        //SelectPathByCardValue(i8Card.value);

        _isTrigger = false;
    }

    public void SpeedSliderChange(float speed)
    {
        CarSpeed = speed / 3.6f;
        CarSpeedKMHr = speed;
    }

    void Update()
    {
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

    /*private void OnTriggerEnter(Collider other)
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

    // 新增平滑減速的協程
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
    }

    public void SelectPathByCardValue(int value)
    {
        if (pathCreators.Length == 0) return;

        // 根据 ChangeCardValue 选择 PathCreator
        int index = (value) / (5 / pathCreators.Length);
        index = Mathf.Clamp(index, 0, pathCreators.Length - 1);

        currentPath = pathCreators[index];

        if (rightCarSpeedKMHr < CarSpeedKMHr)
        {
            for (int i = 0; i < pathCreators.Length; i++)
            {
                if (pathCreators[i] != null && pathCreators[i].gameObject.activeSelf)
                {
                    pathCreators[i].gameObject.SetActive(false);

                    if (error_path[i] != null)
                    {
                        error_path[i].gameObject.SetActive(true);
                    }
                }
            }
        }

        // 在禁用其他 pathCreators 並啟用 error_path 之後，處理選擇的路徑
        if (currentPath != null && currentPath.gameObject.activeSelf)
        {
            currentPath.gameObject.SetActive(false);
        }
        currentPath = pathCreators[index];
        currentPath.gameObject.SetActive(true);


        // 激活当前选择的 PathCreator 并禁用其他的
        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (error_path[i] == null || !error_path[i].gameObject.activeSelf)
            {
                pathCreators[i].gameObject.SetActive(i == index);
            }
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