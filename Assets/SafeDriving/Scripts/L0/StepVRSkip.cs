using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MPack;
namespace MPack
{
    public class StepVRSkip : MonoBehaviour
    {
        [SerializeField]
        private AppSetting appSetting;
        [SerializeField]
        private StepsGuide stepsGuide;

        [SerializeField]
        private int targetStep;
        public IntUnityEvent IntEvent;

        public void ifVR()
        {
            if (appSetting.IsVR)
            {
                IntEvent.Invoke(targetStep);
                Debug.Log("123");
            }
        }


    }
}