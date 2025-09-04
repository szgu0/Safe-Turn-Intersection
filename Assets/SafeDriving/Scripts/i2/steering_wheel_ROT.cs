using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steering_wheel_ROT : MonoBehaviour
{
    public Transform Traget;
    public float rotationSpeed = 50f; // ����t��
    public float maxRotation = 30f;  // �̤j���ਤ��
    private float currentRotation = 0f; // ��e���ਤ��
    private int direction = 0; // -1 ��ܦV���A1 ��ܦV�k�A0 ��ܤ�����

    void Update()
    {
        
        if (Traget.transform.rotation.z<7f) // ����V��
        {
            direction = 1; // �V������
        }
        else if (Traget.transform.rotation.z >7f) // �k��V��
        {
            direction = -1; // �V�k����
        }
        else
        {
            direction = 0; // �������
        }

        // ������ਤ��
        if (direction != 0)
        {
            float rotationStep = rotationSpeed * Time.deltaTime * direction;
            float newRotation = currentRotation + rotationStep;

            // ����̤j���ਤ��
            if (Mathf.Abs(newRotation) <= maxRotation)
            {
                transform.Rotate(0, 0, rotationStep);
                currentRotation = newRotation;
            }
        }
    }
}
