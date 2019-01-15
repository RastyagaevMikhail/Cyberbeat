using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (BitGenerator))]
    public class EffectsGenerator : TransformObject
    {
        private BitGenerator _generator = null;
        public BitGenerator generator { get { if (_generator == null) _generator = GetComponent<BitGenerator> (); return _generator; } }
        public EffectSkinsDataCollection effectsColleciotns { get { return EffectSkinsDataCollection.instance; } }
        public Pool pool { get { return Pool.instance; } }
        public void OnBit (KoreographyEvent koreographyEvent)
        {
            var dataPayload = koreographyEvent.GetTextValue ().Split (',');

            var spawnedSkin = pool.Pop ("Effects");

            spawnedSkin.Apply (this, true, true, false);
            spawnedSkin.position += spawnedSkin.OffsetPosition;
            var skinSetter = spawnedSkin.Get<EffectSkinSetter> ();

            string skinName = dataPayload[0];
            EffectSkinData data = effectsColleciotns[skinName];

            string shapeValue = dataPayload[1];
            // Debug.LogFormat ("shapeValue = {0}", shapeValue);
            int intShapeValue = int.Parse (shapeValue);
            // Debug.LogFormat ("intShapeValue = {0}", intShapeValue);
            Shapes shape = (Shapes) intShapeValue;
            // Debug.LogFormat ("shape = {0}", shape);
            skinSetter.InitSkin (data, shape);

            spawnedSkin.Get<Rotator> ().speed = float.Parse (dataPayload[2]);
        }

    }
}
