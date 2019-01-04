using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class OnTicksTimer : MonoBehaviour
    {

        [SerializeField] UnityEventInt OnStartTimer;

        [SerializeField] UnityEventInt OnTickTimer;

        [SerializeField] UnityEvent OnEndTimer;
        [SerializeField] int Seconds;
        int seconds;
        [SerializeField] float timeTick = 1f;

        public void StartTimer ()
        {
            seconds = Seconds;
            StartTimer (seconds);
        }
        public void StartTimer (int seconds = 0)
        {
            if (seconds != 0) this.seconds = seconds;
            OnStartTimer.Invoke (seconds);
            StartCoroutine (TickTimer ());
        }

        private IEnumerator TickTimer ()
        {
            if (CheckComplete ()) yield break;
            seconds = seconds.Abs (); // seconds = Mathf.Abs(seconds); // Is to need positive
            WaitForSeconds wfs = new WaitForSeconds (timeTick);
            while (seconds > 0)
            {
                yield return wfs;
                seconds--;
                OnTickTimer.Invoke (seconds);
                if (CheckComplete ()) break;
            }

            yield return null;
        }

        private bool CheckComplete ()
        {
            bool isZero = seconds == 0;
            Debug.LogFormat ("CheckComplete = {0}", isZero);
            if (isZero) OnEndTimer.Invoke ();
            return isZero;
        }
    }
}
