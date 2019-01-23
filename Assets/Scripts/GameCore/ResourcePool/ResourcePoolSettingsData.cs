using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
        [CreateAssetMenu (menuName = "GameCore/ResourcePoolSettingsData")]
        public class ResourcePoolSettingsData : ScriptableObject
        {
                public List<ResourcePool.ResourcePoolSettings> settings;
#if UNITY_EDITOR

                [ContextMenu ("AddToPool")]
                void AddToPool ()
                {
                        foreach (var obj in UnityEditor.Selection.objects)
                                settings.Add (new ResourcePool.ResourcePoolSettings ()
                                {
                                        key = obj.name, source = obj, count = 10
                                });
                }
#endif
        }
}
