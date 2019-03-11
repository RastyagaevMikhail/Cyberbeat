using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        List<T> itemsFromRemove = new List<T> ();
        bool startIterrate = false;
        [SerializeField] protected List<T> items = new List<T> ();
        public T[] ToArray () => items.ToArray ();
        public T GetRandom () => items.GetRandom ();

        protected abstract UnityEvent<T> OnAddComplete { get; }
        protected abstract UnityEvent<T> OnRemoveComplete { get; }
        public int Count { get { return items.Count; } }
        public void Add (T item)
        {
            if (!Contains (item))
            {
                items.Add (item);
                OnAddComplete.Invoke (item);
            }
        }

        public void ForEach (Action<T> action)
        {
            if (action == null) return;
            startIterrate = true;
            foreach (var item in items)
                action (item);
            startIterrate = false;
            if (_onItterrateComplete != null) _onItterrateComplete ();
        }
        Action _onItterrateComplete;
        void onItterrateComplete ()
        {
            _onItterrateComplete -= onItterrateComplete;

            foreach (var item in itemsFromRemove)
            {
                items.Remove (item);
                OnRemoveComplete.Invoke (item);
            }
            itemsFromRemove.Clear ();
        }
        public void Remove (T item)
        {
            if (Contains (item))
            {
                if (startIterrate)
                {
                    if (itemsFromRemove == null) itemsFromRemove = new List<T> ();
                    itemsFromRemove.Add (item);
                    _onItterrateComplete += onItterrateComplete;
                }
                else
                {
                    items.Remove (item);
                    OnRemoveComplete.Invoke (item);
                }
            }
        }
        public void Clear ()
        {
            ForEach (item => Remove (item));
        }
        public bool Contains (T item)
        {
            return items.Contains (item);
        }
    }
}
