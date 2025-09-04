using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PreviousRotationDistributor : MonoBehaviour
{
    public Transform carObject;    // 車輛物件 (a)
    public Transform[] steeringWheels; // 方向盤物件陣列 (b1, b2, b3, b4, b5)
    private float lastCarYRotation; // 記錄車輛上一秒的 Y 軸旋轉角度
    public float maxSteeringAngle = 100f; // 方向盤 Z 軸最大旋轉角度
    public float minSteeringAngle = -100f; // 方向盤 Z 軸最小旋轉角度
    public float returnSpeed = 2f; // 方向盤自動回正速度
    public float sensitivityThreshold = 0.1f; // 車輛旋轉的變化閾值，用於判斷直線行駛

    void Start()
    {
        // 初始化車輛的 Y 軸旋轉角度
        lastCarYRotation = carObject.eulerAngles.y;
    }

    void Update()
    {
        // 獲取當前車輛的 Y 軸旋轉角度
        float currentCarYRotation = carObject.eulerAngles.y;

        // 計算車輛 Y 軸旋轉角度的變化量
        float deltaYRotation = Mathf.DeltaAngle(lastCarYRotation, currentCarYRotation);

        // 獲取每個方向盤的當前 Z 軸旋轉角度並更新
        foreach (var steeringWheel in steeringWheels)
        {
            // 獲取方向盤的當前 Z 軸角度
            float currentSteeringZ = steeringWheel.localEulerAngles.z;

            // 將方向盤的 Z 軸角度轉換為 [-180, 180] 範圍
            if (currentSteeringZ > 180f) currentSteeringZ -= 360f;

            float newSteeringZ;

            // 當車輛有旋轉時，更新方向盤角度
            if (Mathf.Abs(deltaYRotation) > sensitivityThreshold)
            {
                // 計算新的方向盤 Z 軸旋轉角度，並限制範圍
                newSteeringZ = Mathf.Clamp(currentSteeringZ - deltaYRotation, minSteeringAngle, maxSteeringAngle);
            }
            else
            {
                // 當車輛沿直線行駛，方向盤逐漸回正
                newSteeringZ = Mathf.Lerp(currentSteeringZ, 0f, Time.deltaTime * returnSpeed);
            }

            // 更新方向盤的旋轉角度 (以自身軸心自轉)
            steeringWheel.localEulerAngles = new Vector3(
                steeringWheel.localEulerAngles.x,
                steeringWheel.localEulerAngles.y,
                newSteeringZ
            );
        }

        // 更新車輛上一秒的 Y 軸旋轉角度
        lastCarYRotation = currentCarYRotation;
    }

}


