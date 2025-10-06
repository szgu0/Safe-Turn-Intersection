using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraObject : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
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
