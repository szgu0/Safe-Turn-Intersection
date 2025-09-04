using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace XCharts.Runtime
{
    public class L3CircularMotion : MonoBehaviour
    {
        [SerializeField]
        private Transform aroundPoint; // 柱子的位置
        [SerializeField]
        private GameObject sphere; // 球體
        [SerializeField]
        private GameObject sphere2; // 球體
        [SerializeField]
        private GameObject CLine; // 線段
                                  //public float forceMagnitude = 10f; // 施加在球體上的力的大小
        [SerializeField]
        private float sphereSize = 1f;

        [SerializeField]
        private float angularspeed;
        [SerializeField]
        private float aroundRadius = 5f;// 初始長度
        [SerializeField]
        private float BallGravity;
        [SerializeField]
        private float aroundLong;



        [SerializeField]
        private float angled;

        [SerializeField]
        private LineChartController lineChart;

        private bool isStop;
        [SerializeField]
        private TextMeshProUGUI tmpTextValuelong;
        [SerializeField]
        private TextMeshProUGUI tmpTextValuespeed;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue1;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue2;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue3;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue4;
        [SerializeField]
        private Slider sliderValuelong;
        [SerializeField]
        private Slider sliderValuespeed;
        [SerializeField]
        private Slider sliderValue;
        [SerializeField]
        private Slider sliderValue1;
        
        [SerializeField]
        private Slider sliderValue2;
        [SerializeField]
        private Slider sliderValue3;
        [SerializeField]
        private Slider sliderValue4;
        [SerializeField]
        private float degree;
        void Start()
        {
            Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
            transform.localPosition = new Vector3(p.x, aroundPoint.localPosition.y, p.z);
            isStop = false;
            
        }

        void Update()
        {
            if (isStop)
            {
                transform.localScale = Vector3.one * sphereSize;
                //sphere2.transform.localScale = Vector3.one * sphereSize;
                float stopAngled = angled - (angularspeed / aroundRadius) / 10 % 360;
                float stopposX = aroundRadius * Mathf.Sin(stopAngled * Mathf.Deg2Rad);
                float stopposZ = aroundRadius * Mathf.Cos(stopAngled * Mathf.Deg2Rad);
                //sphere2.transform.localPosition = new Vector3(stopposX, -8, stopposZ) + aroundPoint.localPosition;

                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                transform.localPosition = new Vector3(posX, -8, posZ) + aroundPoint.localPosition;

                CLine.transform.localPosition = new Vector3(posX / 2, -4, posZ / 2) + aroundPoint.localPosition;
                CLine.transform.LookAt(transform.position, new Vector3(0, 1, 0));
                CLine.transform.Rotate(90, 0, 0, Space.Self);
                
                //CLine.transform.localRotation = Quaternion.Euler(150, angled, 0);
                float distance = Vector3.Distance(aroundPoint.localPosition, transform.localPosition);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, distance / 2, CLine.transform.localScale.z);
            }
            else
            {

                transform.localScale = Vector3.one * sphereSize;

                degree = Mathf.Atan((angularspeed/200)*(angularspeed/200)/BallGravity/aroundLong);

                aroundRadius = aroundLong*Mathf.Sin(degree);

                angled += (angularspeed / aroundRadius * Time.deltaTime) % 360;

                lineChart.LineUpdate(angularspeed / 5 / aroundRadius);

                


                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);
                // degree = Mathf.PI/(2*(4.5f-(angularspeed/1000)));

                // transform.localPosition = new Vector3(posX, -1f*aroundRadius/Mathf.Sin(degree)*Mathf.Cos(degree), posZ) + aroundPoint.localPosition;

                // CLine.transform.localPosition = new Vector3(posX / 2, -1f*aroundRadius/Mathf.Sin(degree)*Mathf.Cos(degree)/2f, posZ / 2) + aroundPoint.localPos

                

                transform.localPosition = new Vector3(posX, -1f*aroundRadius/Mathf.Sin(degree)*Mathf.Cos(degree), posZ) + aroundPoint.localPosition;

                CLine.transform.localPosition = new Vector3(posX / 2, -1f*aroundRadius/Mathf.Sin(degree)*Mathf.Cos(degree)/2f, posZ / 2) + aroundPoint.localPosition;
                CLine.transform.LookAt(transform.position, new Vector3(0, 1, 0));
                CLine.transform.Rotate(90, 0, 0, Space.Self);
                //CLine.transform.localRotation = Quaternion.Euler(150, angled, 0);
                float distance = Vector3.Distance(aroundPoint.localPosition, transform.localPosition);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, distance / 2, CLine.transform.localScale.z);
            }
        }

        public void ChangeSpeed(float speed)
        {
            angularspeed = speed;
            ChangeFormula();
        }
        public void ChangeSize(float size)
        {
            sphereSize = size;
        }
        public void ChangeGravity(float gravity)
        {
            BallGravity = gravity;
            ChangeFormula();
        }
        public void ChangeRadius(float radius)
        {
            aroundLong = radius;
            ChangeFormula();
        }

        public void ChangeFormula()
        {
            float fx = (angularspeed/200)*(angularspeed/200)/aroundLong/Mathf.Sin(degree);
            sliderValue1.value = fx;
            sliderValue2.value = BallGravity;
            sliderValue3.value = BallGravity/fx;
            sliderValue4.value = Mathf.Sin(degree);
            tmpTextValuespeed.text = string.Format("{0:f1}", angularspeed/200);
            tmpTextValuelong.text = string.Format("{0:f1}", aroundLong);
            tmpTextValue.text = BallGravity + "";
            tmpTextValue1.text = string.Format("{0:f1}", fx);
            tmpTextValue2.text = BallGravity + "";
            tmpTextValue3.text = string.Format("{0:f4}", BallGravity/fx);
            tmpTextValue4.text = string.Format("{0:f4}", Mathf.Sin(degree));
            
        }
        public void ChangeStop()
        {
            if (isStop)
            {
                isStop = false;
                //sphere2.SetActive(false);
            }
            else
            {
                isStop = true;
                //sphere2.SetActive(true);
            }
        }
    }
}
