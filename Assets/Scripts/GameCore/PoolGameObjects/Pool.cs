using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class Pool : MonoBehaviour
    {
        public PoolSettingsData data;
        List<PoolSetteings> Settings { get { return data.Settings; } }

        Dictionary<string, SpawnedObject> _dictSettings = null;
        Dictionary<string, SpawnedObject> dictSettings
        {
            get { return _dictSettings ?? (_dictSettings = Settings.ToDictionary (s => s.Key, s => s.Prefab)); }
        }

        [SerializeField] Dictionary<string, List<SpawnedObject>> PoolDict = new Dictionary<string, List<SpawnedObject>> ();
        [SerializeField] Dictionary<string, Transform> Parents = new Dictionary<string, Transform> ();
        private void Awake ()
        {
            InitParents ();
            InitStartCount ();
        }

        private void InitStartCount ()
        {
            foreach (var item in Settings)
            {
                for (int i = 0; i < item.startCount; i++)
                {
                    Extend (item.Key);
                }
            }
        }

        private void InitParents ()
        {
            foreach (var setting in Settings)
            {
                InitParent (setting);
            }
        }

        private void InitParent (PoolSetteings setting)
        {
            var Key = setting.Key;
            var parent = new GameObject (Key).transform;
            parent.SetParent (transform);
            parent.name = Key;
            Parents[Key] = parent;
        }
        public void Pop (PoolData data)
        {
            Pop (data.Key, data.parent);
        }

        public SpawnedObject Pop (string Key, Transform Parent = null)
        {
            SpawnedObject pObj = null;

            dictSettings.TryGetValue (Key, out pObj);

            if (pObj == null)
            {
                return null;
            }

            if (!PoolDict.ContainsKey (Key))
            {
                PoolDict[Key] = new List<SpawnedObject> ();
            }

            var NewObj = PoolDict[Key].Find (po => !po.isActiveAndEnabled);
            if (!NewObj)
            {
                NewObj = Extend (Key);
            }

            if (!NewObj)
            {
                return null;
            }

            NewObj.gameObject.SetActive (true);

            if (Parent == null) Parent = Parents[Key];

            NewObj.SetParent (Parent);
            NewObj.ApplyOffset ();

            NewObj.OnSpawn ();
            return NewObj;
        }
        public T Pop<T> (string Key, Transform parent = null) where T : Component
        {
            return Pop (Key, parent).Get<T> ();
        }

        public void Push (GameObject go, bool force = false)
        {
            var spawnedObj = go.GetComponent<SpawnedObject> ();
            if (!spawnedObj && !force)
            {
                return;
            }

            if (force && !spawnedObj)
            {
                spawnedObj = go.AddComponent<SpawnedObject> ();
                PoolSetteings setting = new PoolSetteings () { Key = spawnedObj.name, Prefab = spawnedObj };
                Settings.Add (setting);
                InitParent (setting);
                Extend (setting.Key);
            }
            foreach (var ListItems in PoolDict.Values)
            {
                foreach (var item in ListItems)
                {
                    if (item.gameObject.Equals (go))
                    {
                        go.SetActive (false);
                        spawnedObj.OnDeSpawn ();
                    }
                }
            }
        }

        private SpawnedObject Extend (string Key)
        {
            // Debug.LogFormat ("Key = {0}", Key);
            var pObj = Settings.Find (ps => ps.Key == Key);
            if (pObj == null)
            {
                return null;
            }

            var newObj = Instantiate (pObj.Prefab);

            newObj.name = string.Format ("{0}{1}", Key, newObj.GetInstanceID ());

            newObj.transform.SetParent (Parents[Key]);

            if (!PoolDict.ContainsKey (Key))
            {
                PoolDict[Key] = new List<SpawnedObject> ();
            }

            PoolDict[Key].Add (newObj);

            newObj.gameObject.SetActive (false);

            return newObj;
        }

#if UNITY_EDITOR
        [ContextMenu ("Add to Pool selected objects")]
        void AddToPoolSelected ()
        {
            var gos = UnityEditor.Selection.gameObjects;
            var spwns = gos.ToList ()
                .FindAll (go => go.GetComponent<SpawnedObject> ())
                .Select (go => go.GetComponent<SpawnedObject> ());
            foreach (var spwn in spwns)
            {
                Settings.Add (new PoolSetteings () { Key = spwn.name, Prefab = spwn, startCount = 10 });
            }
        }
#endif
    }

    [System.Serializable]
    public class PoolSetteings
    {
        public string Key;
        public SpawnedObject Prefab;
        public int startCount;
    }

    [System.Serializable]
    public class PoolData
    {
        public string Key;
        public Transform parent;
    }
}
