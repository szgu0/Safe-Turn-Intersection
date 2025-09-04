using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 oldPosition = transform.position;
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            transform.position = raycastHit.point;

            float dis  = Vector3.SqrMagnitude(transform.position-Camera.main.transform.position);
            if (dis < 15.0f)
            {
                transform.localScale = Vector3.one * dis / 10.0f;
            }

            // Remember this distance in case we click and drag the mouse
            //noSteamVRFallbackInteractorDistance = Mathf.Min(noSteamVRFallbackMaxDistanceNoItem, raycastHit.distance);
        }
        else
        {
            // Didn't hit, just leave it where it was
            transform.position = new Vector3(1000f, 1000f, 1000f);
        }

    }
}
