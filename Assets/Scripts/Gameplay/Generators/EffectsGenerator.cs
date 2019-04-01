using FluffyUnderware.Curvy;

using GameCore;

using Sirenix.OdinInspector;

using SonicBloom.Koreo;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class EffectsGenerator : TransformObject
    {

        [SerializeField] TransformVariable pools;
        [SerializeField] EffectDataPresetSelector selector;
        [SerializeField] TrackVariable track;
        [SerializeField] CurvySplineVariable splineVariable;
        [SerializeField] bool fixedDistance = true;
        [ShowIf ("fixedDistance")]
        [SerializeField] float aheadDistance = 45f;
        CurvySpline spline => splineVariable.Value;
        public Color color { get; set; }
        Pool pool;
        [SerializeField] PoolVariable EffectsPoolVariable;
        void Awake ()
        {
            pool = new GameObject ("[Effects Pool]").AddComponent<Pool> ();
            pool.SetParent (pools.Value);
            var settings = Resources.Load<PoolSettingsData> ($"Data/Pool/Settings/Effects/{track.Value.name}");
            pool.data = settings;
            EffectsPoolVariable.Value = pool;
        }
        public void OnBit (IBitData bitData)
        {
            EffectDataPreset preset = selector[bitData.StringValue];
            var spawnedSkin = pool.Pop (preset.PrefabName);
            if (fixedDistance)
            {
                float nearestPointTF = spline.GetNearestPointTF (position);
                float distance = spline.TFToDistance (nearestPointTF);
                distance += aheadDistance;
                float aheadTF = spline.DistanceToTF (distance);

                spawnedSkin.position = spline.InterpolateFast (aheadTF);
                spawnedSkin.rotation = spline.GetOrientationFast (aheadTF);
            }
            else
            {
                spawnedSkin.Apply (this, true, true);
            }

            var skinSetter = spawnedSkin.Get<EffectSkinSetter> ();

            skinSetter.color = color;
            skinSetter.InitSkin (preset);
        }

        private SpawnedObject GameEventSkinItem ()
        {
            SpawnedObject result = null;

            return result;
        }
    }
}
