using System;
using System.Collections;
using System.Collections.Generic;

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

        [SerializeField] bool useOnSpawn;
        [SerializeField] UnityEvent onSpawn;
        string key;
        public string Key { get => key; }

        public void OnSpawn (String _key)
        {
            key = _key;
            if (useOnSpawn) onSpawn.Invoke ();
        }

        [SerializeField] bool useOnDeSpawn;
        [SerializeField] UnityEvent onDeSpawn;

        [SerializeField] bool debug;
        public void OnDeSpawn ()
        {
            if (useOnDeSpawn)
                onDeSpawn.Invoke ();
        }
        public Vector3 OffsetPosition;
        public Quaternion OffsetRotation;
        [SerializeField] PoolVariable pool;
        public void PushToPool ()
        {
            if (debug)
                Debug.LogFormat ("PushToPool = {0}", this);
            pool.Push (this);
        }
        public void Pop (string key)
        {
            pool.Pop (key);
        }

        public void ApplyOffset (bool posistionApply = true, bool rotationApply = true)
        {
            if (posistionApply)
                localPosition = OffsetPosition;
            if (rotationApply)
                localRotation = OffsetRotation;
        }

        [ContextMenu ("Save Offset")]
        void SaveOffset ()
        {
            OffsetPosition = localPosition;
            OffsetRotation = localRotation;
        }
#if UNITY_EDITOR

        [ContextMenu ("Add To Pool")]
        void AddToPool ()
        {
            pool.AddToPool (this);
        }
#endif
    }
}
