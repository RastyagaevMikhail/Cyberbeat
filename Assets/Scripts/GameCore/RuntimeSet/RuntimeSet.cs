using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        List<T> itemsFromRemove = new List<T> ();
        bool startIterrate = false;
        [SerializeField] List<T> items = new List<T> ();
        public List<T> Items { get { return items; } }
        public int Count { get { return Items.Count; } }
        public void Add (T thing)
        {
            if (!Contains (thing))
                items.Add (thing);
        }

        public void ForEach (Action<T> action)
        {
            if (action == null) return;
            startIterrate = true;
            foreach (var item in Items)
                action (item);
            startIterrate = false;
            if (_onItterrateComplete != null) _onItterrateComplete ();
        }
        Action _onItterrateComplete;
        void onItterrateComplete ()
        {
            _onItterrateComplete -= onItterrateComplete;

            foreach (var item in itemsFromRemove)
                items.Remove (item);
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
                    items.Remove (item);
            }
        }
        public bool Contains (T item)
        {
            return Items.Contains (item);
        }
    }
}
