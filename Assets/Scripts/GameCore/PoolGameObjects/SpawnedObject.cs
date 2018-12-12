using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.OdinInspector;
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
        public void ActionWith<T> (Action<T> ActionWhithComponent) where T : Component
        {
            ActionWhithComponent (Get<T> ());
        }

        [DrawWithUnity]
        public UnityEvent OnSpawn;
        [DrawWithUnity]
        public UnityEvent OnDeSpawn;
        public float yOffset;
        public void PushToPool ()
        {
            Pool.instance.Push (gameObject);
        }
    }
}
