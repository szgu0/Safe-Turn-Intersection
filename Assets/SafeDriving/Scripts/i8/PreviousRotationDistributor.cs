using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PreviousRotationDistributor : MonoBehaviour
{
    public Transform carObject;    // �������� (a)
    public Transform[] steeringWheels; // ��V�L����}�C (b1, b2, b3, b4, b5)
    private float lastCarYRotation; // �O�������W�@�� Y �b���ਤ��
    public float maxSteeringAngle = 100f; // ��V�L Z �b�̤j���ਤ��
    public float minSteeringAngle = -100f; // ��V�L Z �b�̤p���ਤ��
    public float returnSpeed = 2f; // ��V�L�۰ʦ^���t��
    public float sensitivityThreshold = 0.1f; // �������઺�ܤ��H�ȡA�Ω�P�_���u��p

    void Start()
    {
        // ��l�ƨ����� Y �b���ਤ��
        lastCarYRotation = carObject.eulerAngles.y;
    }

    void Update()
    {
        // �����e������ Y �b���ਤ��
        float currentCarYRotation = carObject.eulerAngles.y;

        // �p�⨮�� Y �b���ਤ�ת��ܤƶq
        float deltaYRotation = Mathf.DeltaAngle(lastCarYRotation, currentCarYRotation);

        // ����C�Ӥ�V�L����e Z �b���ਤ�רç�s
        foreach (var steeringWheel in steeringWheels)
        {
            // �����V�L����e Z �b����
            float currentSteeringZ = steeringWheel.localEulerAngles.z;

            // �N��V�L�� Z �b�����ഫ�� [-180, 180] �d��
            if (currentSteeringZ > 180f) currentSteeringZ -= 360f;

            float newSteeringZ;

            // ����������ɡA��s��V�L����
            if (Mathf.Abs(deltaYRotation) > sensitivityThreshold)
            {
                // �p��s����V�L Z �b���ਤ�סA�í���d��
                newSteeringZ = Mathf.Clamp(currentSteeringZ - deltaYRotation, minSteeringAngle, maxSteeringAngle);
            }
            else
            {
                // �����u���u��p�A��V�L�v���^��
                newSteeringZ = Mathf.Lerp(currentSteeringZ, 0f, Time.deltaTime * returnSpeed);
            }

            // ��s��V�L�����ਤ�� (�H�ۨ��b�ߦ���)
            steeringWheel.localEulerAngles = new Vector3(
                steeringWheel.localEulerAngles.x,
                steeringWheel.localEulerAngles.y,
                newSteeringZ
            );
        }

        // ��s�����W�@�� Y �b���ਤ��
        lastCarYRotation = currentCarYRotation;
    }

}


