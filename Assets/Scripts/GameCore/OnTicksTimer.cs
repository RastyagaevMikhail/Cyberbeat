using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class OnTicksTimer : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] UnityEventInt OnStartTimer;
        [DrawWithUnity]
        [SerializeField] UnityEventInt OnTickTimer;
        [DrawWithUnity]
        [SerializeField] UnityEvent OnEndTimer;
        [SerializeField] int Seconds;
        [SerializeField] float timeTick = 1f;

        public void StartTimer ()
        {
            StartTimer (0);
        }
        public void StartTimer (int seconds = 0)
        {
            if (seconds != 0) Seconds = seconds;
            OnStartTimer.Invoke (Seconds);
            StartCoroutine (TickTimer ());
        }

        private IEnumerator TickTimer ()
        {
            if (CheckComplete ()) yield break;
            Seconds = Seconds.Abs (); // Seconds = Mathf.Abs(Seconds); // Is to need positive
            WaitForSeconds wfs = new WaitForSeconds (timeTick);
            while (Seconds > 0)
            {
                yield return wfs;
                Seconds--;
                OnTickTimer.Invoke (Seconds);
                if (CheckComplete ()) yield break;
            }

        }

        private bool CheckComplete ()
        {
            bool isZero = Seconds == 0;
            if (isZero) OnEndTimer.Invoke ();
            return isZero;
        }
    }
}
