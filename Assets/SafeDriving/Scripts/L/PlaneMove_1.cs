using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove_1 : MonoBehaviour
{
    public Transform fixplane;
    Transform cube;
    Vector3 normal;
    void Start()
    {
        //計算平面法向量
        cube = fixplane.Find("Cube");
        cube.localPosition = new Vector3(0, 1, 0);
        normal = Vector3.Normalize(cube.position - fixplane.position);
    }

    public Vector3 projectposition(Vector3 initposition)
    {
        var dir = initposition - fixplane.position;
        var pj = Vector3.ProjectOnPlane(dir, normal);
        var newposition = fixplane.position + pj;
        return newposition;
    }

    private void LateUpdate()
    {
        Vector3 nowposition = transform.position;
        cube.position = projectposition(transform.position);
       
        if(cube.localPosition.x > 5.0f)
        {
            cube.localPosition = new Vector3(5.0f, cube.localPosition.y, cube.localPosition.z);
        }
        if (cube.localPosition.x < -5.0f)
        {
            cube.localPosition = new Vector3(-5.0f, cube.localPosition.y, cube.localPosition.z);
        }
        if (cube.localPosition.z > 5.0f)
        {
            cube.localPosition = new Vector3(cube.localPosition.x, cube.localPosition.y, 5.0f);
        }
        if (cube.localPosition.z < -5.0f)
        {
            cube.localPosition = new Vector3(cube.localPosition.x, cube.localPosition.y, -5.0f);
        }

        transform.position = cube.position;
    }
}
