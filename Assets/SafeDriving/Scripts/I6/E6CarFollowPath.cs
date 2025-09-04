using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6CarFollowPath : MonoBehaviour
{
    public PathCreator[] pathCreators; // 多个 PathCreator 路径
    private PathCreator currentPath;    // 当前选择的 PathCreator
    public int ChangeCardValue;         // 用于改变路径的卡片值

    private bool _isTrigger;

    public float CarSpeed = 15f;
    public  float CarSpeedKMHr = 0.0f;

    private float _distanceTravelled = 0.0f;
    private bool _isRun = false;

    public  float rightCarSpeedKMHr = 0.0f;

    public bool isRun
    {
        get { return _isRun; }
        set { _isRun = value; }
    }

    void Start()
    {
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
        Debug.Log(CarSpeedKMHr);

        if (_isTrigger == true)
        {
            stepsGuide.NextStep();

        }

        if (currentPath != null && _isRun)
        {
            _distanceTravelled += CarSpeed * Time.deltaTime;

            // 如果距离超过路径长度，触发 EndDriver 并停止移动
            if (_distanceTravelled >= currentPath.path.length)
            {

                _distanceTravelled = currentPath.path.length;
                _isRun = false;
                //  EndDriver();
            }

            // 移动和旋转
            transform.position = currentPath.path.GetPointAtDistance(_distanceTravelled);
            transform.rotation = currentPath.path.GetRotationAtDistance(_distanceTravelled);



            if (rightCarSpeedKMHr < CarSpeedKMHr && _distanceTravelled > 110)
            {
                _isRun = false;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, CarSpeed);
            }
        }

        if (!_isRun)
        {
            CorrectVehicleRotation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "end")
        {
            CarSpeed = 0;
            _isTrigger = true;
        }

        if (other.gameObject.tag == "del_rig")
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 finalRotation = transform.eulerAngles;
            finalRotation.z = 0;
            transform.eulerAngles = finalRotation;
        }
    }

    public void SelectPathByCardValue(int value)
    {
        if (pathCreators.Length == 0) return;

        // 根据 ChangeCardValue 选择 PathCreator
        int index = (value - 1) / (25 / pathCreators.Length);
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
        correctedEulerAngles.y = 0; // 重置y轴旋转，使车辆摆正
        correctedEulerAngles.z = 0; // 锁定z轴旋转，防止自转
        transform.eulerAngles = correctedEulerAngles;
    }

    public StepsGuide stepsGuide;

    /* public void EndDriver()
     {
        // gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
     //    Vector3 finalRotation = transform.eulerAngles;
       //  finalRotation.z = 0;
       //  transform.eulerAngles = finalRotation;
         stepsGuide.NextStep();
     }*/

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
