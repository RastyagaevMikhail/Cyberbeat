using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class ResourcePool : MonoBehaviour
    {
        [SerializeField] ResourcePoolSettingsData data;
        List<ResourcePoolSettings> settings { get { return data.settings; } }
        private static ResourcePool _instance = null;
        public static ResourcePool instance { get { if (_instance == null) (_instance = GameObject.FindObjectOfType<ResourcePool> ()).Init (); return _instance; } }

        private void Init ()
        {
            settingsDict = settings.ToDictionary (s => s.key, s => s.source);
            foreach (var set in settings)
                for (int i = 0; i < set.count; i++)
                    Extend (set.key);
        }

        Dictionary<string, Queue<Object>> poolDict = new Dictionary<string, Queue<Object>> ();
        Dictionary<string, Object> settingsDict = new Dictionary<string, Object> ();
        private void Awake ()
        {
            if (_instance == null)
                (_instance = this).Init ();
        }
        private Object Extend (string key)
        {
            // Debug.LogFormat ("Key = {0}", Key);
            Object pObj = null;
            settingsDict.TryGetValue (key, out pObj);
            if (pObj == null)
            {
                return null;
            }

            var newObj = Instantiate (pObj);

            newObj.name = string.Format ("{0}{1}", key, newObj.GetInstanceID ());

            if (!poolDict.ContainsKey (key))
            {
                poolDict[key] = new Queue<Object> ();
            }
            poolDict[key].Enqueue (newObj);
            return newObj;
        }
        public Object Pop (string key)
        {
            Object pObj = null;

            settingsDict.TryGetValue (key, out pObj);

            if (pObj == null)
            {
                return null;
            }

            if (!poolDict.ContainsKey (key))
            {
                poolDict[key] = new Queue<Object> ();
            }

            var NewObj = poolDict[key].Dequeue ();
            if (!NewObj)
            {
                NewObj = Extend (key);
            }

            if (!NewObj)
            {
                return null;
            }

            return NewObj;
        }
        public T Pop<T> (string Key) where T : Object
        {
            return Pop (Key) as T;
        }

        public void Push (string key, Object obj)
        {
            Queue<Object> queue = null;
            poolDict.TryGetValue (key, out queue);
            if (queue == null)
                queue = poolDict[key] = new Queue<Object> ();
            queue.Enqueue (obj);

        }

        [System.Serializable]
        public class ResourcePoolSettings
        {
            public string key;
            public Object source;
            public int count;
        }

    }
}
