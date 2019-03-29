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

        [SerializeField] EffectDataPresetSelector selector;
        [SerializeField] TrackVariable track;
        [SerializeField] CurvySplineVariable splineVariable;
        [SerializeField] bool fixedDistance = true;
        [ShowIf ("fixedDistance")]
        [SerializeField] float aheadDistance = 45f;
        CurvySpline spline => splineVariable.Value;
        public Color color { get; set; }
        Queue<SpawnedObject> EffectsPool;
        private void Start ()
        {
            EffectsPool = new Queue<SpawnedObject> ();
            var originalInstance = Instantiate (Resources.Load<SpawnedObject> ($"Prefabs/Effects/{track.Value.name}"));
            originalInstance.gameObject.SetActive (false);
            EffectsPool.Enqueue (originalInstance);
            for (int i = 0; i < 9; i++)
            {
				SpawnedObject item = Instantiate(originalInstance);
                item.gameObject.SetActive(false);
				EffectsPool.Enqueue(item);
            }
        }
        public void OnBit (IBitData bitData)
        {
            EffectDataPreset preset = selector[bitData.StringValue];
            var spawnedSkin = GameEventSkinItem ();
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
