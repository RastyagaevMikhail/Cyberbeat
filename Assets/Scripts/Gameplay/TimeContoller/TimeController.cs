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
        public bool StartCountTime { get; set; }

        void Awake ()
        {
            OnAwake.Invoke ();
        }
        void Update ()
        {
            if (!StartCountTime) return;

            OnUpdate.Invoke (time);
            
            time += Time.deltaTime;
        }
    }
}
