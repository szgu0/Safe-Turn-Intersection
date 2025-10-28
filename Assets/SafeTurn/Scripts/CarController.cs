using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarManager carManager;
    [Header("車輛設定")]
    public CarConfig config;
    public Transform frontWheel; // 前輪參考點
    public Transform rearWheel;  // 後輪參考點

    private Vector3 O_Position;

    [Header("路徑繪製")]
    public LineRenderer frontWheelLine;
    public LineRenderer rearWheelLine;
    public LineRenderer triangleLine;

    private List<Vector3> frontPoints = new List<Vector3>();
    private List<Vector3> rearPoints = new List<Vector3>();
    private List<Vector3> trianglePoints = new List<Vector3>();

    private float lineHeight = 0.31f; // 車線高

    public float steeringAngle = 30f; // 固定向右轉角度（度數）

    private bool StartToTargetZ = false;
    private bool reachedTargetZ = false;
    public bool coroutineFinished = false;

    private Coroutine moveCoroutine;
    private bool hasCrashed = false;

    [Header("車輪")]
    public Transform[] wheelObjects;
    public float xSpeed = 30f;  // 繞 X 軸的旋轉速度

    [Header("固定Y軸的設定")]
    public int[] fixedYIndices;
    private float xRotate = 0;
    private float yRotate = 0;

    private bool CarStop = false;

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

            transform.position = O_Position + new Vector3(yf, 0, xf);

            Vector3 dir = new Vector3(yf, 0, xf) - new Vector3(yr, 0, xr);
            float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.up);

            t += Time.deltaTime;
            yield return null;
        }
        coroutineFinished = true;
    }

    void Awake()
    {
        InitLine(frontWheelLine);
        InitLine(rearWheelLine);
        InitLine(triangleLine);

        // 啟動時預先繪製路徑
        // PreDrawPath();
    }

    void Update()
    {
        if (CarStop) return;

        if (StartToTargetZ && !reachedTargetZ)
        {
            reachedTargetZ = true;
            O_Position = transform.position;
            moveCoroutine = StartCoroutine(MoveCar());

        }
        else if (coroutineFinished)
        {
            transform.position += Vector3.right * config.speed * Time.deltaTime;
        }

        xRotate -= xSpeed * Time.deltaTime;
        //車輪
        if (reachedTargetZ && !coroutineFinished)
        {
            yRotate = steeringAngle;
            for (int i = 0; i < wheelObjects.Length; i++)
            {
                if (wheelObjects[i] == null) continue;
                // 判斷是否在固定Y的清單中
                if (System.Array.Exists(fixedYIndices, index => index == i))
                {
                    wheelObjects[i].transform.localRotation = Quaternion.Euler(xRotate, yRotate, 90);
                }
                else
                {
                    wheelObjects[i].transform.localRotation = Quaternion.Euler(xRotate, 0, 90);
                }
            }
        }
        else if (StartToTargetZ)
        {
            yRotate = Mathf.Lerp(yRotate, 0, 10 * Time.deltaTime);
            for (int i = 0; i < wheelObjects.Length; i++)
            {
                if (wheelObjects[i] == null) continue;
                // 判斷是否在固定Y的清單中
                if (System.Array.Exists(fixedYIndices, index => index == i))
                {
                    wheelObjects[i].transform.localRotation = Quaternion.Euler(xRotate, yRotate, 90);
                }
                else
                {
                    wheelObjects[i].transform.localRotation = Quaternion.Euler(xRotate, 0, 90);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        // 確認撞到玩家，且未重複觸發
        if (!hasCrashed && other.CompareTag("Player") && StartToTargetZ)
        {
            hasCrashed = true;

            // 停止車輛協程
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }

            carManager.CarCrashs();
        }
    }
    
    public void CarStopHard()
    {
        CarStop = true;
    }

    public void SetSteeringAngle(float Angle)
    {
        if (config.carName == "car") Angle -= 5f;
        if (config.carName == "bus") Angle += 3f;
        steeringAngle = Angle;
        // 角度改變時要重畫路徑
        PreDrawPath();
    }

    public void StartMoving()
    {
        StartToTargetZ = true;
    }

    // 預先模擬並繪製整條路徑
    public void PreDrawPath()
    {
        frontPoints.Clear();
        rearPoints.Clear();
        trianglePoints.Clear();

        // 模擬過程 (不移動實際物件，只計算路徑)
        float simTime = 0f;
        float maxTime = 5f; // 預估模擬時間，避免無限循環
        float step = 0.05f;

        Vector3 PreDraw_O_Position = transform.position;

        trianglePoints.Add(PreDraw_O_Position+ new Vector3(0, lineHeight, 0));
        trianglePoints.Add(PreDraw_O_Position+ new Vector3(0, lineHeight, -config.wheelBase));
        trianglePoints.Add(PreDraw_O_Position+ new Vector3(config.wheelBase/Mathf.Tan(steeringAngle * Mathf.Deg2Rad), 0.35f, -config.wheelBase));
        triangleLine.positionCount = trianglePoints.Count;
        triangleLine.SetPositions(trianglePoints.ToArray());
        

        while (simTime < maxTime)
        {
            float theta = steeringAngle * Mathf.Deg2Rad;

            float xf, yf, xr, yr;

            float angularVelocity = config.speed * Mathf.Sin(theta) / config.wheelBase;

            xf = config.wheelBase * (Mathf.Sin(theta + angularVelocity * simTime) / Mathf.Sin(theta) - 1);
            yf = config.wheelBase * (1 / Mathf.Tan(theta) - Mathf.Cos(theta + angularVelocity * simTime) / Mathf.Sin(theta));

            xr = config.wheelBase * (Mathf.Sin(angularVelocity * simTime) / Mathf.Tan(theta) - 1);
            yr = config.wheelBase / Mathf.Tan(theta) * (1 - Mathf.Cos(angularVelocity * simTime));

            // 計算前後輪位置
            Vector3 frontPos = PreDraw_O_Position + new Vector3(yf, lineHeight, xf);
            Vector3 rearPos = PreDraw_O_Position + new Vector3(yr, lineHeight, xr);

            frontPoints.Add(frontPos);
            rearPoints.Add(rearPos);

            // 停止條件：假設旋轉到 90 度
            if (Mathf.Rad2Deg * (config.speed * Mathf.Sin(theta) / config.wheelBase * simTime) >= 180f)
                break;

            simTime += step;
        }

        // 更新 LineRenderer
        frontWheelLine.positionCount = frontPoints.Count;
        frontWheelLine.SetPositions(frontPoints.ToArray());

        rearWheelLine.positionCount = rearPoints.Count;
        rearWheelLine.SetPositions(rearPoints.ToArray());
    }

    // 顯示或隱藏路徑
    public void TogglePath(bool show)
    {
        if (frontWheelLine != null) frontWheelLine.enabled = show;
        if (rearWheelLine != null) rearWheelLine.enabled = show;
        if(show)PreDrawPath();
    }
    public void ToggleTri(bool show)
    {
        if (triangleLine != null) triangleLine.enabled = show;
    }

    void InitLine(LineRenderer lr)
    {
        if (lr != null)
        {
            lr.positionCount = 0;
            lr.widthMultiplier = 0.1f;

            Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) shader = Shader.Find("Sprites/Default");
            //lr.material = new Material(shader);
        }
    }
}
