using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPNG : MonoBehaviour
{
    public GameObject START;
    public GameObject png;
    // Start is called before the first frame update
    void Start()
    {
        png.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(START.activeInHierarchy)
        {
            png.SetActive(true);
        }else if (!START.activeInHierarchy)
        {
            png.SetActive(false);
        }
    }
}
