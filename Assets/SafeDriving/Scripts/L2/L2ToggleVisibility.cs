using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XCharts.Runtime
{
    public class L2ToggleVisibility : MonoBehaviour
    {
        public L2CircularMotion l2CircularMotion;
        // Start is called before the first frame update
        void Start()
        {
            l2CircularMotion = GameObject.Find("Sphere").GetComponent<L2CircularMotion>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnMouseDown()
        {

            Debug.Log("ss");
        }
    }
}