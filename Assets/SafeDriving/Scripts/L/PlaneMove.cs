using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public bool freeze_x;
    public bool freeze_y;
    public bool freeze_z;

    Vector3 myposition;
    Quaternion myrotation;

    public float linearMappingvalue;
    // Start is called before the first frame update
    void Start()
    {
        myposition = transform.position;
        myrotation = transform.rotation;
    }

    /*
    protected float CalculateLinearMapping(Transform updateTransform)
    {
        Vector3 direction = endPosition.position - startPosition.position;
        float length = direction.magnitude;
        direction.Normalize();

        Vector3 displacement = updateTransform.position - startPosition.position;

        return Vector3.Dot(displacement, direction) / length;
    }
    */
    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //linearMappingvalue = CalculateLinearMapping(transform);
        Vector3 nowposition = transform.position;
        if(freeze_x)
        {
            nowposition = new Vector3(myposition.x, nowposition.y, nowposition.z);
        }
        if (freeze_y)
        {
            nowposition = new Vector3(nowposition.x, myposition.y, nowposition.z);
        }
        if (freeze_z)
        {
            nowposition = new Vector3(nowposition.x, nowposition.y, myposition.z);
        }
        transform.position = nowposition;
        transform.rotation = myrotation;
    }
}
