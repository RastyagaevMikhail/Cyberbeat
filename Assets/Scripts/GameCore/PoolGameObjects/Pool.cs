using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class Pool : TransformObject
    {
        public PoolSettingsData data;
        List<PoolSetteings> Settings { get { return data.Settings; } }

        [SerializeField] Dictionary<string, Queue<SpawnedObject>> PoolDict = new Dictionary<string, Queue<SpawnedObject>> ();
        Dictionary<string, Transform> _parents = null;
        [SerializeField] Dictionary<string, Transform> Parents { get => _parents??(InitParents ()); }

        // protected override void Awake ()
        // {
        //     _parents = InitParents ();
        //     InitStartCount ();
        // }
        protected void Start ()
        {
            _parents = InitParents ();
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

        private Dictionary<string, Transform> InitParents ()
        {
            _parents = new Dictionary<string, Transform> ();
            foreach (var setting in Settings)
            {
                InitParent (setting);
            }
            return _parents;
        }

        private void InitParent (PoolSetteings setting)
        {
            var Key = setting.Key;
            Transform parent = null;
            if (setting.Parent)
                parent = setting.Parent.Value;
            else
            {
                parent = new GameObject (Key).transform;
                parent.SetParent (transform);
                parent.localPosition = Vector3.zero;
                parent.name = Key;
            }
            _parents[Key] = parent;
        }
        public void Pop (PoolData data)
        {
            Pop (data.Key, data.parent);
        }

        public SpawnedObject Pop (string Key, Transform Parent = null)
        {
            SpawnedObject pObj = null;

            data.dictSettings.TryGetValue (Key, out pObj);

            if (pObj == null)
            {
                return null;
            }
            SpawnedObject NewObj = null;

            if (PoolDict.ContainsKey (Key))
            {
                if (PoolDict[Key].Count > 0)
                    NewObj = PoolDict[Key].Dequeue ();
            }
            else
            {
                PoolDict[Key] = new Queue<SpawnedObject> ();
            }

            if (!NewObj)
                NewObj = Extend (Key);

            if (!NewObj)
                return null;

            NewObj.gameObject.SetActive (true);

            if (Parent != null)
                NewObj.SetParent (Parent);

            NewObj.ApplyOffset ();

            NewObj.OnSpawn (Key);

            return NewObj;
        }
#if UNITY_EDITOR

        public void AddToPool (SpawnedObject spawnedObject)
        {
            Settings.Add (new PoolSetteings () { Key = spawnedObject.name, Prefab = spawnedObject, startCount = 10 });
            UnityEditor.Selection.activeObject = data;
        }
#endif

        public T Pop<T> (string Key, Transform parent = null) where T : Component
        {
            return Pop (Key, parent).Get<T> ();
        }

        public void Push (SpawnedObject spawnedObject)
        {
            if (!spawnedObject)
                return;

            spawnedObject.gameObject.SetActive (false);
            spawnedObject.OnDeSpawn ();
            PoolDict[spawnedObject.Key].Enqueue (spawnedObject);
        }

        private SpawnedObject Extend (string Key)
        {
            // Debug.LogFormat ("Key = {0}", Key);
            SpawnedObject pObj = null;
            data.dictSettings.TryGetValue (Key, out pObj);
            if (pObj == null)
                return null;

            var newObj = Instantiate (pObj);

            newObj.name = string.Format ("{0}{1}", Key, newObj.GetInstanceID ());

            newObj.SetParent (Parents[Key]);

            if (!PoolDict.ContainsKey (Key))
                PoolDict[Key] = new Queue<SpawnedObject> ();

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
                AddToPool (spwn);
        }
#endif
    }

    [System.Serializable]
    public class PoolSetteings
    {
        public string Key;
        public SpawnedObject Prefab;
        public TransformVariable Parent;
        public int startCount;
        public override string ToString ()
        {
            return $"{Key}\n{Prefab}\n{Parent}\n{startCount}";
        }
    }

    [System.Serializable]
    public class PoolData
    {
        public string Key;
        public Transform parent;
    }
}
