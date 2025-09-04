using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerFollow : MonoBehaviour
{

    //public GameObject VRPlayerPos;

    public GameObject VRPlayer;
    //public GameObject VR1;
    //public GameObject VR2;


    public GameObject PCPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //VRPlayerPos.transform.position = gameObject.transform.position;

        VRPlayer.transform.position = gameObject.transform.position;
        //VR1.transform.position = gameObject.transform.position;
        //VR2.transform.position = gameObject.transform.position;

        PCPlayer.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
