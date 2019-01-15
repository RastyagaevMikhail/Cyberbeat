using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Pool/Settings")]
    public class PoolSettingsData : ScriptableObject
    {
        public List<PoolSetteings> Settings;
    }
}
