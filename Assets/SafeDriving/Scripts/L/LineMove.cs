using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    Quaternion myrotation;

    public float linearMappingvalue;
    // Start is called before the first frame update
    void Start()
    {
        myrotation = transform.rotation;
    }

    protected float CalculateLinearMapping(Transform updateTransform)
    {
        Vector3 direction = endPosition.position - startPosition.position;
        float length = direction.magnitude;
        direction.Normalize();

        Vector3 displacement = updateTransform.position - startPosition.position;

        return Vector3.Dot(displacement, direction) / length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        linearMappingvalue = CalculateLinearMapping(transform);
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMappingvalue);
        transform.rotation = myrotation;
    }
}
