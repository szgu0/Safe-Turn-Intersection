using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steering_wheel_ROT : MonoBehaviour
{
    public Transform Traget;
    public float rotationSpeed = 50f; // 旋轉速度
    public float maxRotation = 30f;  // 最大旋轉角度
    private float currentRotation = 0f; // 當前旋轉角度
    private int direction = 0; // -1 表示向左，1 表示向右，0 表示不旋轉

    void Update()
    {
        
        if (Traget.transform.rotation.z<7f) // 左方向鍵
        {
            direction = 1; // 向左旋轉
        }
        else if (Traget.transform.rotation.z >7f) // 右方向鍵
        {
            direction = -1; // 向右旋轉
        }
        else
        {
            direction = 0; // 停止旋轉
        }

        // 控制旋轉角度
        if (direction != 0)
        {
            float rotationStep = rotationSpeed * Time.deltaTime * direction;
            float newRotation = currentRotation + rotationStep;

            // 限制最大旋轉角度
            if (Mathf.Abs(newRotation) <= maxRotation)
            {
                transform.Rotate(0, 0, rotationStep);
                currentRotation = newRotation;
            }
        }
    }
}
