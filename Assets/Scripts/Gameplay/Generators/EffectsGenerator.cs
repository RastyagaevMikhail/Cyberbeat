using FluffyUnderware.Curvy;

using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class EffectsGenerator : TransformObject
    {

        [SerializeField] EffectDataPresetSelector selector;
        [SerializeField] PoolVariable pool;
        [SerializeField] CurvySplineVariable splineVariable;
        [SerializeField] float aheadDistance = 45f;
        CurvySpline spline => splineVariable.ValueFast;
        public void OnBit (IBitData bitData)
        {
            var spawnedSkin = pool.Pop ("Effects");

            float nearestPointTF = spline.GetNearestPointTF (position);
            float distance = spline.TFToDistance (nearestPointTF);
            distance += aheadDistance;
            float aheadTF = spline.DistanceToTF (distance);

            spawnedSkin.position = spline.InterpolateFast (aheadTF);
            spawnedSkin.rotation = spline.GetOrientationFast (aheadTF);

            var skinSetter = spawnedSkin.Get<EffectSkinSetter> ();

            skinSetter.InitSkin (selector[bitData.StringValue]);
        }

    }
}
