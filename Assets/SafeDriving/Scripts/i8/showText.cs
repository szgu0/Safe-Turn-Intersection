using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour
{
    public GameObject text;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (button.activeInHierarchy)
        {
            text.SetActive(true);
        }
        else { text.SetActive(false); }
       

    }
}
