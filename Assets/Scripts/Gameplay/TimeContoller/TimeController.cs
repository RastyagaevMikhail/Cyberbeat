using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] float time;
        [SerializeField] UnityEvent OnAwake;
        [SerializeField] UnityEventFloat OnUpdate;
        [SerializeField] UnityEventFloat OnPaused;
        [SerializeField] UnityEventFloat OnStarted;
        private bool _startCountTime;

        public bool StartCountTime
        {
            get => _startCountTime;
            set
            {
                _startCountTime = value;
                if (value) OnStarted.Invoke (time);
                else OnPaused.Invoke (time);
            }
        }

        private void OnEnable ()
        {
            OnStarted.Invoke (time);
        }
        void Start ()
        {
            OnAwake.Invoke ();
        }
        private void OnDisable ()
        {
            OnPaused.Invoke (time);
        }
        void Update ()
        {
            if (!StartCountTime) return;

            OnUpdate.Invoke (time);

            time += Time.deltaTime;
        }
    }
}
