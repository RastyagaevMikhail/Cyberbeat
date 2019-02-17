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
        [SerializeField] List<TrackBitItemData> trackBitItemDatas;
        public bool StartCountTime { get; set; }

        void Awake ()
        {
            OnAwake.Invoke ();
            foreach (var item in trackBitItemDatas)
                item.Start ();
        }
        void Update ()
        {
            if (!StartCountTime) return;

            OnUpdate.Invoke (time);
            
            foreach (var item in trackBitItemDatas)
                item.UpdateInTime (time);

            time += Time.deltaTime;
        }
    }
}
