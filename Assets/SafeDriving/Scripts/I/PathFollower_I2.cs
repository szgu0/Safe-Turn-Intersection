using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower_I2 : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        public PrometeoCarController car;

        public RoadChoose roadChoose;

        public CarCtrl carCtrl;

        public EndLine endLine;
        public GameOver gameOver;

        public bool isDriver = false;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            //判斷是哪一條道路
            if (roadChoose.path_1)
            {
                pathCreator = GameObject.Find("PathCreator_1").GetComponent<PathCreator>();
            }

            else if (roadChoose.path_2)
            {
                pathCreator = GameObject.Find("PathCreator_2").GetComponent<PathCreator>();
            }

            else if (roadChoose.path_3)
            {
                pathCreator = GameObject.Find("PathCreator_3").GetComponent<PathCreator>();
            }

            else if (roadChoose.path_4)
            {
                pathCreator = GameObject.Find("PathCreator_4").GetComponent<PathCreator>();
            }

            else if (roadChoose.path_5)
            {
                pathCreator = GameObject.Find("PathCreator_5").GetComponent<PathCreator>();
            }

            //判斷速度
            if (car.maxSpeed >= 20 && car.maxSpeed < 40)
            {
                speed = 5;
            }

            else if (car.maxSpeed >= 40 && car.maxSpeed < 60)
            {
                speed = 7;
            }

            else if (car.maxSpeed >= 60 && car.maxSpeed < 80)
            {
                speed = 9;
            }

            else if (car.maxSpeed >= 80 && car.maxSpeed < 100)
            {
                speed = 11;
            }

            else if (car.maxSpeed >= 100 && car.maxSpeed < 120)
            {
                speed = 13;
            }

            else if (car.maxSpeed >= 120 && car.maxSpeed <= 140)
            {
                speed = 15;
            }



            //結束
            if (gameOver.isGameOver || endLine.isOver)
            {
                speed = 0;
                distanceTravelled = 0;
            }

            if (isDriver)
            {

                //distanceTravelled += speed * Time.deltaTime;

                //transform.localPosition = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                //transform.localRotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                /*
                                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                */

                //判斷翻車
                if (carCtrl.isCar_1)
                {
                    if (roadChoose.isR_1)
                    {
                        if (car.maxSpeed < 24)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_2)
                    {
                        if (car.maxSpeed < 42)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_3)
                    {
                        if (car.maxSpeed < 55)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_4)
                    {
                        if (car.maxSpeed < 69)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_5)
                    {
                        if (car.maxSpeed < 77)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }
                }

                else if (carCtrl.isCar_2)
                {
                    if (roadChoose.isR_1)
                    {
                        if (car.maxSpeed < 20)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_2)
                    {
                        if (car.maxSpeed < 35)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_3)
                    {
                        if (car.maxSpeed < 45)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_4)
                    {
                        if (car.maxSpeed < 55)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_5)
                    {
                        if (car.maxSpeed < 63)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }
                }

                else if (carCtrl.isCar_3)
                {
                    if (roadChoose.isR_1)
                    {
                        if (car.maxSpeed < 17)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_2)
                    {
                        if (car.maxSpeed < 30)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_3)
                    {
                        if (car.maxSpeed < 39)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_4)
                    {
                        if (car.maxSpeed < 49)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_5)
                    {
                        if (car.maxSpeed < 55)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }
                }

                else if (carCtrl.isCar_4)
                {
                    if (roadChoose.isR_1)
                    {
                        if (car.maxSpeed < 15)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_2)
                    {
                        if (car.maxSpeed < 27)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_3)
                    {
                        if (car.maxSpeed < 35)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_4)
                    {
                        if (car.maxSpeed < 44)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_5)
                    {
                        if (car.maxSpeed < 49)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }
                }

                else if (carCtrl.isCar_5)
                {
                    if (roadChoose.isR_1)
                    {
                        if (car.maxSpeed < 14)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_2)
                    {
                        if (car.maxSpeed < 24)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_3)
                    {
                        if (car.maxSpeed < 32)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_4)
                    {
                        if (car.maxSpeed < 40)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }

                    else if (roadChoose.isR_5)
                    {
                        if (car.maxSpeed < 45)
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                        }
                        else
                        {
                            distanceTravelled += speed * Time.deltaTime;
                            transform.Translate(Vector3.forward * distanceTravelled * Time.deltaTime);
                        }
                    }
                }

               
            }
        }
        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }


        public void IsStartDriver()
        {
            isDriver = true;
        }
    }
}