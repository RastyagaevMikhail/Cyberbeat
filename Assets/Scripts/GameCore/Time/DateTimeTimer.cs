using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class DateTimeTimer : MonoBehaviour
    {
        [SerializeField] DateTimeVariable lastStartedTime;
        [SerializeField] TimeSpanVariable timeFromCheck;
        [SerializeField] UnityEventBool onCheckComplete;
        [SerializeField] UnityEventTimeSpan onTimerTick;
        [SerializeField] UnityEvent onTimerComplited;
        [SerializeField] bool debug;
        [SerializeField] bool debugTicks;

        public DateTime LastStartedTime { get => lastStartedTime.Value; set => lastStartedTime.Value = value; }
        public TimeSpan TimeFromCheck { get => timeFromCheck.Value; set => timeFromCheck.Value = value; }
        public TimeSpan DiffTime => (LastStartedTime + TimeFromCheck) - DateTime.Now;

        bool isComplited = false;
        TimeSpan elapsedTime;
        private void Start ()
        {

            isComplited = checkComplete ();

            if (isComplited)
                Complete ();

            if (debug)
            {
                Debug.LogFormat ("isComplited = {0}", isComplited);
                Debug.LogFormat ("LastStartedTime.Ticks = {0}", LastStartedTime.Ticks);
                Debug.LogFormat ("DiffTime = {0}", DiffTime);
            }
        }

        private bool checkComplete ()
        {
            bool isComplited = LastStartedTime.Ticks == 0 ||
                DiffTime <= TimeSpan.Zero;
            if (debug) Debug.Log ($"checkComplete.{isComplited}");
            onCheckComplete.Invoke (isComplited);

            return isComplited;
        }

        public void StartTimer ()
        {
            if (debug)
            {
                Debug.Log ("StartTimer");
                Debug.LogFormat ("TimeFromCheck = {0}", TimeFromCheck);
            }

            LastStartedTime = DateTime.Now;
            elapsedTime = TimeFromCheck;
            isComplited = false;
        }

        void Update ()
        {
            if (!isComplited)
            {
                OnTick ();
                elapsedTime = DiffTime;
                bool _isComplited = checkComplete();
                if (_isComplited)
                {
                    isComplited = true;
                    Complete ();
                }
            }
        }

        private void OnTick ()
        {
            onTimerTick.Invoke (elapsedTime);
            if (debugTicks) Debug.Log (onTimerTick.Log (elapsedTime));
        }

        private void Complete ()
        {
            onTimerComplited.Invoke ();
            if (debug) Debug.Log (onTimerComplited.Log ("DateTimeTimer.Complete".mb ()));
        }
    }
}
