using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;
using UnityEngine.UI;

using TMPro;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class L5PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        private bool isStop;

        [SerializeField]
        private float TrainSpeed = 0.5f;

        [SerializeField]
        private float PackageSize;
        [SerializeField]
        private float PackageWei;
        [SerializeField]
        private GameObject Package;

        [SerializeField]
        private LineChartController lineChart;
        private bool isFlip;

        [SerializeField]
        private GameObject Package_image1;
        [SerializeField]
        private GameObject Package_image0_point;
        [SerializeField]
        private GameObject Package_image1_point;
        [SerializeField]
        private GameObject Package_image2;
        [SerializeField]
        private TextMeshProUGUI Package_force_value;
        [SerializeField]
        private TextMeshProUGUI Package_N_value;
        [SerializeField]
        private TextMeshProUGUI ms_value;
        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            isFlip = true;
        }

        void Update()
        {
            Package.transform.localPosition = new Vector3(-PackageSize * 1.8f-1.5f, Package.transform.localPosition.y, Package.transform.localPosition.z);
            Package.transform.localScale = new Vector3(Package.transform.localScale.x, Package.transform.localScale.y, 1+PackageWei*3);
            if (pathCreator != null && !isStop)
            {
                //lineChart.LineUpdate(TrainSpeed);

                if (!isFlip)
                {
                    distanceTravelled += TrainSpeed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }

                if (10*0.2f*0.2f/(2 *TrainSpeed * TrainSpeed) < (PackageSize*5+5)/100 && transform.localPosition.x <= -1.68f && !isFlip)
                {
                    isFlip = true;
                    StartCoroutine("FallAndReset");
                }

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
            ms_value.text = string.Format("{0:f1}", speed);
        }
        public void ChangeSize(float size)
        {
            PackageSize = size;
            Package_N_value.text = $"{size*5+5}";
            // if (size == 1f)
            // {
            //     Package_image1.SetActive(false);
            //     Package_image0_point.SetActive(true);
            //     Package_image1_point.SetActive(false);
            //     Package_image2.SetActive(false);
            // }
            // else if (size == 2f)
            // {
            //     Package_image1.SetActive(true);
            //     Package_image0_point.SetActive(false);
            //     Package_image1_point.SetActive(true);
            //     Package_image2.SetActive(false);
            // }
            // else
            // {
            //     Package_image1.SetActive(true);
            //     Package_image0_point.SetActive(false);
            //     Package_image1_point.SetActive(false);
            //     Package_image2.SetActive(true);
            // }
        }

        public void ChangeWei(float Wei)
        {
            PackageWei = Wei;
            Package_force_value.text = string.Format("{0:f1}", Wei);
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

        public void PathRun()
        {
            distanceTravelled = 0;
            isStop = false;
            isFlip = false;
        }



    }

}