using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPointer : MonoBehaviour
{
    public ReticlePoser reticlePoser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float s = reticlePoser.hitDistance;
        if (s > 0 && s < 0.3f)
        {
            s = 0.1f;
        }
        else if (s > 0.3 && s < 0.6f)
        {
            s = 0.2f;
        }
        else if (s > 0.6f && s < 2.0f)
        {
            s = 0.3f;
        }
        else if (s > 2.0f)
        {
            s = 1.0f;
        }
        transform.localScale = new Vector3 (s, s, s);


    }
}
