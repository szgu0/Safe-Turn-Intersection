using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [Header("車輛設定")]
    public CarConfig config;
    public Transform frontWheel; // 前輪參考點
    public Transform rearWheel;  // 後輪參考點

    private Vector3 O_Position;

    [Header("路徑繪製")]
    public LineRenderer frontWheelLine;
    public LineRenderer rearWheelLine;

    private List<Vector3> frontPoints = new List<Vector3>();
    private List<Vector3> rearPoints = new List<Vector3>();

    public float steeringAngle = 30f; // 固定向右轉角度（度數）

    public float targetZ = 9f;     // 判斷要觸發協程的 Z 值

    private bool StartToTargetZ = false;  // 是否開始移動
    private bool reachedTargetZ = false;  // 是否到達目標Z
    private bool coroutineFinished = false; // 協程是否結束



    private IEnumerator MoveCar()
    {
        float xf, yf, xr, yr;
        float t = 0f;

        while (transform.eulerAngles.y < 90f)
        {
            float theta = steeringAngle * Mathf.Deg2Rad;

            if (Mathf.Abs(theta) < 0.001f) // 直行
            {
                xf = config.speed * t;
                yf = 0;
                xr = config.speed * t;
                yr = 0;
            }
            else // 轉彎
            {
                float angularVelocity = config.speed * Mathf.Sin(theta) / config.wheelBase;

                xf = config.wheelBase * (Mathf.Sin(theta + angularVelocity * t) / Mathf.Sin(theta) - 1);
                yf = config.wheelBase * (1 / Mathf.Tan(theta) - Mathf.Cos(theta + angularVelocity * t) / Mathf.Sin(theta));

                xr = config.wheelBase * (Mathf.Sin(angularVelocity * t) / Mathf.Tan(theta) - 1);
                yr = config.wheelBase / Mathf.Tan(theta) * (1 - Mathf.Cos(angularVelocity * t));
            }

            // 更新位置
            transform.position = O_Position + new Vector3(yf, 0, xf);

            // 更新角度 (XZ 平面)
            Vector3 dir = new Vector3(yf, 0, xf) - new Vector3(yr, 0, xr);
            float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.up);

            t += Time.deltaTime;
            yield return null;
        }
        coroutineFinished = true;
    }

    void Start()
    {
        // 初始化 LineRenderer
        InitLine(frontWheelLine, Color.red);   // 前輪軌跡：紅色
        InitLine(rearWheelLine, Color.blue);  // 後輪軌跡：藍色

    }

    void Update()
    {
        if (StartToTargetZ && !reachedTargetZ)
        {
            // 持續往Z正方向移動
            transform.position += Vector3.forward * config.speed * Time.deltaTime;

            // 當Z大於指定值
            if (transform.position.z > targetZ)
            {
                reachedTargetZ = true;
                O_Position = transform.position;
                StartCoroutine(MoveCar());
            }
        }
        else if (coroutineFinished)
        {
            // 協程結束後，改成往X正方向移動
            transform.position += Vector3.right * config.speed * Time.deltaTime;
        }
        DrawPath();
    }


    public void SetSteeringAngle(float Angle)
    {
        steeringAngle = Angle;
        targetZ = Angle / 5f;
    }

    public void StartMoving()
    {
        StartToTargetZ = true;
    }


    void DrawPath()
    {
        // 記錄前後輪位置
        Vector3 frontPos = frontWheel.position;
        Vector3 rearPos = rearWheel.position;

        frontPoints.Add(frontPos);
        rearPoints.Add(rearPos);

        // 更新前輪路徑
        frontWheelLine.positionCount = frontPoints.Count;
        frontWheelLine.SetPositions(frontPoints.ToArray());

        // 更新後輪路徑
        rearWheelLine.positionCount = rearPoints.Count;
        rearWheelLine.SetPositions(rearPoints.ToArray());
    }

    void InitLine(LineRenderer lr, Color color)
    {
        if (lr != null)
    {
        lr.positionCount = 0;
        lr.widthMultiplier = 0.05f;

        // 改成 Universal Render Pipeline 的 Unlit Shader
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null) shader = Shader.Find("Sprites/Default"); // 備用

        lr.material = new Material(shader);
        lr.startColor = lr.endColor = color;
    }
    }
}
