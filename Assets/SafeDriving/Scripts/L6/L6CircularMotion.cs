using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace XCharts.Runtime
{
    public class L6CircularMotion : MonoBehaviour
    {
        [SerializeField]
        private Material metal_Material;
        [SerializeField]
        private Material w_Material;
        [SerializeField]
        private Transform aroundPoint; // 柱子的位置
        [SerializeField]
        private GameObject Load; // 球體
        [SerializeField]
        private GameObject sphere2; // 球體
        [SerializeField]
        private GameObject CLine; // 線段
                                  //public float forceMagnitude = 10f; // 施加在球體上的力的大小
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private float sphereSize = 1f;

        [SerializeField]
        private float angularspeed;

        [SerializeField]
        private float aroundRadius = 5f;// 初始長度



        [SerializeField]
        private float angled;

        [SerializeField]
        private LineChartController lineChart;

        private bool isStop;

        private Vector3 originalScale;
        [Range(0, 45)]
        [SerializeField]
        private float plateAngle;
        [SerializeField]
        private float degree;
        [SerializeField]
        private float aroundLong;
        [Range(0.1f, 1)]
        [SerializeField]
        private float friction;

        private bool isPopout;
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
        private float startAngled;

        public Renderer rendererToChange;

        public bool isWood = false;
        void Start()
        {
            Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
            transform.localPosition = new Vector3(p.x, aroundPoint.localPosition.y, p.z);
            isStop = false;
            originalScale = Load.transform.localScale;
            degree = Mathf.PI / 4;
        }

        async void Update()
        {
            if (isStop)
            {
                Load.transform.localScale = originalScale * sphereSize;
                aroundRadius = aroundLong * Mathf.Sin(degree);
                //angled += (angularspeed / aroundRadius * Time.deltaTime) % 360;
                //lineChart.LineUpdate(angularspeed / 5 / aroundRadius);

                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                transform.localPosition = new Vector3(posX, -1f * aroundRadius / Mathf.Sin(degree) * Mathf.Cos(degree), posZ) + aroundPoint.localPosition;
                transform.LookAt(aroundPoint.position);
                transform.Rotate(-plateAngle, 0, 0, Space.Self);


                //Load.transform.localPosition = new Vector3(posX, -7.37f, posZ) + aroundPoint.localPosition;
                aroundRadius = (aroundLong-3) * Mathf.Sin(degree);
                posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                CLine.transform.localPosition = new Vector3(posX / 2, -1f * aroundRadius / Mathf.Sin(degree) * Mathf.Cos(degree) / 2f, posZ / 2) + aroundPoint.localPosition;
                CLine.transform.LookAt(transform.position, new Vector3(0, 1, 0));
                CLine.transform.Rotate(90, 0, 0, Space.Self);
                //CLine.transform.localRotation = Quaternion.Euler(150, angled, 0);
                float distance = Vector3.Distance(aroundPoint.localPosition, transform.localPosition)-3;
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, distance / 2, CLine.transform.localScale.z);
            }
            else
            {

                Load.transform.localScale = originalScale * sphereSize;



                aroundRadius = aroundLong * Mathf.Sin(degree);

                angled += (angularspeed / aroundRadius * Time.deltaTime) % 360;

                lineChart.LineUpdate(angularspeed / 5 / aroundRadius);


                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                transform.localPosition = new Vector3(posX, -1f * aroundRadius / Mathf.Sin(degree) * Mathf.Cos(degree), posZ) + aroundPoint.localPosition;
                transform.LookAt(aroundPoint.position);
                transform.Rotate(-plateAngle, 0, 0, Space.Self);

                if (!isPopout && angled > startAngled + 90)
                {
                    if (isWood)
                    {
                        float f = (10 * Mathf.Sin(degree)  * sphereSize) - ((angularspeed / 100) * (angularspeed / 100) / ((aroundLong - 1) * 2)* Mathf.Sqrt(2) * Mathf.Cos(degree) * Mathf.Cos(degree) * sphereSize);
                        Debug.Log(f);
                        if(f>friction+0.01f)
                        {
                            popout();
                        }
                        if(f<-friction-0.01f)
                        {
                            popin();
                        }
                    }
                    else
                    {
                        //Debug.Log($"{(angularspeed / 100) * (angularspeed / 100) / ((aroundLong - 1) * 2) * sphereSize * 10},右:{ sphereSize * 10 * 10}");
                        if ((angularspeed / 100) * (angularspeed / 100) / ((aroundLong - 1) * 2) * sphereSize * 10 > sphereSize * 10 * 10)
                        {
                            popout();
                        }
                        else if ((angularspeed / 100) * (angularspeed / 100) / ((aroundLong - 1) * 2) * sphereSize * 10 < sphereSize * 10 * 10)
                        {
                            popin();
                        }
                    }
                }



                aroundRadius = (aroundLong-3) * Mathf.Sin(degree);
                posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                //Load.transform.localPosition = new Vector3(posX, -7.37f, posZ) + aroundPoint.localPosition;

                CLine.transform.localPosition = new Vector3(posX / 2, -1f * aroundRadius / Mathf.Sin(degree) * Mathf.Cos(degree) / 2f, posZ / 2) + aroundPoint.localPosition;
                CLine.transform.LookAt(transform.position, new Vector3(0, 1, 0));
                CLine.transform.Rotate(90, 0, 0, Space.Self);
                //CLine.transform.localRotation = Quaternion.Euler(150, angled, 0);
                float distance = Vector3.Distance(aroundPoint.localPosition, transform.localPosition)-3;
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, distance / 2, CLine.transform.localScale.z);
            }
        }

        public void ChangeSpeed(float speed)
        {
            angularspeed = speed * 100;
            tmpTextValue2.text = speed + "";
        }
        public void ChangeSize(float size)
        {
            sphereSize = size;
            tmpTextValue3.text = string.Format("{0:f0}", size * 10);
        }
        public void ChangeRadius(float radius)
        {
            aroundLong = radius;
            tmpTextValue.text = (radius - 1) * 2 + "√ 2";
        }
        public void ChangePlateAngle(float Angle)
        {
            Angle = Angle * 5;
            degree = Angle * Mathf.Deg2Rad;
            tmpTextValue1.text = Angle + "°";
        }
        public void ChangeFriction(float num)
        {
            friction = num;
            tmpTextValue4.text = string.Format("{0:f1}", num);
        }

        public async void ChangeMaterial()
        {
            rendererToChange.material = w_Material;
            isWood = true;
        }

        IEnumerator PopFall()
        {
            startButton.interactable = false;
            yield return new WaitForSeconds(2f);
            startButton.interactable = true;
            isStop = true;
        }



        private void popin()
        {
            //Load.AddComponent<Rigidbody>().isKinematic = false;
            Load.AddComponent<Rigidbody>();
            Vector3 randomDirection = new Vector3(
                                    Mathf.Sin(angled * Mathf.Deg2Rad),
                                    Random.Range(0f, 0.02f),
                                    Mathf.Cos(angled * Mathf.Deg2Rad)
                                );
            // 将向量标准化为单位向量
            randomDirection.Normalize();
            Load.GetComponent<Rigidbody>().AddForce(randomDirection * 1f, ForceMode.Impulse);
            isPopout = true;
            StartCoroutine("PopFall");
        }
        private void popout()
        {
            //Load.AddComponent<Rigidbody>().isKinematic = false;
            Load.AddComponent<Rigidbody>();
            Vector3 randomDirection = new Vector3(
                                    -1f * Mathf.Sin(angled * Mathf.Deg2Rad),
                                    Random.Range(-1f, 0f),
                                    -1f * Mathf.Cos(angled * Mathf.Deg2Rad)
                                );
            // 将向量标准化为单位向量
            randomDirection.Normalize();
            Load.GetComponent<Rigidbody>().AddForce(randomDirection * 5f, ForceMode.Impulse);
            isPopout = true;
            StartCoroutine("PopFall");
        }

        public void ResetAll()
        {
            Destroy(Load.GetComponent<Rigidbody>());
            Load.transform.localPosition = new Vector3(0, 0, 0);
            Load.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //Load.GetComponent<Rigidbody>().isKinematic = true;


            angularspeed = 1000;
            friction = 1;
            plateAngle = 0;
            sphereSize = 1;
            isPopout = false;
        }
        public void StartMoving()
        {
            Destroy(Load.GetComponent<Rigidbody>());
            Load.transform.localPosition = new Vector3(0, 0, 0);
            Load.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //Load.GetComponent<Rigidbody>().isKinematic = true;
            isPopout = false;
            isStop = false;
            startAngled = angled;



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
