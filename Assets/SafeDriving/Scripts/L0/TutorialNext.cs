using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MPack
{
    public class TutorialNext : MonoBehaviour
    {
        public UnityEvent Event;

        void Start()
        {
            StartCoroutine(WaitToNext());
        }
        IEnumerator WaitToNext()
        {

            yield return new WaitForSeconds(0);
            Event.Invoke();


        }
    }
}