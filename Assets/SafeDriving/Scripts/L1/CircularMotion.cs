using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


namespace XCharts.Runtime
{
    public class CircularMotion : MonoBehaviour
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
        private float sphereSize = 1.6f;

        [SerializeField]
        private float angularspeed;
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

        private AudioSource audioSource;

        [SerializeField]
        private TextMeshProUGUI tmpTextValue;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue1;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue2;

        [SerializeField]
        private Slider sliderValue2;

        [SerializeField]
        private GameObject detector;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue3;

        [SerializeField]
        private Slider sliderValue3;
        [SerializeField]
        private TextMeshProUGUI tmpTextValue4;

        [SerializeField]
        private Slider sliderValue4;

        private bool isVoiceOn = true;
        public int angledCount;
        void Start()
        {
            Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
            transform.localPosition = new Vector3(p.x, aroundPoint.localPosition.y, p.z);
            audioSource = GetComponent<AudioSource>();
            // StartCoroutine("AudioFPlay");

            isStop = false;
        }

        void FixedUpdate()
        {
            if (isStop)
            {
                transform.localScale = Vector3.one * sphereSize;
                sphere2.transform.localScale = Vector3.one * sphereSize;
                float stopAngled = angled - (angularspeed / aroundRadius) / 10 % 360;
                float stopposX = aroundRadius * Mathf.Sin(stopAngled * Mathf.Deg2Rad);
                float stopposZ = aroundRadius * Mathf.Cos(stopAngled * Mathf.Deg2Rad);
                sphere2.transform.localPosition = new Vector3(stopposX, 0.83f, stopposZ) + aroundPoint.localPosition;

                float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
                float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);
                transform.localPosition = new Vector3(posX, 0.83f, posZ) + aroundPoint.localPosition;

                CLine.transform.localPosition = new Vector3(posX / 2 * 1.17f, 0.576f, posZ / 2 * 1.17f) + aroundPoint.localPosition;
                //CLine.transform.LookAt(transform.position,  new Vector3(0,1,0));
                CLine.transform.localRotation = Quaternion.Euler(85, angled, 0);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, aroundRadius / 2, CLine.transform.localScale.z);
            }
            else
            {

                transform.localScale = Vector3.one * sphereSize;

                // angled += (angularspeed / aroundRadius * Time.deltaTime) % 360;
                angled += (angularspeed / 171 * 180 * Time.deltaTime) % 360;

                lineChart.LineUpdate(angularspeed * aroundRadius / 5);

                // if((angled+90)%360<=3){
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

                // aArrow.transform.localPosition = transform.localPosition;
                // aArrow.transform.localRotation = transform.localRotation;
                // vArrow.transform.localPosition = transform.localPosition;
                // vArrow.transform.localRotation = Quaternion.Euler(0, angled+180, 0);

                // aArrow.transform.localScale = new Vector3(30,30,50) * ((angularspeed/5000)+0.3f);
                // vArrow.transform.localScale = new Vector3(30,30,50) * ((angularspeed/5000)+0.3f);

                CLine.transform.localPosition = new Vector3(posX / 2 * 1.17f, 0.576f, posZ / 2 * 1.17f) + aroundPoint.localPosition;
                //CLine.transform.LookAt(transform.position,  new Vector3(0,1,0));
                CLine.transform.localRotation = Quaternion.Euler(85, angled, 0);
                CLine.transform.localScale = new Vector3(CLine.transform.localScale.x, aroundRadius / 2, CLine.transform.localScale.z);
            }
        }

        IEnumerator AudioFPlay()
        {
            while (true)
            {
                audioSource.Play();
                yield return new WaitForSeconds(sliderValue3.value);
                
            }
        }



        public void ChangeSpeed(float speed)
        {
            speed = Mathf.Pow(2, speed - 1);
            angularspeed = speed * 171;
            tmpTextValue.text = $"{speed}π";
            tmpTextValue2.text = string.Format("{0:f1}", aroundRadius / 1.5f / 10 * angularspeed / 171);
            sliderValue2.value = float.Parse(string.Format("{0:f1}", aroundRadius / 1.5f / 10 * angularspeed / 171));
            tmpTextValue3.text = $"{2 / speed}";
            sliderValue3.value = 2 / speed;
            tmpTextValue4.text = $"{speed / 2}";
            sliderValue4.value = speed / 2;
            angled = 270;
            angledCount = 1;
            // StopCoroutine("AudioFPlay");
            // StartCoroutine("AudioFPlay");
        }
        public void ChangeSize(float size)
        {
            sphereSize = size;
        }
        public void ChangeRadius(float radius)
        {
            aroundRadius = radius * 1.5f;
            tmpTextValue1.text = $"{radius / 10}";
            tmpTextValue2.text = string.Format("{0:f1}", aroundRadius / 1.5f / 10 * angularspeed / 171);
            sliderValue2.value = float.Parse(string.Format("{0:f1}", aroundRadius / 1.5f / 10 * angularspeed / 171));
        }
        public void ChangeStop()
        {
            if (isStop)
            {
                isStop = false;
                sphere2.SetActive(false);
            }
            else
            {
                isStop = true;
                sphere2.SetActive(true);
            }
        }
    }
}