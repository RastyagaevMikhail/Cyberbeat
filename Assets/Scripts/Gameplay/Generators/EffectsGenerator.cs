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
        public void OnBit (IBitData bitData)
        {
            var spawnedSkin = pool.Pop ("Effects");

            spawnedSkin.Apply (this, true, true, false);
            spawnedSkin.position += spawnedSkin.OffsetPosition;

            var skinSetter = spawnedSkin.Get<EffectSkinSetter> ();

            skinSetter.InitSkin (selector[bitData.StringValue]);
        }

    }
}
