using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class UIPool : MonoBehaviour
    {
        private static UIPool _instance = null;
        public static UIPool instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<UIPool> (); return _instance; } }

        public List<PoolSetteings> Settings;
        [SerializeField] Dictionary<string, List<SpawnedObject>> PoolDict = new Dictionary<string, List<SpawnedObject>> ();
        [SerializeField] Dictionary<string, Transform> Parents = new Dictionary<string, Transform> ();
        private float timeSpawn = 0.2f;
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
            foreach (var item in Settings)
            {
                var Key = item.Key;
                var parent = new GameObject (Key, typeof (RectTransform)).transform;
                parent.SetParent (transform);
                parent.name = Key;
                Parents[Key] = parent;
            }
        }

        public SpawnedObject Pop (string Key)
        {
            var pObj = Settings.Find (ps => ps.Key == Key);
            if (pObj == null) return null;

            if (!PoolDict.ContainsKey (Key))
                PoolDict[Key] = new List<SpawnedObject> ();

            var NewObj = PoolDict[Key].Find (po => !po.isActiveAndEnabled);
            if (!NewObj) NewObj = Extend (Key);
            if (!NewObj) return null;

            NewObj.gameObject.SetActive (true);

            NewObj.OnSpawn.Invoke ();
            return NewObj;
        }

        public void Push (GameObject go)
        {
            var spawnedObj = go.GetComponent<SpawnedObject> ();
            if (!spawnedObj) return;
            foreach (var ListItems in PoolDict.Values)
            {
                foreach (var item in ListItems)
                {
                    if (item.gameObject.Equals (go))
                    {
                        go.SetActive (false);
                        spawnedObj.OnDeSpawn.Invoke ();
                    }
                }
            }
        }

        private SpawnedObject Extend (string Key)
        {
            var pObj = Settings.Find (ps => ps.Key == Key);
            if (pObj == null) return null;

            var newObj = Instantiate (pObj.Prefab);

            newObj.name = string.Format ("{0}{1}", Key, newObj.GetInstanceID ());

            newObj.transform.SetParent (Parents[Key]);

            if (!PoolDict.ContainsKey (Key))
                PoolDict[Key] = new List<SpawnedObject> ();
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
}
