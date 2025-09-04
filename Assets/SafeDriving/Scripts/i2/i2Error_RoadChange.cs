using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i2Error_RoadChange : MonoBehaviour
{
    public float RightCarSpeed;
    public float CarSpeed;

    public PathCreator[] path;
    public PathCreator[] error_path;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RightCarSpeed = GetComponent<I8RoadStart>().rightCarSpeedKMHr;
        CarSpeed=GetComponent<I8RoadStart>().CarSpeedKMHr;

        
        if (path.Length != error_path.Length)
        {
            Debug.LogError("Path and Error_Path arrays must have the same length!");
            return;
        }

        if (RightCarSpeed < CarSpeed)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null && path[i].gameObject.activeSelf)
                {
                  
                    path[i].gameObject.SetActive(false);

                   
                    if (error_path[i] != null)
                    {
                        error_path[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
