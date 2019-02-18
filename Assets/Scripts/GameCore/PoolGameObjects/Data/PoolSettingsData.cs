using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Pool/Settings")]
    public class PoolSettingsData : ScriptableObject
    {
        Dictionary<string, SpawnedObject> _dictSettings = null;
        public Dictionary<string, SpawnedObject> dictSettings
        {
            get { return _dictSettings ?? InitDict (); }
        }
        private void OnEnable ()
        {
            InitDict ();
        }
        public Dictionary<string, SpawnedObject> InitDict ()
        {
            return _dictSettings ?? (_dictSettings = Settings.ToDictionary (s => s.Key, s => s.Prefab));
        }
        public List<PoolSetteings> Settings;
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
        public void AddToPool (SpawnedObject spawnedObject)
        {
            Settings.Add (new PoolSetteings () { Key = spawnedObject.name, Prefab = spawnedObject, startCount = 10 });
            UnityEditor.Selection.activeObject = this;
        }
#endif
    }
}
