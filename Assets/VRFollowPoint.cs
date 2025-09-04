using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFollowPoint : MonoBehaviour
{
    public Transform driverP;
    public Transform outlookP;
    public Transform followP;

    public Transform VRPlyer;

    public CameraCtlr cameraCtlr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraCtlr.cameraCount == 2)
        {
            VRPlyer.transform.position = driverP.transform.position;
        }

        else if (cameraCtlr.cameraCount == 1)
        {
            VRPlyer.transform.position = outlookP.transform.position;
        }

        else if (cameraCtlr.cameraCount == 3)
        {
            VRPlyer.transform.position = followP.transform.position;
        }
    }
}
