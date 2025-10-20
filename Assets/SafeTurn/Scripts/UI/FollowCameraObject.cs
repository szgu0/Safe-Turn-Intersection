using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraObject : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
        // transform.rotation = Camera.main.transform.rotation;

        // 取得目前物件的 rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;
        // 取得相機的 rotation
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;

        // 保留原本的 X，套用相機的 Y、Z
        transform.rotation = Quaternion.Euler(currentRotation.x, cameraRotation.y, cameraRotation.z);
        // transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
    }

    private void FixedUpdate()
    {
        //transform.position = Camera.main.transform.position;
        //transform.rotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        //transform.position = Camera.main.transform.position;
        //transform.rotation = Camera.main.transform.rotation;
    }
}
