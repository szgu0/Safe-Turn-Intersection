using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRunError : MonoBehaviour
{
    public CarPathFollower carPathFollower;

    bool isTrrgger = true;
    public void Start()
    {
        isTrrgger = true;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(isTrrgger)
        {
            carPathFollower.EndDriver();
            isTrrgger=false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTrrgger)
        {
            carPathFollower.EndDriver();
            isTrrgger = false;
        }
    }
}
