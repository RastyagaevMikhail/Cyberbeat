using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.Utilities;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class SpawnedObject : TransformObject
    {
        Dictionary<Type, Component> ChashedComponents = new Dictionary<Type, Component> ();
        public T Get<T> () where T : Component
        {
            var type = typeof (T);
            if (!ChashedComponents.ContainsKey (type))
                ChashedComponents.Add (type, GetComponent<T> ());

            T result = ChashedComponents[type] as T;
            return result;
        }
        public void ActionWith<T> (System.Action<T> ActionWhithComponent) where T : Component
        {
            ActionWhithComponent (Get<T> ());
        }
        public UnityEvent OnSpawn;
        public UnityEvent OnDeSpawn;
        public float yOffset;

        public string MetaData;
        bool startTimeticker = false;

        public void PushToPool ()
        {
            Pool.instance.Push (gameObject);
        }

        [SerializeField] float timeAutoPush = 1.2f;
        private float time;
        private void OnEnable ()
        {
            startTimeticker = true;
            time = 0f;
        }
        private void OnDisable ()
        {
            startTimeticker = false;
        }
        private void Update ()
        {
            if (startTimeticker && timeAutoPush != 0)
            {
                time += Time.deltaTime;
                if (timeAutoPush <= time) Pool.instance.Push (gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos ()
        {
            if (MetaData.IsNullOrWhitespace ()) return;

            Vector3 p = transform.position;
            Vector3 up = transform.up;
            Handles.Label (p + up * HandleUtility.GetHandleSize (p), MetaData, Tools.BackdropHtmlLabel);
        }
#endif
    }
}
