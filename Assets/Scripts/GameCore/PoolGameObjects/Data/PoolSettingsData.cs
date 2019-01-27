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
    }
}
