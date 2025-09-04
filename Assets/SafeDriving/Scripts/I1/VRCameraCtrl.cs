using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject VRCarema;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 myver = new Vector3(0, 1.7f, 0);
        transform.localPosition = myver - VRCarema.transform.localPosition;
    }
}
