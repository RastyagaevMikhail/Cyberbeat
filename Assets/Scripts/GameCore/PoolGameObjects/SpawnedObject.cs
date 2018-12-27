
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

        
        public UnityEvent OnSpawn;
        
        public UnityEvent OnDeSpawn;
        public Vector3 OffsetPosition;
        public void PushToPool ()
        {
            Pool.instance.Push (gameObject);
        }
    }
}
