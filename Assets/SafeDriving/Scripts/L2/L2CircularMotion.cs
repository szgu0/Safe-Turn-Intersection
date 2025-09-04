using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace XCharts.Runtime
{
    public class L2CircularMotion : MonoBehaviour
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
        [Range(0.3f, 1)]
        [SerializeField]
        private float sphereSize = 1f;

        [SerializeField]
        private float angularspeed;
        [Range(1, 7)]
        [SerializeField]
        private float aroundRadius = 5f;// 初始長度



        [SerializeField]
        private float angled;

        [SerializeField]
        private LineChartController lineChart;

        private bool isStop;

        [SerializeField]
        private GameObject aArrow;
        [SerializeField]
        private GameObject vArrow;

        [SerializeField]
        private GameObject wei;

        [SerializeField]
        private TMP_Text Btn_text;

        [SerializeField]
        private GameObject wei2;
        [SerializeField]
        private GameObject wei21;
        [SerializeField]
        private GameObject wei3;
        [SerializeField]
        private GameObject wei4;
        [SerializeField]
        private GameObject wei5;
        private int weinum = 1;
        private int weiE = 1;

        [SerializeField]
        private TextMeshProUGUI tmpTextValue;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue1;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue2;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue3;
        [SerializeField]
        private Slider sliderValue;
        [SerializeField]
        private Slider sliderValue1;

        [SerializeField]
        private Slider sliderValue2;
        [SerializeField]
        private Slider sliderValue3;
        [SerializeField]
        private GameObject detector;

        private AudioSource audioSource;

        public int angledCount;

        void Start()
        {
            Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
            transform.localPosition = new Vector3(p.x, aroundPoint.localPosition.y, p.z);
            isStop = false;

            audioSource = GetComponent<AudioSource>();
            ChangeValueText();
        }

        void FixedUpdate()
        {
            if (isStop)
            {
                transform.localScale = Vector3.one * sphereSize;
                //sphere2.transform.localScale = Vector3.one * sphereSize;
                float stopAngled = angled - (angularspeed / aroundRadius) / 10 % 360;
                float stopposX = aroundRadius * Mathf.Sin(stopAngled * Mathf.Deg2Rad);
                float stopposZ = aroundRadius * Mathf.Cos(stopAngled * Mathf.Deg2Rad);
                //sphere2.transform.localPosition = new Vector3(stopposX, 0.83f, stopposZ) + aroundPoint.localPosition;

                wei.transform.localPosition = new Vector3(0, 2.5f / (aroundRadius / 2 + 0.5f), 0);

                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);
                transform.localPosition = new Vector3(posX, 0.83f, posZ) + aroundPoint.localPosition;

                aArrow.transform.localPosition = transform.localPosition;
                aArrow.transform.localRotation = transform.localRotation;
                vArrow.transform.localPosition = transform.localPosition;
                vArrow.transform.localRotation = Quaternion.Euler(0, angled + 180, 0);

                CLine.transform.localPosition = new Vector3(posX / 2 * 1.17f, 0.576f, posZ / 2 * 1.17f) + aroundPoint.localPosition;
                //CLine.transform.LookAt(transform.position,  new Vector3(0,1,0));
                CLine.transform.localRotation = Quaternion.Euler(85, angled, 0);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, (aroundRadius + 0.5f) / 2, CLine.transform.localScale.z);
            }
            else
            {

                transform.localScale = Vector3.one * sphereSize;

                angled += (angularspeed * Time.deltaTime) % 360;

                lineChart.LineUpdate(angularspeed / 5 / aroundRadius);

                wei.transform.localPosition = new Vector3(0, 3.5f + 0.3f * (aroundRadius / 2 + 0.5f), 0);

                // if ((angled + 90) % 360 <= 5)
                // {
                //     audioSource.Play();
                // }
                if(angled>270+(angledCount*360))
                {
                    audioSource.PlayOneShot(audioSource.clip);
                    angledCount++;
                }


                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

                transform.localPosition = new Vector3(posX, 0.83f, posZ) + aroundPoint.localPosition;
                transform.LookAt(aroundPoint.position, new Vector3(0, 1, 0));

                detector.transform.localPosition = new Vector3(-aroundRadius, detector.transform.localPosition.y, detector.transform.localPosition.z);

                aArrow.transform.localPosition = transform.localPosition;
                aArrow.transform.localRotation = transform.localRotation;
                vArrow.transform.localPosition = transform.localPosition;
                vArrow.transform.localRotation = Quaternion.Euler(0, angled + 180, 0);

                aArrow.transform.localScale = new Vector3(30, 30, 50) * ((angularspeed / 5000) + 0.3f);
                vArrow.transform.localScale = new Vector3(30, 30, 50) * ((angularspeed / 5000) + 0.3f);

                CLine.transform.localPosition = new Vector3(posX / 2 * 1.17f, 0.576f, posZ / 2 * 1.17f) + aroundPoint.localPosition;
                //CLine.transform.LookAt(transform.position,  new Vector3(0,1,0));
                CLine.transform.localRotation = Quaternion.Euler(85, angled, 0);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, (aroundRadius + 0.38f) / 2, CLine.transform.localScale.z);
            }
        }

        public void ChangeSpeed(float speed)
        {
            angularspeed = speed;
        }
        public void ChangeSize(float size)
        {
            sphereSize = size;
        }
        public void ChangeRadius(float radius)
        {
            aroundRadius = 2 + radius;
            ChangeValueText();
        }
        public void ChangeStop()
        {
            if (isStop)
            {
                isStop = false;
                //sphere2.SetActive(false);
                Btn_text.text = "暫停";
            }
            else
            {
                isStop = true;
                //sphere2.SetActive(true);
                Btn_text.text = "繼續";

            }
        }
        public void weiE2()
        {

            wei4.SetActive(false);
            wei21.SetActive(true);
            wei3.SetActive(true);
            wei2.SetActive(false);
            weinum = 4;
            weiE = 2;
            aroundRadius=5;
            ChangeValueText();
        }

        public void weiAdd(int num)
        {
            if (weiE == 1)
            {
                if (weinum == 4)
                {
                    wei4.SetActive(true);
                    wei21.SetActive(false);
                    weinum = 9;
                    ChangeValueText();
                }
                else if (weinum == 1 && num == 1)
                {
                    wei3.SetActive(true);
                    wei2.SetActive(false);
                    weinum = 4;
                    ChangeValueText();
                }
            }

        }
        public void weiMinus()
        {
            if (weiE == 1)
            {
                if (weinum == 4)
                {
                    wei3.SetActive(false);
                    wei2.SetActive(true);
                    weinum = 1;
                    ChangeValueText();
                }
                else if (weinum == 9)
                {
                    wei4.SetActive(false);
                    wei21.SetActive(true);
                    weinum = 4;
                    ChangeValueText();
                }
            }


        }

        public void ChangeValueText()
        {
            float valueRadius = (aroundRadius - 1) * 10;
            float ballM = 400;
            float BallVelocity = Mathf.Sqrt(weinum * 100 * 1000 * valueRadius / ballM) / 100;
            float AngularVelocity = BallVelocity * 100 / valueRadius;
            angularspeed = AngularVelocity * 57;

            sliderValue.value = AngularVelocity;
            tmpTextValue.text = string.Format("{0:f3}", AngularVelocity);

            sliderValue1.value = valueRadius;
            tmpTextValue1.text = $"{valueRadius / 100}";

            sliderValue2.value = BallVelocity;
            tmpTextValue2.text = string.Format("{0:f3}", BallVelocity);

            sliderValue3.value = weinum * 100;
            tmpTextValue3.text = $"{weinum * 100}";
        }
    }
}