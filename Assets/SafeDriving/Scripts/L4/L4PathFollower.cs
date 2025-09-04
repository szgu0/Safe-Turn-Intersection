using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;
using TMPro;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class L4PathFollower : MonoBehaviour
    {
        [SerializeField]
        private GameObject Balls; // 球體
        public PathCreator pathCreator;
        public PathCreator pathCreator2;
        public PathCreator pathCreator3;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        private bool isStop;

        [SerializeField]
        private float TrainSpeed = 0.5f;

        [SerializeField]
        private LineChartController lineChart;
        private bool isFlip;
        private bool isFall;

        public int pathNum = 1;

        [SerializeField]
        private TextMeshProUGUI tmpTextVa;
        [SerializeField]
        private TextMeshProUGUI tmpTextVc;

        public bool isStepTwo;
        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            isFall = false;
            Balls.SetActive(false);
            StartCoroutine("BallReset");
        }

        void FixedUpdate()
        {
            if (pathCreator != null && !isStop)
            {
                if (pathNum == 3) 
                {
                    if(distanceTravelled<9.2f)lineChart.LineUpdate(TrainSpeed);
                }
                else
                {
                    lineChart.LineUpdate(TrainSpeed);
                }
                

                if (!isFlip)
                {
                    distanceTravelled += TrainSpeed * Time.deltaTime;
                    if (pathNum == 1)
                    {
                        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        TrainSpeed = 1f + ((2 - transform.position.y) * 10);
                        if (transform.position.x > 0 && isStepTwo)
                        {
                            tmpTextVa.text = "<sprite=5>";
                            if (transform.position.y > 1.5f)
                            {
                                tmpTextVc.text = "<sprite=2>";
                            }
                        }
                    }
                    else if (pathNum == 2)
                    {
                        transform.position = pathCreator2.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                        transform.rotation = pathCreator2.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        TrainSpeed = 0.5f + ((1.8f - transform.position.y) * 10);
                        if (transform.position.x > 0 && isStepTwo)
                        {
                            tmpTextVa.text = "<sprite=4>";
                            if (transform.position.y > 1.5f)
                            {
                                tmpTextVc.text = "<sprite=1>";
                            }
                        }
                    }
                    else if (pathNum == 3)
                    {
                        transform.position = pathCreator3.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                        transform.rotation = pathCreator3.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        TrainSpeed = 0.5f + ((1.6f - transform.position.y) * 10);
                        if (transform.position.x > 0 && isStepTwo) 
                        {
                            tmpTextVa.text = "<sprite=2>";
                            if (transform.position.y > 1.4f)
                            {
                                tmpTextVc.text = "0";
                            }
                        }
                    }

                    if (transform.position.x > 1f)
                    {
                        Balls.SetActive(true);
                    }
                }



                //isFlip = true;
                //StartCoroutine("FallAndReset");


            }
        }
        IEnumerator FallAndReset()
        {
            Vector3 fallRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, 0f); // 倒下时的旋转角度

            float startTime = Time.time;// 记录开始时间
            float cost = 0.13f / TrainSpeed;
            // 计算目标旋转
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(fallRotation);

            Vector3 startPosition = transform.localPosition;
            Vector3 endPosition = transform.localPosition + new Vector3(-0.12f, 0, 0.15f);

            while (Time.time < startTime + cost)
            {
                // 插值计算旋转角度
                float t = (Time.time - startTime) / cost;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
                yield return null;
            }

            // 确保最终旋转角度
            transform.localRotation = endRotation;

            // 等待3秒
            yield return new WaitForSeconds(1f);
            transform.position = pathCreator.path.GetPoint(0);
            transform.rotation = pathCreator.path.GetRotation(0.1f);
            distanceTravelled = 0;
            isFlip = false;
        }

        IEnumerator BallReset()
        {
            if(pathNum==1)
            {
                yield return new WaitForSeconds(3.2f);
            }
            if(pathNum==2)
            {
                yield return new WaitForSeconds(3.1f);
            }
            if(pathNum==3)
            {
                yield return new WaitForSeconds(3.2f);
            }
            
            Balls.SetActive(true);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void ChangeSpeed(float speed)
        {
            TrainSpeed = speed;

        }

        public void PathRun(int pathnum)
        {
            distanceTravelled = 0;
            pathNum = pathnum;
            Balls.SetActive(false);
            isFall = false;

            if(isStepTwo)
            {
                tmpTextVa.text = "";
                tmpTextVc.text = "";
            }
            
            lineChart.gameObject.GetComponent<LineChart>().ClearData();
            // if(pathnum==3)StartCoroutine("BallReset");
            StartCoroutine("BallReset");
        }
        public void isStep()
        {
            isStepTwo = true;
        }

        public void ChangeStop()
        {
            if (isStop)
            {
                isStop = false;
            }
            else
            {
                isStop = true;
            }
        }



    }

}