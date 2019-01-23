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
        public EffectSkinsDataCollection effectsColleciotns { get { return EffectSkinsDataCollection.instance; } }
        public Pool pool { get { return Pool.instance; } }
        public void OnBit (IBitData bitData)
        {
            var spawnedSkin = pool.Pop ("Effects");
            // Debug.LogFormat ("spawnedSkin = {0}", spawnedSkin);
            spawnedSkin.Apply (this, true, true, false);
            spawnedSkin.position += spawnedSkin.OffsetPosition;
            var skinSetter = spawnedSkin.Get<EffectSkinSetter> ();

            string skinName = bitData.Strings[0];
            EffectSkinData data = effectsColleciotns[skinName];

            int intShapeValue = bitData.Ints[1];;
            // Debug.LogFormat ("intShapeValue = {0}", intShapeValue);
            Shapes shape = (Shapes) intShapeValue;
            // Debug.LogFormat ("shape = {0}", shape);
            skinSetter.InitSkin (data, shape);
            skinSetter.FadeIn ();
            spawnedSkin.Get<Rotator> ().speed = (float) bitData.Ints[2];
        }

    }
}
