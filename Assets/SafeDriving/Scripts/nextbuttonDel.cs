using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextbuttonDel : MonoBehaviour
{
    public GameObject next;
    public GameObject mode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode.activeInHierarchy)
        {
            next.SetActive(false);
        }
    }
}
