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
        public void ActionWith<T> (Action<T> ActionWhithComponent) where T : Component
        {
            ActionWhithComponent (Get<T> ());
        }

        [SerializeField] UnityEvent onSpawn;
        [SerializeField] bool useOnSpawn;
        string key;
        public string Key { get => key; }

        public void OnSpawn (String _key)
        {
            key = _key;
            if (useOnSpawn) onSpawn.Invoke ();
        }
        [SerializeField] UnityEvent onDeSpawn;
        [SerializeField] bool useOnDeSpawn;

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
            pool.Push (this);
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
    }
}
